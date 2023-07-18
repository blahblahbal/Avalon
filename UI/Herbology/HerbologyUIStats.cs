using System.Globalization;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUIStats : ExxoUIPanelWrapper<ExxoUIList>
{
    private readonly ExxoUIText herbTotalText;
    private readonly ExxoUIText potionTotalText;

    public HerbologyUIStats() : base(new ExxoUIList())
    {
        Height.Set(0, 1);

        InnerElement.FitWidthToContent = true;
        InnerElement.Justification = Justification.Center;
        InnerElement.ContentHAlign = UIAlign.Center;

        RankTitleText = new ExxoUITextPanel("");
        RankTitleText.TextElement.TextColor = Color.Gold;
        InnerElement.Append(RankTitleText);

        HerbTierText = new ExxoUITextPanel("");
        InnerElement.Append(HerbTierText);

        HerbTotalContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList()) { Tooltip = Language.GetTextValue("Mods.Avalon.Herbology.Credits.Herb") };
        HerbTotalContainer.InnerElement.Direction = Direction.Horizontal;
        HerbTotalContainer.InnerElement.FitHeightToContent = true;
        HerbTotalContainer.InnerElement.FitWidthToContent = true;
        HerbTotalContainer.InnerElement.ContentVAlign = UIAlign.Center;
        InnerElement.Append(HerbTotalContainer);

        var herbTotalIcon =
            new ExxoUIImage(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed"))
            {
                Inset = new Vector2(7, 7),
            };
        HerbTotalContainer.InnerElement.Append(herbTotalIcon);

        herbTotalText = new ExxoUIText("");
        HerbTotalContainer.InnerElement.Append(herbTotalText);

        PotionTotalContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList()) { Tooltip = Language.GetTextValue("Mods.Avalon.Herbology.Credits.Potion") };
        PotionTotalContainer.InnerElement.Direction = Direction.Horizontal;
        PotionTotalContainer.InnerElement.FitHeightToContent = true;
        PotionTotalContainer.InnerElement.FitWidthToContent = true;
        PotionTotalContainer.InnerElement.ContentVAlign = UIAlign.Center;
        InnerElement.Append(PotionTotalContainer);

        var potionTotalIcon =
            new ExxoUIImage(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"))
            {
                Inset = new Vector2(4, 5),
            };
        PotionTotalContainer.InnerElement.Append(potionTotalIcon);

        potionTotalText = new ExxoUIText("");
        PotionTotalContainer.InnerElement.Append(potionTotalText);
    }

    public ExxoUITextPanel HerbTierText { get; }
    public ExxoUIPanelWrapper<ExxoUIList> HerbTotalContainer { get; }
    public ExxoUIPanelWrapper<ExxoUIList> PotionTotalContainer { get; }
    public ExxoUITextPanel RankTitleText { get; }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);

        Player player = Main.LocalPlayer;
        AvalonHerbologyPlayer modPlayer = player.GetModPlayer<AvalonHerbologyPlayer>();

        string rankTitle = $"Herbology {modPlayer.Tier}";
        RankTitleText.TextElement.SetText(rankTitle);

        string tier = $"Tier {(int)modPlayer.Tier + 1} Herbologist";
        HerbTierText.TextElement.SetText(tier);

        string herbTotal = modPlayer.HerbTotal.ToString(CultureInfo.CurrentCulture);
        herbTotalText.SetText(herbTotal);

        string potionTotal = modPlayer.PotionTotal.ToString(CultureInfo.CurrentCulture);
        potionTotalText.SetText(potionTotal);
    }
}
