using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class PoisonSpike : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PoisonSpike>());
		Item.notAmmo = true;
		Item.ammo = ItemID.Spike;
	}
	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient(ItemID.Spike, 20).AddIngredient(ItemID.Stinger).AddTile(TileID.Anvils).Register();
		CreateRecipe(20).AddIngredient(ItemID.Spike, 20).AddIngredient(ModContent.ItemType<Material.MosquitoProboscis>()).AddTile(TileID.Anvils).Register();
	}
}
