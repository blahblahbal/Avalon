using System;
using System.Collections.Generic;
using Avalon.UI.InterfaceLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI;

public abstract class ExxoUIElement : UIElement
{
    private bool mouseWasOver;
    public event MouseEvent? OnFirstMouseOver;
    public event MouseEvent? OnLastMouseOut;
    public event MouseEvent? OnMouseHovering;
    public event EventHandler<EventArgs>? OnRecalculateFinish;
    public Queue<UIElement> ElementsForRemoval { get; } = new();

    public bool IsVisible => Active && !Hidden && GetOuterDimensions().Width > 0 && GetOuterDimensions().Height > 0;
    public int ElementCount => Elements.Count;

    public abstract bool IsDynamicallySized { get; }

    public bool Active { get; set; } = true;
    public bool Hidden { get; set; }
    public bool IsRecalculating { get; private set; }
    public string Tooltip { get; set; } = "";

    public static void BeginDefaultSpriteBatch(SpriteBatch spriteBatch) =>
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, DepthStencilState.None, null, null,
            Main.UIScaleMatrix);

    public override void Update(GameTime gameTime)
    {
        if (!Active)
        {
            return;
        }

        if (IsMouseHovering)
        {
            OnMouseHovering?.Invoke(new UIMouseEvent(this, UserInterface.ActiveInstance.MousePosition), this);
        }

        UpdateSelf(gameTime);
        base.Update(gameTime);
        while (ElementsForRemoval.TryDequeue(out UIElement? uiElement))
        {
            RemoveChild(uiElement);
        }
    }

    public override bool ContainsPoint(Vector2 point) => IsVisible && base.ContainsPoint(point);

    public sealed override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsVisible)
        {
            return;
        }

        base.Draw(spriteBatch);

        if (IsMouseHovering && !string.IsNullOrEmpty(Tooltip))
        {
            ModContent.GetInstance<ExxoUIElementTooltipInterfaceLayer>().SetText(Tooltip);
        }
    }

    public sealed override void Recalculate()
    {
        IsRecalculating = true;
        PreRecalculate();
        RecalculateSelf();
        RecalculateChildren();
        PostRecalculate();
        if (Parent is not ExxoUIElement)
        {
            RecalculateFinish();
        }

        IsRecalculating = false;
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        base.MouseOver(evt);
        if (!mouseWasOver)
        {
            mouseWasOver = true;
            FirstMouseOver(evt);
        }
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        base.MouseOut(evt);
        if (!ContainsPoint(evt.MousePosition))
        {
            mouseWasOver = false;
            LastMouseOut(evt);
        }
    }

    /// <summary>
    ///     Optimised method that only moves positions, only to be used if the elements have already previously been
    ///     recalculated
    /// </summary>
    public virtual void RecalculateChildrenSelf()
    {
        RecalculateSelf();
        foreach (UIElement element in Elements)
        {
            if (element is ExxoUIElement exxoElement)
            {
                exxoElement.RecalculateChildrenSelf();
            }
            else
            {
                element.Recalculate();
            }
        }
    }

    /// <summary>
    ///     This works because the UIChanges.ILUIElementRecalculate hook doesn't recalulate children if the element is an
    ///     ExxoUIElement
    /// </summary>
    public virtual void RecalculateSelf() => base.Recalculate();

    protected virtual void UpdateSelf(GameTime gameTime)
    {
    }

    protected virtual void PreRecalculate()
    {
    }

    protected virtual void PostRecalculate()
    {
    }

    protected virtual void FirstMouseOver(UIMouseEvent evt) => OnFirstMouseOver?.Invoke(evt, this);

    protected virtual void LastMouseOut(UIMouseEvent evt) => OnLastMouseOut?.Invoke(evt, this);

    private void RecalculateFinish()
    {
        foreach (UIElement element in Elements)
        {
            if (element is ExxoUIElement exxoElement)
            {
                exxoElement.RecalculateFinish();
            }
        }

        OnRecalculateFinish?.Invoke(this, EventArgs.Empty);
    }
}
