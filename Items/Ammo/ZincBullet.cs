using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class ZincBullet : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToBullet(11, ModContent.ProjectileType<Projectiles.Ranged.UltrabrightRazorbladeBullet>(), 3.75f, 3f);
		Item.value = Item.sellPrice(0, 0, 0, 3);
	}
	public override void AddRecipes()
	{
		CreateRecipe(70).AddIngredient(ItemID.MusketBall, 70).AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 1).AddTile(TileID.Anvils).Register();
	}
}
