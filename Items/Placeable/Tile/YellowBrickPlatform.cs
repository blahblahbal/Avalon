using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class YellowBrickPlatform : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.YellowDungeon.YellowBrickPlatform > ();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 300;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
	
    public override void AddRecipes()
    {
        CreateRecipe(2)
            .AddIngredient(ModContent.ItemType<Tile.YellowBrick>())
            .Register();

        Recipe.Create(ModContent.ItemType<Tile.YellowBrick>())
            .AddIngredient(this, 2)
            .DisableDecraft()
            .Register();
    }
}
