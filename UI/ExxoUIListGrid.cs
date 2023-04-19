using System.Collections.Generic;
using System.Linq;
using Terraria.UI;

namespace Avalon.UI;

public class ExxoUIListGrid : ExxoUIList {
    private readonly int amountPerInnerList;
    private readonly List<(UIElement element, ElementParams elementParams)> gridItems = new();

    public ExxoUIListGrid(int amountPerInnerList = -1) {
        this.amountPerInnerList = amountPerInnerList;
        Direction = Direction.Vertical;
        FitHeightToContent = true;
    }

    /// <inheritdoc />
    public override void RecalculateChildren() {
        base.Clear();
        AddNewInnerList();
        foreach ((UIElement element, ElementParams elementParams) item in gridItems) {
            DealWithItem(item.element, item.elementParams);
        }

        base.RecalculateChildren();
    }

    /// <inheritdoc />
    public override void Clear() {
        base.Clear();
        gridItems.Clear();
    }

    public new void Append(UIElement item) {
        gridItems.Add((item, new ElementParams()));
    }

    public new void Append(UIElement item, ElementParams elementParams) {
        gridItems.Add((item, elementParams));
    }

    private void DealWithItem(UIElement element, ElementParams elementParams) {
        var lastElement = (Elements.Last() as ExxoUIList)!;

        if (amountPerInnerList == -1) {
            if (lastElement.MinWidth.Pixels + element.MinWidth.Pixels + ListPadding >
                GetInnerDimensions().Width) {
                lastElement = AddNewInnerList();
            }
        }
        else if (lastElement.ElementCount >= amountPerInnerList) {
            lastElement = AddNewInnerList();
        }

        lastElement.Append(element, elementParams);
        lastElement.Recalculate();
    }

    private ExxoUIList AddNewInnerList() {
        var list = new ExxoUIList {
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
