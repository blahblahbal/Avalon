using Avalon.Common.Templates;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee
{
	public class MidnightsRazor : PiercingBoomerangTemplate
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return false;
		}
		public float MiscTimer { get => Projectile.ai[1]; set => Projectile.ai[1] = value; }
		private int MiscTimerLimit = 4;
		public override void SetStaticDefaults()
		{
			// These lines facilitate the trail drawing
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}
		public override void SetDefaults()
		{
			Rectangle dims = this.GetDims();
			Projectile.CloneDefaults(ProjectileID.ThornChakram);
			Projectile.aiStyle = -1;
			Projectile.Size = new Vector2(30);
			Projectile.penetrate = -1;
			DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
			DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));

			ReturnSpeed = 30f;
			ReturnAccel = 2f;
			SpriteSpinDirectionFlip = true;
			TimeBeforeReturn = 20;
			SpinSpeed = 0.6f;
		}
		private readonly bool?[] hitNPCs = new bool?[Main.maxNPCs];
		private readonly bool?[] hitPlayers = new bool?[Main.maxPlayers];
		public override void AI()
		{
			if (Projectile.ai[2] == 0)
			{
				Main.player[Projectile.owner].position.X += 0.01f;
			}
			if (Returning && MiscTimer < MiscTimerLimit)
			{
				MiscTimer++;
			}
			if (Main.rand.NextBool())
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.Shadowflame, Projectile.velocity.RotatedByRandom(0.8f) * 0.1f, 128);
				d.fadeIn = 1.5f;
				d.noGravity = true;
				d.scale = 1.5f;
				if (Returning)
				{
					Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.ShadowbeamStaff, Projectile.velocity.RotatedByRandom(0.8f) * 0.15f, 128);
					d2.fadeIn = 1.5f;
					d2.noGravity = true;
					d2.scale = 1.5f;
				}
			}
			base.AI();
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hitNPCs[target.whoAmI] == true && Returning)
			{
				HitEffects(target, true);
				target.AddBuff(BuffID.ShadowFlame, 120);
				hitNPCs[target.whoAmI] = false;
			}
			else if (!hitNPCs[target.whoAmI].HasValue && (!Returning || MiscTimer < MiscTimerLimit))
			{
				HitEffects(target, false);
				hitNPCs[target.whoAmI] = true;
			}
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			if (hitPlayers[target.whoAmI] == true && Returning)
			{
				HitEffects(target, true);
				target.AddBuff(BuffID.ShadowFlame, 120);
				hitPlayers[target.whoAmI] = false;
			}
			else if (!hitPlayers[target.whoAmI].HasValue && (!Returning || MiscTimer < MiscTimerLimit))
			{
				HitEffects(target, false);
				hitPlayers[target.whoAmI] = true;
			}
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			if (Returning && hitNPCs[target.whoAmI] == true)
			{
				modifiers.FinalDamage *= 2f;
			}
		}
		public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
			if (Returning && hitPlayers[target.whoAmI] == true)
			{
				modifiers.FinalDamage *= 2f;
			}
		}
		public void HitEffects(Entity target, bool alreadyHit)
		{
			ParticleOrchestraSettings particleOrchestraSettings = default;
			particleOrchestraSettings.PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
			if (alreadyHit)
			{
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge, particleOrchestraSettings, Projectile.owner);
			}
			else
			{
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.SilverBulletSparkle, particleOrchestraSettings, Projectile.owner);
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(projectileTexture.Width * 0.5f, projectileTexture.Height * 0.5f);
			SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			for (int i = 0; i < Projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2;
				Color color = new Color(55, 33, 75, 0) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(projectileTexture, drawPos - Vector2.Normalize(Projectile.velocity) * 8, null, color, Projectile.rotation, drawOrigin, ((Projectile.scale + 0.1f) - i / (float)Projectile.oldPos.Length), spriteEffects, 0f);
			}
			//Projectile.GetAlpha(lightColor)
			//Vector2 drawPos = Projectile.oldPos[0] - Main.screenPosition + Projectile.Size / 2; //+ new Vector2(DrawOffsetX, Projectile.gfxOffY);
			//																			 //drawPos.Y -= (projectileTexture.Height / 4) - 1;
			//Color color = new Color(55, 33, 75, 0) * (float)(Projectile.oldPos.Length - 0);
			//Main.spriteBatch.Draw(projectileTexture, drawPos, null, color, Projectile.rotation, drawOrigin, 1f, spriteEffects, 0f);
			return base.PreDraw(ref lightColor);
		}
	}
}
