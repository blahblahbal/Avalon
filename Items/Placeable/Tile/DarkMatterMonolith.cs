using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Tile;

class DarkMatterMonolith : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.DarkMatter.DarkMatterMonolith>();
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 20);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<Material.DarkMatterGel>(), 100)
    //        .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 10)
    //        .AddIngredient(ModContent.ItemType<Bar.BerserkerBar>(), 5)
    //        .AddTile(TileID.Furnaces)
    //        .Register();
    //}
}
