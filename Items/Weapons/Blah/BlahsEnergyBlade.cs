using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah;

public class BlahsEnergyBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, new Color(250, 250, 250, 250));
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<Projectiles.Melee.BlahEnergySlash>(), 250, 20f, 14f, 14, 14, shootsEveryUse: true, noMelee: true, crit: 10, width: 60, height: 70);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(0, 25);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 255);
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 0.5f);
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<Phantoplasm>(), 45)
	//        .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 40)
	//        .AddIngredient(ModContent.ItemType<SoulofTorture>(), 45)
	//        .AddIngredient(ModContent.ItemType<ElementalExcalibur>())
	//        .AddIngredient(ModContent.ItemType<BerserkerBlade>())
	//        .AddIngredient(ModContent.ItemType<PumpkingsSword>())
	//        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//        .Register();
	//}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem) * 1.4f;
		Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, (float)player.direction * player.gravDir, player.itemAnimationMax * 1.2f, adjustedItemScale5 * 1.4f);
		NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		for (int num194 = 0; num194 < 4; num194++)
		{
			float num195 = velocity.X;
			float num196 = velocity.Y;
			num195 += Main.rand.Next(-40, 41) * 0.05f;
			num196 += Main.rand.Next(-40, 41) * 0.05f;
			Projectile.NewProjectile(source, position.X, position.Y, num195, num196, ModContent.ProjectileType<Projectiles.Melee.BlahBeam>(), damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		player.VampireHeal(hit.Damage, target.position);
	}
}
