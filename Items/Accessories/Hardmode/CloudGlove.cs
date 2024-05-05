using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class CloudGlove : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().CloudGlove = true;
    }

    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().CloudGlove = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Silk, 15)
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ModContent.ItemType<TornadoShard>(), 3)
            .AddRecipeGroup("Avalon:GoldBar", 5)
            .AddIngredient(ItemID.SunplateBlock, 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Silk, 15)
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ModContent.ItemType<TornadoShard>(), 3)
            .AddRecipeGroup("Avalon:GoldBar", 5)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.MoonplateBlock>(), 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Silk, 15)
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ModContent.ItemType<TornadoShard>(), 3)
            .AddRecipeGroup("Avalon:GoldBar", 5)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.DuskplateBlock>(), 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
public class CloudGloveBuilderToggle : BuilderToggle
{
    public static LocalizedText OnText { get; private set; }
    public static LocalizedText OffText { get; private set; }

    public override bool Active()
    {
        return Main.LocalPlayer.HasItemInFunctionalAccessories(ItemID.HandOfCreation) ||
            Main.LocalPlayer.HasItemInArmor(ModContent.ItemType<CloudGlove>()) ||
            Main.LocalPlayer.HasItemInArmor(ModContent.ItemType<ObsidianGlove>());
    }
    public override int NumberOfStates => 2;
    public override string Texture => "Avalon/Assets/Textures/UI/CloudGloveToggle";

    public override void SetStaticDefaults()
    {
        OnText = this.GetLocalization(nameof(OnText));
        OffText = this.GetLocalization(nameof(OffText));
    }
    public override string DisplayValue()
    {
        return CurrentState == 0 ? OnText.Value : OffText.Value;
    }
    public override Color DisplayColorTexture()/* tModPorter Note: Removed. Use BuilderToggle.Draw */
    {
        return CurrentState == 0 ? Color.White : new Color(100, 100, 100);
    }
}
