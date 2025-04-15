using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class BloodyArrow : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToArrow(10, ModContent.ProjectileType<Projectiles.Ranged.BloodyArrow>(), 3.4f, 3f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 0, 8);
	}
	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient(ItemID.WoodenArrow, 5).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Anvils).Register();
	}
}
