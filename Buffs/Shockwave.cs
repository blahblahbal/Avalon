using System.Collections.Generic;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Avalon.Common;
using Terraria.Graphics.CameraModifiers;
using Avalon.Particles;

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

        if (fallStart_old == -1) fallStart_old = player.fallStart;
        int fall_dist = 0;
        if (player.velocity.Y == 0f) // detect landing from a fall
        {
            fall_dist = (int)(((int)(player.position.Y / 16f) - fallStart_old) * player.gravDir);
            if (fall_dist > 30) fall_dist = 30;
        }

        Vector2 p_pos = player.position + (new Vector2(player.width, player.height) / 2f);
        int numOfNPCs = 0;
        if (fall_dist > 3) // just fell
        {
            float Radius = fall_dist * 35;
            for (int o = 0; o < Main.npc.Length; o++)
            {
                // iterate through NPCs
                NPC N = Main.npc[o];
                var list = new List<NPC>();
                if (!N.active || N.dontTakeDamage || N.friendly || N.life < 1 || N.type == NPCID.TargetDummy)
                {
                    continue;
                }

                if (N.Center.Distance(player.Center) < Radius)
                {
                    list.Add(N);
                }

                var n_pos = N.Center; // NPC location
                int HitDir = -1;
                if (n_pos.X > p_pos.X)
                {
                    HitDir = 1;
                }

                if (N.Center.Distance(player.Center) < Radius)
                {
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

                    int dmg = N.StrikeNPC(new NPC.HitInfo {Damage = (int)(multiplier * fall_dist * -((N.Center.Distance(player.Center) / Radius) - 1)) * 2, Knockback = fall_dist * -((N.Center.Distance(player.Center) / Radius) - 1) * 2.7f, HitDirection = HitDir});
                    player.addDPS(dmg);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, o, dmg, 10f, HitDir); // for multiplayer support
                    }

                    

                    // optionally add debuff here
                } // END on screen
            } // END iterate through NPCs

            var Sound = SoundEngine.PlaySound(SoundID.Item14, player.position);
            ParticleSystem.AddParticle(new ShockwaveParticle(), player.Center + new Vector2(0,(player.height * 0.5f * player.gravDir)),Vector2.Zero,default,Radius);
            if (SoundEngine.TryGetActiveSound(Sound, out ActiveSound sound) && sound != null && sound.IsPlaying)
            {
                sound.Volume = MathHelper.Clamp(fall_dist / 7f,0.2f,3);
            }
            if (player.whoAmI == Main.myPlayer && ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(player.Center, new Vector2(Main.rand.NextFloat(-0.3f,0.3f), fall_dist / 2f), 1f, 3f, 15, 300f, player.name);
                Main.instance.CameraModifiers.Add(modifier);
            }
        } // END just fell
        fallStart_old = player.fallStart;
    }
}
