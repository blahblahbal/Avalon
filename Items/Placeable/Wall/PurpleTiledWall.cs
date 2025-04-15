using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class PurpleTiledWall : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Item.Size = new Vector2(12);
        Item.autoReuse = true;
        Item.consumable = true;
        Item.useTurn = true;
        Item.useTime = 7;
        Item.createWall = ModContent.WallType<Walls.PurpleTiledWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.PurpleBrick>())
			.AddTile(TileID.HeavyWorkBench)
            .DisableDecraft()
            .Register();
			
        Recipe.Create(ModContent.ItemType<Tile.PurpleBrick>())
			.AddIngredient(this, 4)
			.AddTile(TileID.HeavyWorkBench)
            .DisableDecraft()
            .Register();
    }
}
