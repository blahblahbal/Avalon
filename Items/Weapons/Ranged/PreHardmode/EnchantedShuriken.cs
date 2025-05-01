using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class EnchantedShuriken : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<Projectiles.Ranged.EnchantedShuriken>(), 15, 0f, 10.5f, 16, false, false);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 1, 50);
	}
	//float real;
	//public override bool GrabStyle(Player player)
	//{
	//	real += 0.3f;
	//	//if (Main.rand.NextBool(3))
	//	//{
	//	//    Dust d = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(Item.Hitbox), DustID.MagicMirror);
	//	//    d.velocity *= 0.1f;
	//	//}
	//	Item.velocity = new Vector2(real, 0).RotatedBy(Item.position.AngleTo(player.Center));
	//	return true;
	//}
	//public override void GrabRange(Player player, ref int grabRange)
	//{
	//    if (Item.useLimitPerAnimation != null && Item.useLimitPerAnimation == player.whoAmI)
	//    {
	//        grabRange += (int)(300 * (1 + real));
	//    }
	//}

	//public override bool OnPickup(Player player)
	//{
	//    Item.useLimitPerAnimation = 1;
	//    return base.OnPickup(player);
	//}
	public override void AddRecipes()
	{
		Recipe.Create(Type, 1)
			.AddIngredient(ModContent.ItemType<EnchantedBar>(), 5)
			.AddTile(TileID.Anvils).Register();
	}
}
