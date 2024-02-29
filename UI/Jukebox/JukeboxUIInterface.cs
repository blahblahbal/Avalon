using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace Avalon.UI.Jukebox;

internal class JukeboxUIInterface : ExxoUIPanelWrapper<ExxoUIList>
{
    public static string TrackText = Language.GetTextValue("Mods.Avalon.Jukebox.Tracks");
    ExxoUIText TitleText { get; } = new ExxoUIText(TrackText);
    public JukeboxUIInterface() : base(new ExxoUIList())
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

        //string text = Language.GetTextValue("Mods.Avalon.Jukebox.Tracks");
        //if ()

        TitleText.SetText(TrackText); //new ExxoUIText(Language.GetTextValue("Mods.Avalon.Jukebox.Tracks"));
        herbExchangeTitleContainer.Append(TitleText);

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
    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        TitleText.SetText(TrackText);
    }
    public ExxoUIListGrid Grid { get; }
    public ExxoUIScrollbar Scrollbar { get; }
}
