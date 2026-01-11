using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.ChunkstoneBrickWall;
[LegacyName("ChunkstoneBrickWall")]
public class ChunkstoneBrickWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<ChunkstoneBrickWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<ChunkstoneBrick>()).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<ChunkstoneBrick>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}
public class ChunkstoneBrickWall : ModWall
{
	public override void SetStaticDefaults()
	{
		Main.wallHouse[Type] = true;
		//ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ChunkstoneBrickWall>();
		AddMapEntry(new Color(55, 73, 50));
		DustType = ModContent.DustType<Dusts.ChunkstoneBrickDust>();
	}
}
