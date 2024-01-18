using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

internal class CloudGlove : ModItem
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
