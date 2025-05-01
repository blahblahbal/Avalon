using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class VertexOfExcalibur : ModItem
{
	public override bool MeleePrefix()
	{
		return true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<VertexSlash>(), 130, 5f, 16f, 18, 18, shootsEveryUse: true, noMelee: true, width: 54, height: 54);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 9, 63);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{

		float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
		Projectile.NewProjectile(source, player.MountedCenter, velocity, type, damage, knockback, player.whoAmI, (float)player.direction * player.gravDir * 0.08f, 40, adjustedItemScale5 * 1.3f);
		Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, 0, knockback, player.whoAmI, (float)player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale5 * 1.4f);
		NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.TrueNightsEdge)
			.AddIngredient(ItemID.TrueExcalibur)
			.AddIngredient(ItemID.BrokenHeroSword)
			.AddIngredient(ItemID.DarkShard)
			.AddIngredient(ItemID.LightShard)
			.AddIngredient(ItemID.BeetleHusk, 4)
			.AddTile(TileID.AdamantiteForge).Register();
	}
}
