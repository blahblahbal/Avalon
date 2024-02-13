using System;
using System.Globalization;
using Avalon.Common.Players;
using Avalon.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using System.Linq;
using Terraria.GameContent;
using Terraria.Audio;

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
        RankTitleText.BackgroundColor *= 0.7f;
        InnerElement.Append(RankTitleText);

        HerbTierText = new ExxoUITextPanel("");
        HerbTierText.BackgroundColor = Color.Transparent;
        InnerElement.Append(HerbTierText);

        HerbTotalContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList()) { Tooltip = Language.GetTextValue("Mods.Avalon.Herbology.Credits.Herb") };
        HerbTotalContainer.InnerElement.Direction = Direction.Horizontal;
        HerbTotalContainer.InnerElement.FitHeightToContent = true;
        HerbTotalContainer.InnerElement.FitWidthToContent = true;
        HerbTotalContainer.InnerElement.ContentVAlign = UIAlign.Center;
        HerbTotalContainer.BackgroundColor = Color.Transparent;
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
        PotionTotalContainer.BackgroundColor = Color.Transparent;
        InnerElement.Append(PotionTotalContainer);

        var potionTotalIcon =
            new ExxoUIImage(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/UI/HerbPotion"))
            {
                Inset = new Vector2(4, 5),
            };
        PotionTotalContainer.InnerElement.Append(potionTotalIcon);

        potionTotalText = new ExxoUIText("");
        PotionTotalContainer.InnerElement.Append(potionTotalText);

        Button = new ExxoUIImageButton(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/UI/HerbButton"))
        {
            Tooltip = Language.GetTextValue("Mods.Avalon.Herbology.ConsumeHerbPotion"),
        };
        InnerElement.Append(Button);

        ItemSlot = new ExxoUIItemSlot(TextureAssets.InventoryBack7, ItemID.None);
        InnerElement.Append(ItemSlot);
        ItemSlot.OnLeftClick += delegate
        {
            if ((Main.mouseItem.type == ItemID.None && ItemSlot.Item.type != ItemID.None && ItemSlot.Item.stack > 0) ||
                (Main.mouseItem.stack >= 1 &&
                 (HerbologyData.LargeHerbIdByLargeHerbSeedId.ContainsValue(Main.mouseItem.type) ||
                  HerbologyData.LargeHerbIdByLargeHerbSeedId.ContainsKey(Main.mouseItem.type) ||
                  HerbologyData.HerbIdByLargeHerbId.ContainsValue(Main.mouseItem.type) ||
                  HerbologyData.PotionIds.Contains(Main.mouseItem.type) ||
                  HerbologyData.ElixirIds.Contains(Main.mouseItem.type))))
            {
                SoundEngine.PlaySound(SoundID.Grab);
                (Main.mouseItem, ItemSlot.Item) = (ItemSlot.Item, Main.mouseItem);
                Recipe.FindRecipes();
            }
        };
    }

    public ExxoUITextPanel HerbTierText { get; }
    public ExxoUIPanelWrapper<ExxoUIList> HerbTotalContainer { get; }
    public ExxoUIPanelWrapper<ExxoUIList> PotionTotalContainer { get; }
    public ExxoUITextPanel RankTitleText { get; }


    public ExxoUIImageButton Button { get; }
    public ExxoUIItemSlot ItemSlot { get; }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        Player player = Main.LocalPlayer;
        AvalonHerbologyPlayer modPlayer = player.GetModPlayer<AvalonHerbologyPlayer>();

        string herbTier = Language.GetTextValue("Mods.Avalon.Herbology.Tier0");

        if (modPlayer.Tier == AvalonHerbologyPlayer.HerbTier.Apprentice)
        {
            herbTier = Language.GetTextValue("Mods.Avalon.Herbology.Tier1");
        }
        else if (modPlayer.Tier == AvalonHerbologyPlayer.HerbTier.Expert)
        {
            herbTier = Language.GetTextValue("Mods.Avalon.Herbology.Tier2");
        }
        else if (modPlayer.Tier == AvalonHerbologyPlayer.HerbTier.Master)
        {
            herbTier = Language.GetTextValue("Mods.Avalon.Herbology.Tier3");
        }

        string rankTitle = Language.GetTextValue("Mods.Avalon.Herbology.Name") + herbTier;
        RankTitleText.TextElement.SetText(rankTitle);

        string tier = Language.GetTextValue("Mods.Avalon.Herbology.Tier") + $"{(int)modPlayer.Tier + 1}" + Language.GetTextValue("Mods.Avalon.Herbology.Herbologist");
        HerbTierText.TextElement.SetText(tier);

        string herbTotal = modPlayer.HerbTotal.ToString(CultureInfo.CurrentCulture);
        herbTotalText.SetText(herbTotal);

        string potionTotal = modPlayer.PotionTotal.ToString(CultureInfo.CurrentCulture);
        potionTotalText.SetText(potionTotal);

        Button.LocalScale = 1 + ((1 + (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)) * 0.15f);
        Button.LocalRotation = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds + 1) * 0.25f;
    }
}
