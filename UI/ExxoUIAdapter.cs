using Terraria.UI;

namespace Avalon.UI;

public abstract class ExxoUIAdapter<T> : ExxoUIElement where T : UIElement
{
    protected ExxoUIAdapter(T childBase)
    {
        ChildBase = childBase;
        childBase.Width = StyleDimension.Fill;
        childBase.Height = StyleDimension.Fill;

        PaddingTop = childBase.PaddingTop;
        PaddingLeft = childBase.PaddingLeft;
        PaddingRight = childBase.PaddingRight;
        PaddingBottom = childBase.PaddingBottom;

        Append(childBase);
    }

    /// <inheritdoc />
    public override bool IsDynamicallySized => false;

    protected T ChildBase { get; }

    /// <inheritdoc />
    public override void RecalculateChildren()
    {
        float oldPaddingTop = PaddingTop;
        float oldPaddingBottom = PaddingBottom;
        float oldPaddingRight = PaddingRight;
        float oldPaddingLeft = PaddingLeft;
        PaddingTop = 0;
        PaddingLeft = 0;
        PaddingRight = 0;
        PaddingBottom = 0;

        RecalculateSelf();
        ChildBase.Recalculate();

        PaddingTop = oldPaddingTop;
        PaddingLeft = oldPaddingLeft;
        PaddingRight = oldPaddingRight;
        PaddingBottom = oldPaddingBottom;

        RecalculateSelf();

        for (int i = 1; i < Elements.Count; i++)
        {
            Elements[i].Recalculate();
        }
    }

    /// <inheritdoc />
    public override void RecalculateChildrenSelf()
    {
        float oldPaddingTop = PaddingTop;
        float oldPaddingBottom = PaddingBottom;
        float oldPaddingRight = PaddingRight;
        float oldPaddingLeft = PaddingLeft;
        PaddingTop = 0;
        PaddingLeft = 0;
        PaddingRight = 0;
        PaddingBottom = 0;

        base.RecalculateSelf();
        ChildBase.Recalculate();

        PaddingTop = oldPaddingTop;
        PaddingLeft = oldPaddingLeft;
        PaddingRight = oldPaddingRight;
        PaddingBottom = oldPaddingBottom;

        base.RecalculateSelf();
        for (int i = 1; i < Elements.Count; i++)
        {
            if (Elements[i] is ExxoUIElement exxoElement)
            {
                exxoElement.RecalculateChildrenSelf();
            }
            else
            {
                Elements[i].Recalculate();
            }
        }
    }
}
