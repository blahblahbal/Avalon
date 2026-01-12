using Avalon.Items.Material;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class VoltBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Bricks.VoltBrick>());
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Cloud)
			.AddIngredient(ModContent.ItemType<LivingLightningBlock>())
			.AddTile(TileID.Hellforge)
			.Register();
	}
}
