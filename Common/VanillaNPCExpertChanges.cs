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
            if (Main.expertMode)
            {
                if (npc.netID == NPCID.BlazingWheel)
                {
                    npc.scale *= 1.5f;
                    npc.Size *= 1.5f;
                }
                //                                    ⛧ SCARY NUMBR OMGOMG ⛧
                if (npc.netID is NPCID.BurningSphere or NPCID.ChaosBall or NPCID.WaterSphere or NPCID.VileSpit or NPCID.ChaosBallTim) // making eow vile spit not destroyable is a bit much lol // Was unintentional lmao
                {
                    npc.dontTakeDamage = true;
                }
            }
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode)
            {
                if (Main.rand.NextBool(5) && Main.expertMode && npc.netID is NPCID.BlackSlime or NPCID.MotherSlime or NPCID.BabySlime)
                {
                    target.AddBuff(BuffID.Blackout, 60 * 10);
                }
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            OnHitByAnything(npc,player,hit,damageDone);
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            OnHitByAnything(npc, projectile, hit, damageDone);
        }
        void OnHitByAnything(NPC npc, Entity attacker, NPC.HitInfo hit, int damageDone)
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
            Player TargetPlayer = Main.player[Main.myPlayer];
            if (npc.target is > -1 and < 256)
            {
                 TargetPlayer = Main.player[npc.target];
            }
            if (Main.expertMode)
            {
                #region Slimes
                if (npc.netID is NPCID.YellowSlime or NPCID.RedSlime or NPCID.PurpleSlime or NPCID.BlackSlime or
                    NPCID.CorruptSlime or NPCID.Slimeling or NPCID.Slimer2 or NPCID.ToxicSludge or NPCID.Crimslime or NPCID.BabySlime) // lots of slimes
                {
                    npc.ai[0]++;
                }
                if(npc.aiStyle == NPCAIStyleID.Slime)
                {
                    if (npc.life <= npc.lifeMax / 3)
                    {
                        npc.ai[0]+= 2;
                    }
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
                #region Blazing Wheel
                if (npc.netID == NPCID.BlazingWheel)
                {
                    npc.ReflectProjectiles(npc.Hitbox);
                }
                #endregion Blazing Wheel
            }
        }
        public override void OnKill(NPC npc)
        {
            if (Main.expertMode)
            {
                //if (npc.netID == NPCID.SandSlime)
                //{
                //    for (int i = 0; i < Main.rand.Next(3, 10); i++)
                //    {
                //        Projectile p = Projectile.NewProjectileDirect(npc.GetSource_FromThis(), npc.Center, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-12, -8)), ProjectileID.SandBallFalling, npc.damage, 0, Main.myPlayer,2f);
                //        p.friendly = false;
                //        p.timeLeft = 300;
                //    }
                //}
            }
        }
    }
}
