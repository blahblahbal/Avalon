using Avalon.Common.Extensions;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles.OldParticleSystem;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.Boomlash;

public class Boomlash : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponChanneled(ModContent.ProjectileType<BoomlashProj>(), 80, 12f, 40, 4f, 30);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 15);
		Item.UseSound = SoundID.Item20;
	}
}
public class BoomlashProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<Boomlash>().DisplayName;
	private static Asset<Texture2D>? texture;
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 1;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 29;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		//Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
		texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/SparklySingleEnd");
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.Flamelash);
		//Projectile.width = 10;
		//Projectile.height = 10;
		//Projectile.friendly = true;
		//Projectile.light = 0.8f;
		Projectile.DamageType = DamageClass.Magic;
		DrawOriginOffsetY = -6;
		Projectile.extraUpdates = 1;
		Projectile.penetrate = 1;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Rectangle frame = texture.Frame();
		Vector2 frameOrigin = frame.Size() / 2f;

		Rectangle frame2 = TextureAssets.Projectile[Type].Frame();
		Vector2 frameOrigin2 = frame2.Size() / 2f;

		Color col = Color.Lerp(Color.Yellow, Color.Red, Main.masterColor) * 0.4f;
		Color col2 = Color.Lerp(Color.White, Color.Black, Main.masterColor);
		Vector2 stretchscale = new Vector2(Projectile.scale * 1.4f + (Main.masterColor / 2));


		for (int i = 1; i < (Projectile.oldPos.Length - 1); i++)
		{
			col.A = 0;
			Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2);
			Main.EntitySpriteDraw(texture.Value, drawPos + Main.rand.NextVector2Circular(i / 2, i / 2), frame, new Color(col.R, col.G - (i * 8), col.B, 0) * (1 - (i * 0.04f)), Projectile.oldRot[i] + Main.rand.NextFloat(-i * 0.01f, i * 0.01f), frameOrigin, new Vector2(stretchscale.X - (i * 0.05f), (stretchscale.Y * Main.rand.NextFloat(0.1f, 0.05f) * Vector2.Distance(Projectile.oldPos[i], Projectile.oldPos[i + 1]) - (i * 0.05f)) * 0.7f), SpriteEffects.None, 0);
		}

		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, frame2, new Color(255, 0, 0, 0), Projectile.rotation, frameOrigin2, stretchscale * 0.8f, SpriteEffects.None, 0);
		col.A = 255;
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, frame2, col2 * Projectile.Opacity, Projectile.rotation, frameOrigin2, Projectile.scale, SpriteEffects.None, 0);

		return false;
	}
	public override void AI()
	{
		if (Projectile.ai[2] == 0)
		{
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Projectile.oldPos[k] = Projectile.position;
			}
			Projectile.ai[2]++;
		}
		if (Projectile.position.Distance(Projectile.oldPosition) > 1f)
		{
			if (Main.rand.NextBool(3) && Projectile.velocity != Vector2.Zero)
			{
				int dusty = Dust.NewDust(Projectile.Center, 0, 0, DustID.InfernoFork);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].velocity = Projectile.velocity * -0.6f;
				Main.dust[dusty].scale = 1f;
			}
			if (Main.rand.NextBool(6))
			{
				int dusty = Dust.NewDust(Projectile.Center, 0, 0, DustID.Smoke);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].scale = 1f;
				Main.dust[dusty].alpha = 128;
			}
		}
		if (Projectile.velocity != Vector2.Zero)
		{
			Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
		}
	}
	public override void OnKill(int timeLeft)
	{
		OldParticleSystemDeleteSoon.AddParticle(new ExplosionParticle(), Projectile.Center, Vector2.Zero, default, Main.rand.NextFloat(MathHelper.TwoPi), Main.rand.NextFloat(0.9f, 1.2f));
		if (Main.myPlayer == Projectile.owner)
		{
			int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
			Main.projectile[p].DamageType = DamageClass.Magic;
		}
		SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

		for (int i = 0; i < 10; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 2f);
			Main.dust[d].velocity = Main.rand.NextVector2Circular(6, 6);
			Main.dust[d].noGravity = true;
			Main.dust[d].fadeIn = 2.3f;
			Main.dust[d].customData = 0;
		}
		for (int i = 0; i < 20; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 2f);
			Main.dust[d].velocity = Main.rand.NextVector2Circular(5, 5);
			Main.dust[d].fadeIn = Main.rand.NextFloat(1, 2);
			Main.dust[d].customData = 0;
		}
		for (int i = 0; i < 20; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 0, default, 1.4f);
			Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-3, 0).RotatedBy(Projectile.velocity.ToRotation());
			Main.dust[d].noGravity = !Main.rand.NextBool(10);
		}
		for (int i = 0; i < 7; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0, 0, 0, default, 1.4f);
			//Main.dust[d].color = Color.Red;
			Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-5, 0).RotatedBy(Projectile.velocity.ToRotation());
			Main.dust[d].noGravity = Main.rand.NextBool(3);
		}
		for (int i = 0; i < 9; i++)
		{
			int g = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(10, 6) + new Vector2(-1, 0).RotatedBy(Projectile.velocity.ToRotation()), Main.rand.Next(61, 63), 0.8f);
			Main.gore[g].alpha = 128;
		}
		Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
	}
}
