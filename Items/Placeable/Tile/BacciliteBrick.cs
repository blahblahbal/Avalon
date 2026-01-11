using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Avalon.Items.Material.Ores;

namespace Avalon.Items.Placeable.Tile;
public class BacciliteBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.DemoniteBrick);
		Item.createTile = ModContent.TileType<Tiles.Contagion.BacciliteBrick>();
	}
	public override void AddRecipes()
	{
		CreateRecipe(5)
			.AddIngredient(ModContent.ItemType<BacciliteOre>())
			.AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 5)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
