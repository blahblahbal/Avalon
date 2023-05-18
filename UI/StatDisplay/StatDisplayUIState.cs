using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Avalon.UI.StatDisplay;

public class StatDisplayUIState : ExxoUIState
{
    private ExxoUIDraggablePanel? mainPanel;
    private StatDisplayUIThing? statDisplay;

    public override void OnInitialize()
    {
        base.OnInitialize();

        Player p = Main.LocalPlayer;

        mainPanel = new ExxoUIDraggablePanel
        {
            Width = StyleDimension.FromPixels(720),
            Height = StyleDimension.FromPixels(660),
            VAlign = UIAlign.Center,
            HAlign = UIAlign.Center,
        };
        mainPanel.SetPadding(15);
        Append(mainPanel);

        var mainContainer = new ExxoUIList
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill,
            ContentHAlign = UIAlign.Center,
        };
        mainPanel.Append(mainContainer);

        var titleRow = new ExxoUIList
        {
            Width = StyleDimension.Fill,
            Direction = Direction.Horizontal,
            Justification = Justification.Center,
            FitHeightToContent = true,
            ContentVAlign = UIAlign.Center,
        };
        mainContainer.Append(titleRow);
        var titleText = new ExxoUITextPanel("Player Stats", 0.8f, true);
        titleRow.Append(titleText);

        var statsContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        statsContainer.Width.Set(0, 1);
        statsContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(statsContainer, new ExxoUIList.ElementParams(true, false));

        statDisplay = new StatDisplayUIThing();
        statsContainer.InnerElement.Append(statDisplay, new ExxoUIList.ElementParams(true, false));
    }

    public override void OnActivate()
    {
        base.OnActivate();
        SoundEngine.PlaySound(SoundID.MenuOpen);
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        SoundEngine.PlaySound(SoundID.MenuClose);
    }
}
