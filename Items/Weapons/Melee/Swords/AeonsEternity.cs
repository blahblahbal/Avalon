using Avalon;
using Avalon.Common.Extensions;
using Avalon.Common.Interfaces;
using Avalon.Dusts;
using Avalon.Particles;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class AeonsEternity : ModItem, ISyncedOnHitEffect
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
	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		Main.ParticleSystem_World_OverPlayers.Add(new AeonStarburst(Main.rand.NextVector2FromRectangle(target.Hitbox), Vector2.Zero, Color.Cyan, Main.rand.NextFloat(MathHelper.TwoPi), 1.5f));
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source,position,velocity,type,damage / 5,knockback,player.whoAmI,Utils.RandomNextSeed((ulong)Main.timeForVisualEffects));
		return false;
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{

		ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
		Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * player.direction * player.gravDir);
		Dust d = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<SimpleColorableGlowyDust>(), player.velocity.X * 0.2f + player.direction * 3, player.velocity.Y * 0.2f, 140, Color.Lerp(new Color(1f,1f,0.8f,0f), new Color(1f,0.7f,0.6f,0f),player.itemAnimation / (float)player.itemAnimationMax), 1.2f);
		d.position = location2;
		d.noGravity = true;
		d.velocity *= 0.25f;
		d.velocity += vector2 * 5f;
	}
	public override void AddRecipes()
	{
		
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
			.AddIngredient(ModContent.ItemType<DesertLongsword>())
			.AddIngredient(ModContent.ItemType<OsmiumGreatsword>())
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
