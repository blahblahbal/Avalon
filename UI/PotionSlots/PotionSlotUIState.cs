using Avalon.Data;
using Avalon.Items.Potions.Buff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI.PotionSlots;

public class PotionSlotUIState : ExxoUIState
{
    private ExxoUIPanel? mainPanel;
    private PotionsSlots? slots;

    public override void OnInitialize()
    {
        base.OnInitialize();

        mainPanel = new ExxoUIPanel
        {
            Width = StyleDimension.FromPixels(200),
            Height = StyleDimension.FromPixels(214),
            Top = StyleDimension.FromPixels(95),
            Left = StyleDimension.FromPixels(550),
            VAlign = UIAlign.Top,
            HAlign = UIAlign.Left,
            BackgroundColor = Color.Transparent,
            BorderColor = Color.Transparent
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
            ContentVAlign = UIAlign.Center
        };
        mainContainer.Append(titleRow);
        var titleText = new ExxoUIText(Language.GetTextValue("Mods.Avalon.Potions"), 0.25f, true);
        titleRow.Append(titleText);

        slots = new PotionsSlots();
        mainContainer.Append(slots);
    }
}
