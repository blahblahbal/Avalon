using Avalon;
using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;
public class QuantumClaymore : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<QuantumBeam>(), 88, 6f, 16f, 46, 23, scale: 1.1f);
		Item.rare = ModContent.RarityType<Rarities.QuantumRarity>();
		Item.value = Item.sellPrice(0, 10, 90);
		Item.UseSound = SoundID.Item15;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<WickedShard>(), 10)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		int DustType = DustID.CorruptTorch;
		if (Main.rand.NextBool())
			DustType = DustID.HallowedTorch;

		for (int j = 0; j < 2; j++)
		{
			ClassExtensions.GetPointOnSwungItemPath(60f, 120f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
			Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
			Dust d = Dust.NewDustPerfect(location2, DustType, vector2 * 2f, 100, default(Color), 0.7f + Main.rand.NextFloat() * 1.3f);
			d.noGravity = true;
			d.velocity *= 2f;
			if (Main.rand.NextBool(20))
			{
				int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 140, default(Color), 1.3f);
				Main.dust[num15].position = location2;
				Main.dust[num15].fadeIn = 1.2f;
				Main.dust[num15].noGravity = true;
				Main.dust[num15].velocity *= 2f;
				Main.dust[num15].velocity += vector2 * 5f;
			}
		}
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 1f);
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		Vector2 SwordSpawn = player.position - new Vector2(Main.rand.Next(60, 180) * player.direction, Main.rand.Next(-75, 75));
		Projectile P = Projectile.NewProjectileDirect(player.GetSource_FromThis(), SwordSpawn, SwordSpawn.DirectionTo(target.Center) * (Item.shootSpeed * Main.rand.NextFloat(1.2f, 1.6f)), ModContent.ProjectileType<QuantumBeam>(), (int)(Item.damage * 0.6f), Item.knockBack * 0.1f, player.whoAmI, 0, Main.rand.Next(-20, -10));
		target.AddBuff(BuffID.ShadowFlame, 300);
	}
}
