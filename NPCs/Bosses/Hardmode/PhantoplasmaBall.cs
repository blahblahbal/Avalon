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
	public class PhantoplasmaBall : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 4;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers() { Hide = true});
			NPCID.Sets.TrailingMode[NPC.type] = 0;
			NPCID.Sets.TrailCacheLength[NPC.type] = 12;
		}
		public override void SetDefaults()
		{
			NPC.Size = new Vector2(100);
			NPC.damage = 50;
			NPC.lifeMax = 75000;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.alpha = 256;
			NPC.HitSound = SoundID.DD2_LightningBugZap;
			NPC.DeathSound = SoundID.NPCDeath56;
		}
		public override void AI()
		{
			Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>());
			d.noGravity = true;
			d.velocity = NPC.velocity;

			if (!Main.npc[(int)NPC.ai[0]].active)
			{
				NPC.alpha += 8;
				if (NPC.alpha > 255)
					NPC.active = false;
			}

			if(!NPC.HasValidTarget)
				NPC.TargetClosest();
			else
			{
				if (NPC.ai[1] == 0)
					NPC.velocity = NPC.Center.DirectionTo(Main.player[NPC.target].Center) * 2;
				else
					NPC.velocity = NPC.Center.DirectionTo(Main.player[NPC.target].Center) * 4;
				//NPC.velocity = NPC.Center.DirectionTo(Main.npc[(int)NPC.ai[0]].Center) * 4;
			}
			Lighting.AddLight(NPC.Center, new Vector3(1f, 0f, 0f));

			if(NPC.alpha > 0)
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

			Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				Vector2 drawPosOld = NPC.oldPos[i] - Main.screenPosition + frameOrigin + offset;
				Main.EntitySpriteDraw(texture.Value, drawPosOld + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 125, 125, 0) * (1 - (i * (1/12f))) * 0.2f * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);
			}
			Main.EntitySpriteDraw(texture.Value, drawPos + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 255, 255, 0) * 0.3f * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale * 1.1f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(texture.Value, drawPos + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 255, 255, 0) * 0.15f * NPC.Opacity, NPC.rotation, frameOrigin, NPC.scale * 1.2f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(texture.Value, drawPos + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 255, 255, 0) * NPC.Opacity, NPC.rotation, frameOrigin, new Vector2(NPC.scale, NPC.scale), SpriteEffects.None, 0);
			return false;
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter++;
			NPC.frame.Y = frameHeight * (int)((NPC.frameCounter / 5) % 4);
		}
	}
}
