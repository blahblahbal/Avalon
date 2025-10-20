using Avalon.Tiles.Contagion.Chunkstone;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class ContagionMouldWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.ContagionMouldWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<ChunkstoneBlock>()).AddCondition(Condition.InGraveyard).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<ChunkstoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
