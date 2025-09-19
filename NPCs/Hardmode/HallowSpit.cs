using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Dusts;

namespace Avalon.NPCs.Hardmode;

public class HallowSpit : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Hide = true
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
	}
    public override void SetDefaults()
    {
        NPC.npcSlots = 1;
        NPC.width = 16;
        NPC.height = 16;
        NPC.aiStyle = -1;
        NPC.timeLeft = 750;
        NPC.damage = 65;
        NPC.DeathSound = SoundID.NPCDeath9;
        NPC.lifeMax = 1;
        NPC.hide = true;
        NPC.scale = 0.9f;
        NPC.netAlways = true;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.buffImmune[BuffID.Confused] = true;
    }
    public override void AI()
    {
        if (NPC.target == 255)
        {
            NPC.TargetClosest(true);
            float num157 = 6f;
            num157 = 7f;
            Vector2 vector15 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num158 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector15.X;
            float num159 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector15.Y;
            float num160 = (float)Math.Sqrt((double)(num158 * num158 + num159 * num159));
            num160 = num157 / num160;
            NPC.velocity.X = num158 * num160;
            NPC.velocity.Y = num159 * num160;
        }
		Color color = Color.Lerp(new Color(1f, 0.4f, 1f, 0f), new Color(0.4f, 1f, 1f, 0f),Math.Abs(MathF.Sin((float)Main.timeForVisualEffects * 0.1f)));
        NPC.ai[0] += 1f;
        if (NPC.ai[0] > 3f)
        {
            NPC.ai[0] = 3f;
        }
        if (NPC.ai[0] == 2f)
        {
            NPC.position += NPC.velocity;
            SoundEngine.PlaySound(SoundID.NPCDeath9, NPC.position);
            for (int num161 = 0; num161 < 20; num161++)
            {
                int num162 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, ModContent.DustType<SimpleColorableGlowyDust>(), 0f, 0f, 100, color, 1.8f);
                Main.dust[num162].velocity *= 1.3f;
                Main.dust[num162].velocity += NPC.velocity;
                Main.dust[num162].noGravity = true;
            }
        }

		NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.Center.DirectionTo(Main.player[NPC.target].Center) * 10,0.01f);

        if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
        {
            #region spread hallow code
            /*if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num163 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int num164 = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                int num165 = 8;
                for (int num166 = num163 - num165; num166 <= num163 + num165; num166++)
                {
                    for (int num167 = num164 - num165; num167 < num164 + num165; num167++)
                    {
                        if ((double)(Math.Abs(num166 - num163) + Math.Abs(num167 - num164)) < (double)num165 * 0.5)
                        {
                            if (Main.tile[num166, num167].type == 2)
                            {
                                Main.tile[num166, num167].type = 109;
                                WorldGen.SquareTileFrame(num166, num167, true);
                                if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.SendTileSquare(-1, num166, num167, 1);
                                }
                            }
                            else
                            {
                                if (Main.tile[num166, num167].type == 1)
                                {
                                    Main.tile[num166, num167].type = 117;
                                    WorldGen.SquareTileFrame(num166, num167, true);
                                    if (Main.netMode == NetmodeID.Server)
                                    {
                                        NetMessage.SendTileSquare(-1, num166, num167, 1);
                                    }
                                }
                                else
                                {
                                    if (Main.tile[num166, num167].type == 53)
                                    {
                                        Main.tile[num166, num167].type = 116;
                                        WorldGen.SquareTileFrame(num166, num167, true);
                                        if (Main.netMode == NetmodeID.Server)
                                        {
                                            NetMessage.SendTileSquare(-1, num166, num167, 1);
                                        }
                                    }
                                    else
                                    {
                                        if (Main.tile[num166, num167].type == 23)
                                        {
                                            Main.tile[num166, num167].type = 109;
                                            WorldGen.SquareTileFrame(num166, num167, true);
                                            if (Main.netMode == NetmodeID.Server)
                                            {
                                                NetMessage.SendTileSquare(-1, num166, num167, 1);
                                            }
                                        }
                                        else
                                        {
                                            if (Main.tile[num166, num167].type == 25)
                                            {
                                                Main.tile[num166, num167].type = 117;
                                                WorldGen.SquareTileFrame(num166, num167, true);
                                                if (Main.netMode == NetmodeID.Server)
                                                {
                                                    NetMessage.SendTileSquare(-1, num166, num167, 1);
                                                }
                                            }
                                            else
                                            {
                                                if (Main.tile[num166, num167].type == 112)
                                                {
                                                    Main.tile[num166, num167].type = 116;
                                                    WorldGen.SquareTileFrame(num166, num167, true);
                                                    if (Main.netMode == NetmodeID.Server)
                                                    {
                                                        NetMessage.SendTileSquare(-1, num166, num167, 1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }*/
            #endregion
            NPC.HitInfo h = new NPC.HitInfo { Damage = 999 };
            NPC.StrikeNPC(h);

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendStrikeNPC(NPC, h);
            }
        }
        if (NPC.timeLeft > 100)
        {
            NPC.timeLeft = 100;
        }
		Dust d = Dust.NewDustPerfect(NPC.Center, ModContent.DustType<SimpleColorableGlowyDust>(), NPC.velocity * 0.2f, 80, color, 1.3f);
		d.noGravity = true;
		NPC.rotation += 0.4f * (float)NPC.direction;
        return;
    }
}
