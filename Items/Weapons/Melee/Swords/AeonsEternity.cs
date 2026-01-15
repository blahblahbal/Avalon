using Avalon;
using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Particles;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class AeonsEternity : ModItem
{
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 128);
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<AeonStar>(), 40, 5, 8f, 81, 20);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		ParticleSystem.AddParticle(new AeonStarburst(), Main.rand.NextVector2FromRectangle(target.Hitbox), Vector2.Zero, Color.Cyan, Main.rand.NextFloat(MathHelper.TwoPi), 1.5f);
	}
	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		ParticleSystem.AddParticle(new AeonStarburst(), Main.rand.NextVector2FromRectangle(target.Hitbox), Vector2.Zero, Color.Cyan, Main.rand.NextFloat(MathHelper.TwoPi), 1.5f);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int lastStar = -255;
		SoundEngine.PlaySound(SoundID.Item9, player.Center);
		for (int i = 0; i < Main.rand.Next(4, 8); i++)
		{
			Vector2 velRand = velocity.RotatedByRandom(Math.PI / 6) * Main.rand.NextFloat(0.3f, 2.4f);

			Vector2 dirToMouse = player.SafeDirectionTo(Main.MouseWorld);
			Vector2 velMult = player.velocity * new Vector2(MathF.Abs(dirToMouse.X), MathF.Abs(dirToMouse.Y)); // The player's current velocity, multiplied by the unsigned cosine & sine of the angle to the mouse

			Projectile p = Projectile.NewProjectileDirect(Item.GetSource_FromThis(), position, velRand + velMult * 0.8f + player.velocity * 0.2f, ModContent.ProjectileType<AeonStar>(), damage / 5, knockback, player.whoAmI, lastStar, 160 + i * 10, (float)Main.timeForVisualEffects);
			p.scale = Main.rand.NextFloat(0.9f, 1.1f);
			p.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
			lastStar = p.whoAmI;
		}
		return false;
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{

		ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
		Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * player.direction * player.gravDir);
		int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<SimpleColorableGlowyDust>(), player.velocity.X * 0.2f + player.direction * 3, player.velocity.Y * 0.2f, 140, Color.Lerp(new Color(1f,1f,0.8f,0f), new Color(1f,0.7f,0.6f,0f),player.itemAnimation / (float)player.itemAnimationMax), 1.2f);
		Main.dust[num15].position = location2;
		Main.dust[num15].noGravity = true;
		Main.dust[num15].velocity *= 0.25f;
		Main.dust[num15].velocity += vector2 * 5f;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword>())
			.AddIngredient(ItemID.AntlionClaw)
			.AddIngredient(ModContent.ItemType<OsmiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword>())
			.AddIngredient(ModContent.ItemType<DesertLongsword>())
			.AddIngredient(ModContent.ItemType<OsmiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword>())
			.AddIngredient(ItemID.AntlionClaw)
			.AddIngredient(ModContent.ItemType<RhodiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword>())
			.AddIngredient(ModContent.ItemType<DesertLongsword>())
			.AddIngredient(ModContent.ItemType<RhodiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword>())
			.AddIngredient(ItemID.AntlionClaw)
			.AddIngredient(ModContent.ItemType<IridiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword>())
			.AddIngredient(ModContent.ItemType<DesertLongsword>())
			.AddIngredient(ModContent.ItemType<IridiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}
