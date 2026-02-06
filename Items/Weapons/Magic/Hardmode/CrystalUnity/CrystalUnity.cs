using Avalon;
using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Particles;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.CrystalUnity;

public class CrystalUnity : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
		ItemGlowmask.AddGlow(this, 255);
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<CrystalUnityShard>(), 46, 1.5f, 14, 13f, 11, 11, true);
		Item.scale = 0.9f;
		Item.reuseDelay = 14;
		Item.rare = ModContent.RarityType<Rarities.FractureRarity>();
		Item.value = Item.sellPrice(0, 10, 10);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("Avalon:GemStaves", 2)
			.AddIngredient(ItemID.CrystalStorm)
			.AddIngredient(ModContent.ItemType<Material.TomeMats.ElementDiamond>(), 2)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int x = Main.rand.Next(9);
		if (Main.rand.NextBool(15))
		{
			x = 9;
		}

		for (int spread = 0; spread < 3; spread++)
		{
			int dmg = Item.damage;
			if (x == 9) dmg = (int)(dmg * 2.5f);
			Projectile.NewProjectile(source, position, velocity * Main.rand.NextFloat(0.8f, 1.2f), ModContent.ProjectileType<CrystalUnityShard>(), (int)player.GetDamage(DamageClass.Magic).ApplyTo(dmg), knockback, player.whoAmI, Main.rand.NextFloat() - 0.5f, 0f, x);
		}
		return false;
	}
}
public class CrystalUnityShard : ModProjectile
{
	const int amber = 0;
	const int amethyst = 1;
	const int diamond = 2;
	const int emerald = 3;
	const int peridot = 4;
	const int ruby = 5;
	const int sapphire = 6;
	const int topaz = 7;
	const int tourmaline = 8;
	const int zircon = 9;
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 10;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(200, 200, 200, 128);
	}
	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.scale = 1f;
		Projectile.aiStyle = -1;
		Projectile.timeLeft = 3600;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 60 * 3;
		Projectile.alpha = 255;
	}
	private int GemType => (int)Projectile.ai[2];

	public static int[] DustIds = { DustID.AmberBolt, DustID.GemAmethyst, DustID.GemDiamond, DustID.GemEmerald, ModContent.DustType<PeridotDust>(), DustID.GemRuby, DustID.GemSapphire, DustID.GemTopaz, ModContent.DustType<TourmalineDust>(), ModContent.DustType<ZirconDust>() };
	public static Color[] Colors = { Color.OrangeRed, Color.Purple, Color.White, Color.MediumSeaGreen, Color.GreenYellow, Color.Red, Color.Blue, Color.Orange, Color.Cyan, new Color(128, 32, 8) };
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 12; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustIds[GemType], Main.rand.NextVector2Circular(5, 5));
			d.noGravity = true;
			//d.velocity += Projectile.velocity;
		}
		for(int i = 0; i < Projectile.oldPos.Length; i++)
		{
			if (Main.rand.NextBool())
				continue;

			Dust d = Dust.NewDustPerfect(Projectile.oldPos[i] + Projectile.Size / 2, DustIds[GemType], new Vector2(3, 0).RotatedBy(Projectile.oldRot[i]) + Main.rand.NextVector2Circular(3,3));
			d.noGravity = true;
			d.scale *= (1f - (i / (float)Projectile.oldPos.Length)) * 1.5f;
		}
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		for(int i = 0; i < 4; i++)
		{
			SparkleParticle p = new();
			p.ColorTint = Colors[GemType] with { A = 0 };
			p.HighlightColor = Color.White with { A = 0 };
			p.FadeInEnd = p.FadeOutStart = Main.rand.Next(5, 10);
			p.FadeOutEnd = Main.rand.NextFloat(15, 20);
			p.Scale = new Vector2(3, 0.5f);
			p.Velocity = new Vector2(0, Main.rand.NextFloat(2,4)).RotatedBy(i * MathHelper.PiOver2 + Main.rand.NextFloat(-0.3f, 0.3f));
			p.Rotation = p.Velocity.ToRotation();
			p.DrawVerticalAxis = false;
			ParticleSystem.NewParticle(p, Projectile.Center);
		}
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation();
		if (Projectile.ai[1] == 0f)
		{
			if (GemType == diamond)
			{
				Projectile.penetrate = 4;
			}

			Vector2 location = Projectile.Center + Vector2.Normalize(Projectile.velocity) * 20;
			for (int i = 0; i < 3; i++)
			{
				SparkleParticle p = new();
				p.ColorTint = Colors[GemType] with { A = 0 };
				p.HighlightColor = Color.White with { A = 0 };
				p.FadeInEnd = p.FadeOutStart = Main.rand.Next(5, 10);
				p.FadeOutEnd = Main.rand.NextFloat(15, 20);
				p.Scale = new Vector2(3, 0.5f);
				p.Velocity = Projectile.velocity.RotateRandom(0.2f) * Main.rand.NextFloat();
				p.Rotation = p.Velocity.ToRotation();
				p.DrawVerticalAxis = false;
				ParticleSystem.NewParticle(p, location);
			}
		}

		Projectile.ai[1]++;
		Projectile.frame = GemType;
		Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[0] * 0.01f);
		Projectile.ai[0] *= 0.99f;
		if(Projectile.Opacity < 1f)
		{
			Projectile.Opacity += 0.05f;
		}
		if (Main.rand.NextBool(5))
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustIds[GemType], Projectile.velocity.RotatedByRandom(0.1f));
			d.noGravity = true;
			d.scale *= 1.3f;
		}
		//if (Projectile.ai[1] > 1)
		//{
		//	for (var i = 0; i < 3; i++)
		//	{
		//		var dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.position, Projectile.oldPosition, i / 2f) + Projectile.Size / 2 + Vector2.Normalize(Projectile.velocity) * 3, DustIds[GemType], Projectile.velocity, 50, default, 1f);
		//		dust.frame.Y = 10;
		//		if (dust.type == DustID.AmberBolt)
		//		{
		//			dust.frame.Y += 60;
		//		}
		//		dust.scale = (float)Math.Sin(Projectile.ai[1] * 0.2f) / 5 + 1;

		//		dust.scale *= MathHelper.Clamp(Projectile.ai[1] * 0.1f, 0, 1);
		//		dust.noGravity = true;
		//		dust.velocity *= 0.3f;
		//	}
		//}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		if (Projectile.ai[1] > 0) 
		{
			default(CrystalUnityVertexStrip).Draw(Projectile);
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, TextureAssets.Projectile[Type].Frame(1, Main.projFrames[Type], 0, Projectile.frame),Color.White * Projectile.Opacity,Projectile.rotation + MathHelper.PiOver2, new Vector2(5,9), Projectile.scale, SpriteEffects.None);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, TextureAssets.Projectile[Type].Frame(1, Main.projFrames[Type], 0, Projectile.frame), Color.White with { A = 0 } * Projectile.Opacity * 0.5f, Projectile.rotation + MathHelper.PiOver2, new Vector2(5, 9), Projectile.scale * 1.5f, SpriteEffects.None);
		return false;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		switch (GemType)
		{
			case amber:
				Main.player[Projectile.owner].AddBuff(ModContent.BuffType<AmberShardBuff>(), 60 * 5);
				break;
			case amethyst:
				Main.player[Projectile.owner].AddBuff(ModContent.BuffType<AmethystShardBuff>(), 60 * 5);
				break;
			case emerald:
				target.AddBuff(BuffID.Midas, 60 * 10);
				break;
			case peridot:
				target.AddBuff(BuffID.Poisoned, 60 * 5);
				break;
			case ruby:
				Main.player[Projectile.owner].AddBuff(ModContent.BuffType<RubyShardBuff>(), 60 * 5);
				break;
			case sapphire:
				Main.player[Projectile.owner].AddBuff(ModContent.BuffType<SapphireShardBuff>(), 60 * 5);
				break;
			case topaz:
				target.AddBuff(BuffID.Ichor, 60 * 4);
				break;
			case tourmaline:
				target.AddBuff(BuffID.Slow, 60 * 5);
				break;
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (!info.PvP)
		{
			return;
		}
		switch (GemType)
		{
			case amber:
				target.AddBuff(BuffID.OnFire, 60 * 5);
				break;
			case peridot:
				target.AddBuff(BuffID.Poisoned, 60 * 5);
				break;
			case ruby:
				Main.player[Projectile.owner].AddBuff(ModContent.BuffType<RubyShardBuff>(), 60 * 5);
				break;
			case sapphire:
				Main.player[Projectile.owner].AddBuff(ModContent.BuffType<SapphireShardBuff>(), 60 * 5);
				break;
			case topaz:
				target.AddBuff(BuffID.Ichor, 60 * 4);
				break;
		}
	}
}
public struct CrystalUnityVertexStrip
{
	private static VertexStrip _vertexStrip = new VertexStrip();

	private Color StripColor;
	public void Draw(Projectile proj)
	{
		StripColor = CrystalUnityShard.Colors[(int)proj.ai[2]];
		MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
		miscShaderData.UseSaturation(proj.velocity.Length() * -0.2f);
		miscShaderData.UseOpacity(proj.Opacity * 2);
		miscShaderData.UseImage1(TextureAssets.Extra[197]);
		miscShaderData.UseImage2(TextureAssets.Extra[193]);
		miscShaderData.Apply();
		_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
		_vertexStrip.DrawTrail();
		Main.pixelShader.CurrentTechnique.Passes[0].Apply();
	}
	private Color StripColors(float progressOnStrip)
	{
		return Color.Lerp(StripColor, Color.White, 0.15f) with { A = 0 } * (1f - MathF.Pow(progressOnStrip,3));
	}
	private float StripWidth(float progressOnStrip)
	{
		return MathHelper.Lerp(7,20,progressOnStrip);
	}
}
