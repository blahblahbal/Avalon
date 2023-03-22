using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUIPanelButton<T> : ExxoUIPanelWrapper<T> where T : UIElement
{
    private readonly Color inactiveColor;
    private bool mouseWasOver;
    private float visibilityActive = 1f;
    private float visibilityInactive = 0.4f;

    public ExxoUIPanelButton(T uiElement) : base(uiElement) =>
        inactiveColor = BackgroundColor;

    public override void MouseOver(UIMouseEvent evt)
    {
        if (!mouseWasOver)
        {
            mouseWasOver = true;
            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        base.MouseOver(evt);
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        base.MouseOut(evt);

        if (!ContainsPoint(evt.MousePosition))
        {
            mouseWasOver = false;
        }
    }

    public void SetVisibility(float whenActive, float whenInactive)
    {
        visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
        visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        if (IsMouseHovering)
        {
            BackgroundColor = inactiveColor * visibilityActive;
        }
        else
        {
            BackgroundColor = inactiveColor * visibilityInactive;
        }
    }
}
