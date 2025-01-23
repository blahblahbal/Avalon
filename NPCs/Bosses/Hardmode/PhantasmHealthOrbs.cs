using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode
{
	public class PhantasmHealthOrbs : ModNPC
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return false;
		}
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 4;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers() { Hide = true});
		}
		public override void SetDefaults()
		{
			NPC.Size = new Vector2(100);
			NPC.damage = 50;
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

			Color color = (!NPC.dontTakeDamage ? Color.White : Color.Purple);
			Main.EntitySpriteDraw(texture.Value, drawPos, sourceRectangle, color * 0.3f * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale * 1.1f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(texture.Value, drawPos, sourceRectangle, color * 0.15f * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale * 1.2f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(texture.Value, drawPos, sourceRectangle, color * NPC.Opacity, NPC.rotation, frameOrigin, new Vector2(NPC.scale, NPC.scale), SpriteEffects.None, 0);
			return false;
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter++;
			NPC.frame.Y = frameHeight * (int)((NPC.frameCounter / 5) % 4);
		}
	}
}
