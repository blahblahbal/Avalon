using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class ChunkstoneBrick : ModItem
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
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.createTile = ModContent.TileType<Tiles.ChunkstoneBrick>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<ChunkstoneBlock>()).AddIngredient(ItemID.StoneBlock).AddTile(TileID.Furnaces).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<Items.Placeable.Wall.ChunkstoneBrickWall>(), 4).AddTile(TileID.WorkBenches).Register();
    //}
}
