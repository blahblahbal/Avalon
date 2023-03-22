using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace Avalon.UI;

public class ExxoUIText : ExxoUIAdapter<UIText>
{
    public ExxoUIText(string text, float textScale = 1f, bool large = false) : this(new UIText(text, textScale, large))
    {
    }

    public ExxoUIText(LocalizedText text, float textScale = 1f, bool large = false) : this(new UIText(text, textScale,
        large))
    {
    }

    protected ExxoUIText(UIText childBase) : base(childBase)
    {
        ChildBase.OnInternalTextChange += () =>
        {
            MinWidth = ChildBase.MinWidth;
            MinHeight = ChildBase.MinHeight;
            OnInternalTextChange?.Invoke(this, EventArgs.Empty);
        };
        MinWidth = ChildBase.MinWidth;
        MinHeight = ChildBase.MinHeight;
    }

    public event EventHandler<EventArgs>? OnInternalTextChange;

    public string Text => ChildBase.Text;

    public float TextOriginX { get => ChildBase.TextOriginX; set => ChildBase.TextOriginX = value; }

    public float TextOriginY { get => ChildBase.TextOriginY; set => ChildBase.TextOriginY = value; }

    public float WrappedTextBottomPadding
    {
        get => ChildBase.WrappedTextBottomPadding;
        set => ChildBase.WrappedTextBottomPadding = value;
    }

    public bool IsWrapped
    {
        get => ChildBase.IsWrapped;
        set => ChildBase.IsWrapped = value;
    }

    public Color TextColor
    {
        get => ChildBase.TextColor;
        set => ChildBase.TextColor = value;
    }

    public bool DynamicallyScaleDownToWidth
    {
        get => ChildBase.DynamicallyScaleDownToWidth;
        set => ChildBase.DynamicallyScaleDownToWidth = value;
    }

    public void SetText(string text) => ChildBase.SetText(text);

    public void SetText(LocalizedText text) => ChildBase.SetText(text);

    public void SetText(string text, float textScale, bool large) => ChildBase.SetText(text, textScale, large);

    public void SetText(LocalizedText text, float textScale, bool large) => ChildBase.SetText(text, textScale, large);
}
