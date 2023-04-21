//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace Avalon.NPCs.PreHardmode
//{
//    public class Rockarolla : ModNPC
//    {
//        public override void SetDefaults()
//        {
//            NPC.lifeMax = 120;
//            NPC.defense = 5;
//            NPC.aiStyle = -1;
//            NPC.HitSound = SoundID.Tink;
//            NPC.Size = new Vector2(30);
//            NPC.noGravity= true;
//        }
//        public override void AI()
//        {
//            if (NPC.ai[0] == 0f)
//            {
//                NPC.TargetClosest();
//                NPC.directionY = 1;
//                NPC.ai[0] = 1f;
//            }
//            int Speed = 8;
//            if (NPC.ai[1] == 0f)
//            {
//                NPC.rotation += (float)(NPC.direction * NPC.directionY) * 0.13f;
//                if (NPC.collideY)
//                {
//                    NPC.ai[0] = 2f;
//                }
//                if (!NPC.collideY && NPC.ai[0] == 2f)
//                {
//                    NPC.direction = -NPC.direction;
//                    NPC.ai[1] = 1f;
//                    NPC.ai[0] = 1f;
//                }
//                if (NPC.collideX)
//                {
//                    NPC.directionY = -NPC.directionY;
//                    NPC.ai[1] = 1f;
//                }
//            }
//            else
//            {
//                NPC.rotation -= (float)(NPC.direction * NPC.directionY) * 0.13f;
//                if (NPC.collideX)
//                {
//                    NPC.ai[0] = 2f;
//                }
//                if (!NPC.collideX && NPC.ai[0] == 2f)
//                {
//                    NPC.directionY = -NPC.directionY;
//                    NPC.ai[1] = 0f;
//                    NPC.ai[0] = 1f;
//                }
//                if (NPC.collideY)
//                {
//                    NPC.direction = -NPC.direction;
//                    NPC.ai[1] = 0f;
//                }
//            }
//            NPC.velocity.X = Speed * NPC.direction;
//            NPC.velocity.Y = Speed * NPC.directionY;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
//        {
//            Texture2D texture = ModContent.Request<Texture2D>(Texture + "Glow").Value;
//            Vector2 drawPos = NPC.Center - Main.screenPosition + new Vector2(0,-2);
//            Vector2 drawOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
//            Main.EntitySpriteDraw(texture, drawPos, texture.Frame(), new Color(255,255,255,0) * 0.5f * Main.masterColor, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
//        }
//    }
//}
