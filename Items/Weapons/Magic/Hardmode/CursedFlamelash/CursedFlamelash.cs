using Avalon.Common.Extensions;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.CursedFlamelash;

public class CursedFlamelash : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponChanneled(ModContent.ProjectileType<CursedFlamelashProj>(), 40, 4f, 17, 6f, 23);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
		Item.UseSound = SoundID.Item20;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Flamelash)
			.AddIngredient(ItemID.CursedFlame, 30)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class CursedFlamelashProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<CursedFlamelash>().DisplayName;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		Main.projFrames[Type] = 6;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.MagicMissile);
		//Projectile.extraUpdates = 1;
		Projectile.penetrate = 3;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
	}
	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
	}
	public override void AI()
	{
		Projectile.scale = MathHelper.Lerp(1.5f, 1.2f, MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, 0, 1));

		if (Main.rand.NextBool(5) && Projectile.position.Distance(Projectile.oldPos[1]) > 0.2f)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.CursedTorch, Projectile.velocity.RotateRandom(0.4f) * Main.rand.NextFloat(0.1f, 0.2f));
			d.noGravity = true;
			d.scale = (Main.rand.NextFloat(1, 2));
			d.fadeIn = (Main.rand.NextFloat(1.5f, 2.5f));
		}

		if (Projectile.position.Distance(Projectile.oldPos[1]) < 0.2f && Main.rand.NextBool(2))
		{
			Dust d2 = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(Projectile.Hitbox), DustID.CursedTorch, Main.rand.NextVector2Circular(1, 2));
			d2.noGravity = Main.rand.NextBool(6);
			d2.scale = (Main.rand.NextFloat(2, 3));
			//d2.noLight = true;
			if (!d2.noGravity)
			{
				d2.scale *= 0.2f;
			}
		}

		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 3)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame > 5)
		{
			Projectile.frame = 0;
		}
	}

	public override void OnKill(int timeLeft)
	{
		ParticleSystem.NewParticle(new CursedExplosionParticle(Main.rand.NextFloat(MathHelper.TwoPi), Main.rand.NextFloat(0.9f, 1.2f)), Projectile.Center);
		SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
		float decreaseBy = 0.05f;
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type] / 2; i++)
		{
			if (Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]).Length() > 0.6f)
			{
				for (int i2 = 0; i2 < Main.rand.Next(2, 4); i2++)
				{
					Dust d = Dust.NewDustPerfect(Projectile.oldPos[i], DustID.CursedTorch, Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]).RotateRandom(0.4f) * Main.rand.NextFloat(7, 9));
					d.noGravity = !Main.rand.NextBool(3);
					d.scale = (Main.rand.NextFloat(0.25f, 0.5f) * i2) - (decreaseBy * i);
					d.fadeIn = (Main.rand.NextFloat(0.75f, 1f) * i2) - (decreaseBy * i * 2);
					//d.noLight = true;
					if (!d.noGravity)
					{
						d.scale *= 0.5f;
					}
				}
			}
		}

		for (int i = 0; i < 30; i++)
		{
			Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.CursedTorch, 0, 0, 0, default, 3);
			D.color = new Color(255, 255, 255, 0);
			D.noGravity = !Main.rand.NextBool(3);
			D.fadeIn = Main.rand.NextFloat(0f, 2f);
			D.velocity = Main.rand.NextVector2Circular(4, 6).RotatedBy(Projectile.rotation);
		}
		if (Main.myPlayer == Projectile.owner)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CursedFlamelashExplosion>(), Projectile.damage * 2, Projectile.knockBack * 2, Projectile.owner);
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		default(CursedFlameLashVertexStrip).Draw(Projectile);

		//Thanks ballsfah
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
		Vector2 drawPos = Projectile.Center - Main.screenPosition;

		float Rot = MathHelper.Lerp(0, Projectile.velocity.ToRotation() - MathHelper.PiOver2, MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, 0, 1));

		//Main.EntitySpriteDraw(texture, drawPos, frame, Color.White, Rot, new Vector2(texture.Width, frameHeight) / 2, new Vector2(Projectile.scale,MathHelper.Clamp(Projectile.velocity.Length() * 0.2f,Projectile.scale,Projectile.scale * 2f)), SpriteEffects.None, 0);
		//The line above stretches the flame with speed
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame, new Color(230, 255, 0, 128), Rot, new Vector2(TextureAssets.Projectile[Type].Value.Width / 2, frameHeight * 0.6f), Projectile.scale, SpriteEffects.None, 0);

		return false;
	}
}
public struct CursedFlameLashVertexStrip
{
	private static VertexStrip _vertexStrip = new VertexStrip();

	private float transitToDark;
	public void Draw(Projectile proj)
	{
		this.transitToDark = Utils.GetLerpValue(0f, 6f, proj.localAI[0], clamped: true);
		MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
		miscShaderData.UseSaturation(-4f);
		miscShaderData.UseOpacity(proj.Opacity * 8);
		miscShaderData.UseImage1(TextureAssets.Extra[ExtrasID.FlameLashTrailShape]);
		miscShaderData.UseImage2(TextureAssets.Extra[ExtrasID.MagicMissileTrailErosion]);
		miscShaderData.Apply();
		_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
		_vertexStrip.DrawTrail();
		Main.pixelShader.CurrentTechnique.Passes[0].Apply();
	}

	private Color StripColors(float progressOnStrip)
	{
		float lerpValue = Utils.GetLerpValue(0f - 0.1f * this.transitToDark, 0.7f - 0.2f * this.transitToDark, progressOnStrip, clamped: true);
		Color result = Color.Lerp(Color.Lerp(new Color(128,255,0), Color.Green, this.transitToDark * 0.5f), Color.DarkGreen, lerpValue) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
		result.A /= 8;
		return result;
	}

	private float StripWidth(float progressOnStrip)
	{
		float lerpValue = Utils.GetLerpValue(0f, 0.06f + this.transitToDark * 0.01f, progressOnStrip, clamped: true);
		lerpValue = 1f - (1f - lerpValue) * (1f - lerpValue);
		return MathHelper.Lerp(32f + this.transitToDark * 24f, 8f, Utils.GetLerpValue(0f, 1f, progressOnStrip, clamped: true)) * lerpValue;
	}
}
public class CursedFlamelashExplosion : ModProjectile
{
	public override string Texture => ModContent.GetInstance<CursedFlamelash>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(128);
		Projectile.aiStyle = -1;
		Projectile.hide = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 20;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
		modifiers.HitDirectionOverride = (target.Center.X <= Projectile.Center.X) ? -1 : 1;
	}
	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
		modifiers.HitDirectionOverride = (target.Center.X <= Projectile.Center.X) ? -1 : 1;
	}
}