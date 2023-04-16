using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Avalon.Common
{
    public class VanillaNPCExpertChanges : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.netID == NPCID.BlazingWheel)
            {
                npc.scale *= 1.5f;
                npc.Size *= 1.5f;
            }
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if(Main.rand.NextBool(5) && Main.expertMode && npc.netID is -6 or 16 or -5) 
            {
                target.AddBuff(BuffID.Blackout, 60 * 10);
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            #region Cursed Skull
            if (npc.netID == NPCID.CursedSkull && npc.ai[2] is > -160 and < 100)
            {
                npc.ai[2] -= damageDone * 2;
            }
            if (npc.netID == NPCID.CursedSkull && npc.ai[2] > 100)
            {
                npc.ai[2] = -100;
            }
            #endregion Cursed Skull
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            #region Cursed Skull
            if (npc.netID == NPCID.CursedSkull && npc.ai[2] is > -160 and < 100)
            {
                npc.ai[2] -= damageDone * 2;
            }
            if (npc.netID == NPCID.CursedSkull && npc.ai[2] > 100)
            {
                npc.ai[2] = 110;
            }
            #endregion Cursed Skull
        }
        public override void AI(NPC npc)
        {
            Player TargetPlayer = Main.player[npc.target];
            if (Main.expertMode)
            {
                #region Slimes
                if (npc.netID is 25 or 30 or 33)
                {
                    npc.dontTakeDamage = true;
                }
                if (npc.netID is -9 or -8 or -7 or -6 or 81 or -1 or 200 or -2 or 180 or 141) // lots of slimes
                {
                    npc.ai[0]++;
                }
                #endregion Slimes
                #region Cursed Skull
                if (npc.netID == NPCID.CursedSkull)
                {
                    npc.alpha = (int)MathHelper.Clamp(npc.Center.Distance(TargetPlayer.Center) * 0.65f,0,255);

                    npc.ai[2]++;
                    if (npc.ai[2] >= 20)
                    {
                        Dust CursedSkullDust = Dust.NewDustPerfect(npc.Center + new Vector2(-8 * npc.spriteDirection, -2).RotatedBy(npc.rotation), DustID.UnusedWhiteBluePurple, Vector2.Zero);
                        CursedSkullDust.noGravity = true;
                    }
                    if (npc.ai[2] == 100)
                    {
                        npc.aiStyle = -1;
                        npc.velocity = npc.Center.DirectionTo(TargetPlayer.Center) * 22;

                        SoundStyle CursedSkullDash = new SoundStyle("Terraria/Sounds/Zombie_53");
                        CursedSkullDash.MaxInstances = 10;

                        SoundEngine.PlaySound(CursedSkullDash, npc.Center);
                        for (int i = 0; i < 30; i++)
                        {
                            int D = Dust.NewDust(npc.Center, 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 3);
                            //Main.dust[D].color = new Color(255, 255, 255, 0);
                            Main.dust[D].noGravity = !Main.rand.NextBool(5);
                            Main.dust[D].noLightEmittence = true;
                            Main.dust[D].fadeIn = Main.rand.NextFloat(0f, 2f);
                            Main.dust[D].velocity = new Vector2(Main.rand.NextFloat(2, 5), 0).RotatedBy(MathHelper.Pi / 15 * i);
                        }
                    }

                    if (npc.ai[2] >= 100)
                    {
                        Dust CursedSkullDust = Dust.NewDustPerfect(npc.Center + Main.rand.NextVector2Circular(npc.width / 2, npc.height / 2), DustID.UnusedWhiteBluePurple, Vector2.Zero, 0, default, 2);
                        CursedSkullDust.noGravity = true;
                        CursedSkullDust.fadeIn = 2.5f;
                    }
                    if (npc.ai[2] >= 110)
                    {
                        npc.velocity *= 0.5f;
                        npc.aiStyle = NPCAIStyleID.CursedSkull;
                        npc.ai[2] = -160;
                    }
                }
                #endregion Cursed Skull
                #region Meteor Head
                if(npc.netID == NPCID.MeteorHead)
                {
                    npc.position += npc.velocity * 2;
                }
                #endregion Meteor Head
            }
        }
    }
}
