using System.Collections.Generic;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Buffs;

public class Shockwave : ModBuff
{
    private int fallStart_old = -1;
    public override void Update(Player player, ref int buffIndex)
    {
        if (Main.rand.NextBool(50))
        {
            int D = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
            Main.dust[D].noGravity = true;
            Main.dust[D].velocity.X *= 1.2f;
            Main.dust[D].velocity.X *= 1.2f;
        }
        if (Main.rand.NextBool(50))
        {
            int D2 = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
            Main.dust[D2].noGravity = true;
            Main.dust[D2].velocity.X *= -1.2f;
            Main.dust[D2].velocity.X *= 1.2f;
        }
        if (Main.rand.NextBool(50))
        {
            int D3 = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
            Main.dust[D3].noGravity = true;
            Main.dust[D3].velocity.X *= 1.2f;
            Main.dust[D3].velocity.X *= -1.2f;
        }
        if (Main.rand.NextBool(50))
        {
            int D4 = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
            Main.dust[D4].noGravity = true;
            Main.dust[D4].velocity.X *= -1.2f;
            Main.dust[D4].velocity.X *= -1.2f;
        }

        int sw = Main.screenWidth;
        int sh = Main.screenHeight;
        int sx = (int)Main.screenPosition.X;
        int sy = (int)Main.screenPosition.Y;


        if (fallStart_old == -1) fallStart_old = player.fallStart;
        int fall_dist = 0;
        if (player.velocity.Y == 0f) // detect landing from a fall
        {
            fall_dist = (int)(((int)(player.position.Y / 16f) - fallStart_old) * player.gravDir);
        }

        Vector2 p_pos = player.position + (new Vector2(player.width, player.height) / 2f);
        int numOfNPCs = 0;
        if (fall_dist > 3) // just fell
        {
            for (int o = 0; o < Main.npc.Length; o++)
            {
                // iterate through NPCs
                NPC N = Main.npc[o];
                var list = new List<NPC>();
                if (!N.active || N.dontTakeDamage || N.friendly || N.life < 1)
                {
                    continue;
                }

                if (N.position.X >= sx && N.position.X <= sx + sw && N.position.Y >= sy && N.position.Y <= sy + sh)
                {
                    list.Add(N);
                }

                var n_pos = new Vector2(N.position.X + (N.width * 0.5f), N.position.Y + (N.height * 0.5f)); // NPC location
                int HitDir = -1;
                if (n_pos.X > p_pos.X)
                {
                    HitDir = 1;
                }

                if (N.position.X >= sx && N.position.X <= sx + sw && N.position.Y >= sy && N.position.Y <= sy + sh)
                {
                    // on screen
                    numOfNPCs++;
                    int multiplier = 5;

                    //if (player.IsOnGround() && numOfNPCs == list.Count - 1)
                    //{
                    //    break;
                    //}

                    //if (player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && Main.hardMode)
                    //{
                    //    multiplier = 6;
                    //}
                    //else if (!player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && Main.hardMode)
                    //{
                    //    multiplier = 4;
                    //}
                    //else if (player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && !Main.hardMode)
                    //{
                    //    multiplier = 3;
                    //}
                    //else if (!player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && !Main.hardMode)
                    //{
                    //    multiplier = 2;
                    //}

                    N.StrikeNPC(new NPC.HitInfo {Damage = multiplier * fall_dist, Knockback = 5f, HitDirection = HitDir});
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, o, multiplier * fall_dist, 10f, HitDir); // for multiplayer support
                    }

                    

                    // optionally add debuff here
                } // END on screen
            } // END iterate through NPCs
        } // END just fell
        fallStart_old = player.fallStart;
    }
}
