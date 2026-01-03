using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode.Phantasm
{
	public class PhantasmHealthOrbs : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 4;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers() { Hide = true});
		}
		public override void SetDefaults()
		{
			NPC.Size = new Vector2(100);
			NPC.damage = 0;
			NPC.lifeMax = 25000;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.alpha = 256;
			NPC.HitSound = SoundID.DD2_LightningBugZap;
			NPC.DeathSound = SoundID.NPCDeath56;
			NPC.knockBackResist = 0;
		}
		public override void AI()
		{
			Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit);
			d.noGravity = true;
			d.velocity = NPC.velocity;

			if (!Main.npc[(int)NPC.ai[0]].active)
			{
				NPC.alpha += 8;
				if (NPC.alpha > 255)
					NPC.active = false;
			}

			NPC.ai[1]+= 0.01f;
			NPC.velocity = Vector2.One.RotatedBy(NPC.ai[1]) * 0.4f;

			foreach (Player p in Main.ActivePlayers)
			{
				if (p.Center.Distance(NPC.Center) < 16 * 30)
				{
					NPC.dontTakeDamage = false;
					break;
				}
				else
				{
					NPC.dontTakeDamage = true;
				}
			}
			NPC.localAI[0] = MathHelper.Lerp(NPC.dontTakeDamage ? 0 : 1, NPC.localAI[0], 0.9f);
			Lighting.AddLight(NPC.Center, new Vector3(0f, 1f, 1f));

			if (NPC.alpha > 0)
			{
				NPC.alpha -= 2;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Asset<Texture2D> texture = TextureAssets.Npc[Type];
			int frameHeight = texture.Value.Height / Main.npcFrameCount[NPC.type];
			Rectangle sourceRectangle = new Rectangle(0, NPC.frame.Y, texture.Value.Width, frameHeight);
			Vector2 frameOrigin = sourceRectangle.Size() / 2f;
			Vector2 offset = new Vector2(NPC.width / 2 - frameOrigin.X, NPC.height - sourceRectangle.Height);

			Vector2 drawPos = NPC.position - screenPos + frameOrigin + offset;

			NPC target = Main.npc[(int)NPC.ai[0]];
			// light beams
			Texture2D beam = TextureAssets.Extra[ExtrasID.RainbowRodTrailShape].Value;
			Main.EntitySpriteDraw(beam, drawPos, null, Color.Lerp(new Color(0.6f,0f,1f,0f), new Color(0.3f,0.7f,1f,0f), NPC.localAI[0]) * NPC.Opacity * 0.5f, NPC.Center.DirectionTo(target.Center).ToRotation(), new Vector2(0, beam.Height / 2), new Vector2(NPC.Center.Distance(target.Center) / beam.Height, 0.3f + MathF.Sin(NPC.ai[1] * 3) * 0.1f), SpriteEffects.None, 0);

			Color color = Color.Lerp(Color.Purple,Color.White,NPC.localAI[0]);
			Main.EntitySpriteDraw(texture.Value, drawPos, sourceRectangle, color * 0.3f * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale * 1.1f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(texture.Value, drawPos, sourceRectangle, color * 0.15f * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale * 1.2f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(texture.Value, drawPos, sourceRectangle, color * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);
			return false;
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter++;
			NPC.frame.Y = frameHeight * (int)((NPC.frameCounter / 5) % 4);
		}
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life > 0) return;
			NPC target = Main.npc[(int)NPC.ai[0]];
			int iterations = (int)(NPC.Center.Distance(target.Center) / 16);
			for (int i = 0; i < iterations; i++)
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = Vector2.Lerp(NPC.Center,target.Center, i / (float)iterations) + Main.rand.NextVector2Circular(8,8);
				s.Rotation = target.Center.DirectionTo(NPC.Center).ToRotation();
				s.Velocity = target.velocity * (i / (float)iterations);
				s.Scale = new Vector2(3f, 1f) * Main.rand.NextFloat(0.5f,1f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 10);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(15, 35);
				s.AdditiveAmount = 1f;
				s.ColorTint = Color.Lerp(new Color(0.6f, 0f, 1f, 0f), new Color(0.3f, 0.7f, 1f, 0f), NPC.localAI[0]);
				Main.ParticleSystem_World_OverPlayers.Add(s);
			}

			for (int i = 0; i < 20; i++)
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = NPC.Center;
				s.Velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(6f, 12f);
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(6f, 2f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 10);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(20, 40);
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(Main.rand.NextFloat(0.2f, 0.7f), 0.9f, 1f);
				Main.ParticleSystem_World_OverPlayers.Add(s);
			}
			for (int i = 0; i < 20; i++)
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = NPC.Center;
				s.Velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(12f, 24f);
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(3f, 1f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 5);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(10, 30);
				s.AdditiveAmount = 1f;
				s.ColorTint = Color.White;
				Main.ParticleSystem_World_OverPlayers.Add(s);
			}
			for (int i = 0; i < 40; i++)
			{
				int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default, 1f);
				Main.dust[num890].velocity *= Main.rand.NextFloat(10f);
				Main.dust[num890].scale = 1.5f;
				Main.dust[num890].noGravity = true;
				Main.dust[num890].fadeIn = 2f;
			}
		}
	}
}
