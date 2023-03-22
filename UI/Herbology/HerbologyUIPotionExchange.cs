using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUIPotionExchange : ExxoUIPanelWrapper<ExxoUIList>
{
    public HerbologyUIPotionExchange() : base(new ExxoUIList())
    {
        Height.Set(0, 1);
        InnerElement.Direction = Direction.Horizontal;

        var list = new ExxoUIList();
        list.Height.Set(0, 1);
        InnerElement.Append(list, new ExxoUIList.ElementParams(true, false));

        var herbExchangeTitleContainer = new ExxoUIList { FitHeightToContent = true, ContentVAlign = UIAlign.Center };
        herbExchangeTitleContainer.Width.Set(0, 1);
        herbExchangeTitleContainer.Direction = Direction.Horizontal;
        herbExchangeTitleContainer.Justification = Justification.Center;
        list.Append(herbExchangeTitleContainer);

        var title = new ExxoUIText("Potion Exchange");
        herbExchangeTitleContainer.Append(title);

        Toggle = new ExxoUIImageButtonToggle(
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"), Color.White,
            Color.Orange) { Tooltip = "Toggle Potions/Elixirs" };
        herbExchangeTitleContainer.Append(Toggle);

        var horizontalRule = new ExxoUIHorizontalRule();
        horizontalRule.Width.Set(0, 1);
        list.Append(horizontalRule);

        var gridWrapper = new ExxoUIEmpty { OverflowHidden = true };
        gridWrapper.Width = StyleDimension.Fill;
        list.Append(gridWrapper, new ExxoUIList.ElementParams(true, false));
        Grid = new ExxoUIListGrid { HAlign = UIAlign.Center, FitWidthToContent = true };
        gridWrapper.Append(Grid);

        Scrollbar = new ExxoUIScrollbar();
        Scrollbar.VAlign = UIAlign.Center;
        Scrollbar.Height = StyleDimension.Fill;
        Scrollbar.SetPadding(0);
        InnerElement.Append(Scrollbar);
        Grid.ScrollBar = Scrollbar;
        OnScrollWheel += Grid.ScrollWheelListener;
    }

    public ExxoUIListGrid Grid { get; }
    public ExxoUIScrollbar Scrollbar { get; }
    public ExxoUIImageButtonToggle Toggle { get; }
}
