using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class VenomSpike : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Hellcastle.VenomSpike>());
		Item.value = Item.sellPrice(copper: 10);
	}
	public override void AddRecipes()
	{
		CreateRecipe(40)
			.AddIngredient(ItemID.Spike, 40)
			.AddIngredient(ItemID.FlaskofVenom)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
