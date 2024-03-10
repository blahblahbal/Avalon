using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Avalon.Items.Material.Herbs;

namespace Avalon.Items.Placeable.Crafting;

class CrimsonAltar : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.EvilAltarsPlaced>();
        Item.placeStyle = 1;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<CrimsonAltar>())
            .AddIngredient(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ItemID.Vertebrae, 10)
            .AddIngredient(ModContent.ItemType<Bloodberry>(), 5)
            .AddIngredient(ItemID.SoulofNight, 20)
            .AddIngredient(ItemID.TissueSample, 20)
            .AddIngredient(ItemID.CrimtaneBar, 25)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
