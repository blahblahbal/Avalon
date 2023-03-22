using System.Collections.Generic;
using System.Linq;
using Terraria.UI;

namespace Avalon.UI;

public class ExxoUIListGrid : ExxoUIList
{
    private readonly int amountPerInnerList;
    private readonly List<UIElement> gridElements = new();

    public ExxoUIListGrid(int amountPerInnerList = -1)
    {
        this.amountPerInnerList = amountPerInnerList;
        Direction = Direction.Vertical;
        FitHeightToContent = true;
    }

    /// <inheritdoc />
    public override void RecalculateChildren()
    {
        RemoveAllChildren();
        AddNewInnerList();
        foreach (UIElement element in gridElements)
        {
            DealWithItem(element);
        }

        base.RecalculateChildren();
    }

    /// <inheritdoc />
    public override void Clear()
    {
        base.Clear();
        gridElements.Clear();
    }

    public new void Append(UIElement item) => gridElements.Add(item);

    private void DealWithItem(UIElement item)
    {
        var lastElement = (Elements.Last() as ExxoUIList)!;

        if (amountPerInnerList == -1)
        {
            if (lastElement.MinWidth.Pixels + item.MinWidth.Pixels + ListPadding >
                GetInnerDimensions().Width)
            {
                lastElement = AddNewInnerList();
            }
        }
        else if (lastElement.ElementCount >= amountPerInnerList)
        {
            lastElement = AddNewInnerList();
        }

        lastElement.Append(item);
        lastElement.Recalculate();
    }

    private ExxoUIList AddNewInnerList()
    {
        var list = new ExxoUIList
        {
            Direction = Direction.Horizontal,
            FitHeightToContent = true,
            FitWidthToContent = true,
            Width = StyleDimension.Fill,
            ListPadding = ListPadding,
        };
        base.Append(list);
        return list;
    }
}
