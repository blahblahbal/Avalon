using System;
using Avalon.Logic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;

namespace Avalon.UI;

public class ExxoUIScrollbar : ExxoUIAdapter<UIScrollbar>
{
    private readonly Observer<float> observer;

    public ExxoUIScrollbar() : base(new UIScrollbar())
    {
        observer = new Observer<float>(() => ViewPosition);
        Width.Set(20f, 0.0f);
        MaxWidth.Set(20f, 0.0f);
    }

    public bool CanScroll => ChildBase.CanScroll;
    public EventHandler? OnViewPositionChanged { get; set; }

    public float ViewPosition
    {
        get => ChildBase.ViewPosition;
        set => ChildBase.ViewPosition = value;
    }

    public void GoToBottom() => ChildBase.GoToBottom();

    public void SetView(float viewSize, float maxViewSize)
    {
        ChildBase.SetView(viewSize, maxViewSize);
        Hidden = maxViewSize - viewSize <= 0;
    }

    public float GetValue() => ChildBase.GetValue();

    /// <inheritdoc />
    protected override void UpdateSelf(GameTime gameTime)
    {
        if (observer.Check())
        {
            OnViewPositionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
