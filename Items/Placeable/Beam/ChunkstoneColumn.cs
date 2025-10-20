using Avalon.Tiles.Contagion.Chunkstone;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Beam;

public class ChunkstoneColumn : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 50;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ChunkstoneColumn>());
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type, 2)
			.AddIngredient(ModContent.ItemType<ChunkstoneBlock>())
			.AddTile(TileID.HeavyWorkBench).Register();
	}
}
