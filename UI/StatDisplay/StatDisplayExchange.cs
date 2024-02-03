using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.StatDisplay;

internal class StatDisplayExchange : ExxoUIPanelWrapper<ExxoUIList>
{
    public StatDisplayExchange() : base(new ExxoUIList())
    {
        Height.Set(0, 1);
        InnerElement.Direction = Direction.Horizontal;

        var list = new ExxoUIList();
        list.Height.Set(0, 1);
        InnerElement.Append(list, new ExxoUIList.ElementParams(true, false));

        #region potions
        var potionExchangeTitleContainer = new ExxoUIList { FitHeightToContent = true, ContentVAlign = UIAlign.Center, ContentHAlign = 0.3f };
        potionExchangeTitleContainer.Width.Set(0, 1);
        potionExchangeTitleContainer.Direction = Direction.Horizontal;
        potionExchangeTitleContainer.Justification = Justification.Center;
        list.Append(potionExchangeTitleContainer);

        var titlePotions = new ExxoUIText("Potion Exchange");
        potionExchangeTitleContainer.Append(titlePotions);

        Toggle = new ExxoUIImageButtonToggle(
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"), Color.White,
            Color.Orange)
        { Tooltip = "Toggle Potions/Elixirs" };
        potionExchangeTitleContainer.Append(Toggle);
        potionExchangeTitleContainer.Hidden = true;
        #endregion

        #region herbs
        var herbExchangeTitleContainer = new ExxoUIList { FitHeightToContent = true, ContentVAlign = UIAlign.Center, ContentHAlign = 0.3f };
        herbExchangeTitleContainer.Width.Set(0, 1);
        herbExchangeTitleContainer.Direction = Direction.Horizontal;
        herbExchangeTitleContainer.Justification = Justification.Center;
        list.Append(herbExchangeTitleContainer);

        var titleHerbs = new ExxoUIText("Herb Exchange");
        herbExchangeTitleContainer.Append(titleHerbs);

        Toggle = new ExxoUIImageButtonToggle(
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed"), Color.Red, Color.White)
        {
            Tooltip = "Toggle Seeds/Large Seeds",
        };
        herbExchangeTitleContainer.Append(Toggle);
        #endregion

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
