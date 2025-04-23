using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class NastySpike : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.NastySpike>());
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 1);
		Item.notAmmo = true;
		Item.ammo = ItemID.Spike;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ItemID.Spike).AddIngredient(ModContent.ItemType<Material.Booger>()).AddTile(TileID.Anvils).Register();
	}
}
