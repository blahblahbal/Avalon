using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Avalon.UI;

public class ExxoUIList : ExxoUIElement
{
    private StyleDimension origHeight;
    private StyleDimension origWidth;

    public delegate bool ElementSearchMethod(UIElement element);

    public override bool IsDynamicallySized => FitHeightToContent || FitWidthToContent;
    public float ContentHAlign { get; set; }
    public float ContentVAlign { get; set; }
    public Direction Direction { get; set; } = Direction.Vertical;
    public bool FitHeightToContent { get; set; }
    public bool FitWidthToContent { get; set; }

    public Justification Justification { get; set; } = Justification.Start;

    public float ListPadding { get; set; } = 5f;

    public ExxoUIScrollbar? ScrollBar { get; set; }
    public float TotalLength { get; set; }

    protected List<ElementParams> ElementParamsList { get; } = new();

    public override void RecalculateChildren()
    {
        float largestOppLength = 0;
        float total = 0;
        int fillLengthCount = 0;

        for (int i = 0; i < Elements.Count; i++)
        {
            Elements[i].MarginBottom = 0;
            Elements[i].MarginRight = 0;
            if (Elements[i] is ExxoUIElement exxoElement)
            {
                if (exxoElement.Hidden || !exxoElement.Active)
                {
                    continue;
                }

                if (exxoElement.IsDynamicallySized)
                {
                    exxoElement.Recalculate();
                }
            }

            if (!ElementParamsList[i].IgnoreContentAlign)
            {
                Elements[i].VAlign = ContentVAlign;
                Elements[i].HAlign = ContentHAlign;
            }

            if (ElementParamsList[i].FillLength)
            {
                fillLengthCount++;
            }

            float width = Math.Max(Elements[i].MinWidth.Pixels, Elements[i].Width.Pixels);
            float height = Math.Max(Elements[i].MinHeight.Pixels, Elements[i].Height.Pixels);

            if (!ElementParamsList[i].FillLength)
            {
                float length = Direction == Direction.Vertical ? height : width;
                total += length;
            }

            float oppLength = Direction == Direction.Vertical ? width : height;
            largestOppLength = Math.Max(oppLength, largestOppLength);
        }

        float padding = ListPadding;

        // List padding
        if (Justification == Justification.SpaceBetween)
        {
            padding = 0;
            if (Elements.Count > 1)
            {
                for (int i = 0; i < Elements.Count; i++)
                {
                    Elements[i].HAlign = i / (float)(Elements.Count - 1);
                }
            }
        }

        for (int i = 0; i < Elements.Count - 1; i++)
        {
            if (Elements[i] is ExxoUIElement exxoElement && (exxoElement.Hidden || !exxoElement.Active))
            {
                continue;
            }

            float width = Math.Max(Elements[i + 1].MinWidth.Pixels, Elements[i + 1].Width.Pixels);
            float height = Math.Max(Elements[i + 1].MinHeight.Pixels, Elements[i + 1].Height.Pixels);

            var nextExxoElement = Elements[i + 1] as ExxoUIElement;
            if (nextExxoElement != null && (nextExxoElement.Hidden || !nextExxoElement.Active))
            {
                continue;
            }

            if (nextExxoElement == null && height <= 0 && width <= 0)
            {
                continue;
            }

            if (Direction == Direction.Vertical)
            {
                Elements[i].MarginBottom = padding;
            }
            else
            {
                Elements[i].MarginRight = padding;
            }

            total += padding;
            if (Justification == Justification.SpaceBetween)
            {
                total += ListPadding;
            }
        }

        // Set largest opposite length
        if (FitHeightToContent && Direction == Direction.Horizontal)
        {
            MinHeight.Set(largestOppLength, 0);
            Height = origHeight;
        }

        if (FitWidthToContent && Direction == Direction.Vertical)
        {
            MinWidth.Set(largestOppLength, 0);
            Width = origWidth;
        }

        // Set direction length
        if (FitHeightToContent && Direction == Direction.Vertical)
        {
            MinHeight.Set(total, 0);
            Height = origHeight;
        }

        if (FitWidthToContent && Direction == Direction.Horizontal)
        {
            MinWidth.Set(total, 0);
            Width = origWidth;
        }

        TotalLength = total;

        RecalculateSelf();

        float innerLength = Direction == Direction.Vertical ? GetInnerDimensions().Height : GetInnerDimensions().Width;
        float fillLength = (innerLength - total) / Math.Max(1, fillLengthCount);
        float offset = 0f;

        if (Justification == Justification.Center && !((FitWidthToContent && Direction == Direction.Horizontal) ||
                                                       (FitHeightToContent && Direction == Direction.Vertical)))
        {
            offset = (innerLength / 2) - (total / 2);
        }

        for (int i = 0; i < Elements.Count; i++)
        {
            if (Elements[i] is ExxoUIElement exxoElement &&
                (exxoElement.Hidden || !exxoElement.Active))
            {
                continue;
            }

            if (ElementParamsList[i].FillLength)
            {
                if (Direction == Direction.Vertical)
                {
                    Elements[i].Height.Set(fillLength, 0);
                }
                else
                {
                    Elements[i].Width.Set(fillLength, 0);
                }
            }

            float width = Math.Max(Elements[i].MinWidth.Pixels, Elements[i].Width.Pixels);
            float height = Math.Max(Elements[i].MinHeight.Pixels, Elements[i].Height.Pixels);

            float outerLength = Direction == Direction.Vertical
                ? height + Elements[i].MarginBottom
                : width + Elements[i].MarginRight;
            float pixels;

            switch (Justification)
            {
                case Justification.Start:
                case Justification.Center:
                    pixels = offset;
                    offset += outerLength;
                    break;
                case Justification.End:
                    offset += outerLength;
                    pixels = innerLength - offset;
                    break;
                default:
                    pixels = 0;
                    break;
            }

            if (Direction == Direction.Vertical)
            {
                Elements[i].Top.Set(pixels, 0);
            }
            else
            {
                Elements[i].Left.Set(pixels, 0);
            }
        }

        base.RecalculateChildren();
    }

    public override bool ContainsPoint(Vector2 point)
    {
        foreach (UIElement uIElement in Elements)
        {
            if (uIElement.ContainsPoint(point))
            {
                return true;
            }
        }

        return false;
    }

    public void ScrollTo(ElementSearchMethod searchMethod)
    {
        if (ScrollBar == null)
        {
            return;
        }

        int num;
        for (num = 0; num < Elements.Count; num++)
        {
            if (searchMethod(Elements[num]))
            {
                break;
            }
        }

        ScrollBar.ViewPosition = Elements[num].Top.Pixels;
    }

    public new void Append(UIElement item) => Append(item, new ElementParams(false, false));

    public void Append(UIElement item, ElementParams elementParams)
    {
        ElementParamsList.Add(elementParams);
        base.Append(item);
    }

    public virtual void AddRange(IEnumerable<UIElement> items)
    {
        foreach (UIElement item in items)
        {
            base.Append(item);
            ElementParamsList.Add(new ElementParams());
        }
    }

    public virtual void Remove(UIElement item)
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            if (Elements[i] == item)
            {
                ElementParamsList.RemoveAt(i);
                break;
            }
        }

        RemoveChild(item);
    }

    public virtual void Clear()
    {
        ElementParamsList.Clear();
        Elements.Clear();
    }

    public void ScrollWheelListener(UIScrollWheelEvent evt, UIElement _)
    {
        if (ScrollBar != null)
        {
            ScrollBar.ViewPosition -= evt.ScrollWheelValue;
        }
    }

    protected override void PreRecalculate()
    {
        base.PreRecalculate();
        if (FitHeightToContent)
        {
            MinHeight.Set(0, 0);
            origHeight = Height;
            Height.Set(0, 1);
        }

        if (FitWidthToContent)
        {
            MinWidth.Set(0, 0);
            origWidth = Width;
            Width.Set(0, 1);
        }
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        if (ScrollBar != null)
        {
            Top.Set(-ScrollBar.GetValue(), 0f);
        }
    }

    protected override void PostRecalculate()
    {
        base.PostRecalculate();
        UpdateScrollbar();
    }

    private void UpdateScrollbar()
    {
        if (Parent != null && ScrollBar != null)
        {
            ScrollBar.SetView(Parent.GetInnerDimensions().Height, GetInnerDimensions().Height);
        }
    }

    public struct ElementParams
    {
        public ElementParams(bool fillLength, bool ignoreContentAlign)
        {
            FillLength = fillLength;
            IgnoreContentAlign = ignoreContentAlign;
        }

        public bool FillLength { get; }
        public bool IgnoreContentAlign { get; }
    }
}
