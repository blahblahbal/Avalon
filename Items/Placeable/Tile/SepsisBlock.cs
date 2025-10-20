using Avalon.Tiles.Contagion.Chunkstone;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class SepsisBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.SepsisBlock>());
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type, 1)
			.AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 2)
			.AddTile(TileID.MeatGrinder).Register();

		//Terraria.Recipe.Create(Type, 1)
		//    .AddIngredient(ModContent.ItemType<SepsisPlatform>(), 2)
		//    .AddTile(TileID.MeatGrinder).Register();
	}
}
