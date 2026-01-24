using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class UltrabrightRazorbladeBullet : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 500;
	}

	public override void SetDefaults()
	{
		Item.DefaultToBullet(17, ModContent.ProjectileType<Projectiles.Ranged.Ammo.UltrabrightRazorbladeBullet>(), 10f, 3.5f);
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 0, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe(1000)
			.AddIngredient(ItemID.MusketBall, 1000)
			.AddIngredient(ItemID.UltrabrightTorch, 250)
			.AddIngredient(ItemID.RazorbladeTyphoon)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
