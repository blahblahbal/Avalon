using Avalon;
using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Particles;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class TrueAeonsEternity : ModItem
{
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 128);
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<TrueAeonStar>(), 64, 5, 8f, 81, 20);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int lastStar = -255;
		SoundEngine.PlaySound(SoundID.Item9, player.Center);
		for (int i = 0; i < 6; i++)
		{
			int P = Projectile.NewProjectile(Item.GetSource_FromThis(), position, velocity.RotatedByRandom(Math.PI / 4) * Main.rand.NextFloat(0.6f, 2.8f), ModContent.ProjectileType<TrueAeonStar>(), damage / 4, knockback, player.whoAmI, lastStar, 160 + (i * 10), (float)Main.time);
			Main.projectile[P].scale = Main.rand.NextFloat(0.9f, 1.1f);
			Main.projectile[P].rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
			lastStar = P;
		}
		return false;
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		if (player.itemTime % 4 == 0)
		{
			int type = Main.rand.Next(2);
			ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
			Vector2 vector2 = outwardDirection2.RotatedBy(-1f * (float)player.direction * player.gravDir) * Main.rand.NextFloat(34,120);

			float percent = player.itemAnimation / (float)player.itemAnimationMax;
			ParticleSystem.AddParticle(new TrueAeonSlash(vector2,player,Main.rand.NextFloat(0.3f,1f),Main.rand.NextFloat(MathHelper.PiOver2,MathHelper.Pi) * Math.Max(percent,0.5f),player.direction,Main.rand.NextFloat(20f,35f), new Color(Main.rand.Next(200), 100, 255, 0)), player.Center);
		}
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<AeonsEternity>())
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddIngredient(ItemID.MeteoriteBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}