using Terraria.UI;

namespace Avalon.UI;

public abstract class ExxoUIPanelWrapper : ExxoUIPanel
{
    protected ExxoUIPanelWrapper(UIElement uiElement)
    {
        InnerElement = uiElement;
        InnerElement.Width = StyleDimension.Fill;
        InnerElement.Height = StyleDimension.Fill;
        Append(InnerElement);
    }

    /// <inheritdoc />
    public override bool IsDynamicallySized => true;

    protected UIElement InnerElement { get; }

    protected override void PostRecalculate()
    {
        MinWidth.Set(InnerElement.MinWidth.Pixels + PaddingLeft + PaddingRight, 0);
        MinHeight.Set(InnerElement.MinHeight.Pixels + PaddingBottom + PaddingTop, 0);
    }
}

public class ExxoUIPanelWrapper<T> : ExxoUIPanelWrapper where T : UIElement {
    public ExxoUIPanelWrapper(T uiElement) : base(uiElement) {}
    public new T InnerElement => (T)base.InnerElement;
}
