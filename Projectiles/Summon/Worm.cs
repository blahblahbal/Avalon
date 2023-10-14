using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon
{
    public class Worm : ModProjectile
    {
        public override void SetDefaults()
        {
            Rectangle dims = this.GetDims();
            Projectile.width = dims.Width * 10 / 32;
            Projectile.height = dims.Height * 10 / 32 / Main.projFrames[Projectile.type];
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            Rectangle myRect = Projectile.Hitbox;
            if (Projectile.owner != Main.myPlayer)
            {
                return;
            }
            for (int npcIndex = 0; npcIndex < 200; npcIndex++)
            {
                NPC npc = Main.npc[npcIndex];
                if (!npc.active || npc.dontTakeDamage || ((!Projectile.friendly || (npc.friendly && (npc.type != NPCID.Guide || Projectile.owner >= 255 || !player.killGuide) && (npc.type != 54 || Projectile.owner >= 255 || !player.killClothier))) && (!Projectile.hostile || !npc.friendly || npc.dontTakeDamageFromHostiles)) || (Projectile.owner >= 0 && npc.immune[Projectile.owner] != 0 && Projectile.maxPenetrate != 1) || (!npc.noTileCollide && Projectile.ownerHitCheck))
                {
                    continue;
                }
                bool stickingToNPC;
                if (npc.type == NPCID.SolarCrawltipedeTail)
                {
                    Rectangle rect = npc.Hitbox;
                    int num31 = 8;
                    rect.X -= num31;
                    rect.Y -= num31;
                    rect.Width += num31 * 2;
                    rect.Height += num31 * 2;
                    stickingToNPC = Projectile.Colliding(myRect, rect);
                }
                else
                {
                    stickingToNPC = Projectile.Colliding(myRect, npc.Hitbox);
                }
                if (!stickingToNPC)
                {
                    continue;
                }
                if (npc.reflectsProjectiles && Projectile.CanBeReflected())
                {
                    npc.ReflectProjectile(Projectile);
                    break;
                }
                //if (Projectile.ai[0] == 0)
                //{
                //    target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Wormed>(), 8 * 60);
                //}
                Projectile.ai[0] = 1f;
                Projectile.ai[1] = npcIndex;
                Projectile.velocity = (npc.Center - Projectile.Center) * 0.75f;
                Projectile.netUpdate = true;
                var array2 = (Point[])(object)new Point[5]; // 5 = maximum sticking in target
                int projCount = 0;
                for (int projIndex = 0; projIndex < 1000; projIndex++)
                {
                    Projectile proj = Main.projectile[projIndex];
                    if (projIndex != Projectile.whoAmI && proj.active && proj.owner == Main.myPlayer && proj.type == Projectile.type && proj.ai[0] == 1f && proj.ai[1] == npcIndex)
                    {
                        array2[projCount++] = new Point(projIndex, proj.timeLeft);
                        if (projCount >= array2.Length)
                        {
                            break;
                        }
                    }
                }
                if (projCount < array2.Length)
                {
                    continue;
                }
                int num30 = 0;
                for (int i = 1; i < array2.Length; i++)
                {
                    if (array2[i].Y < array2[num30].Y)
                    {
                        num30 = i;
                    }
                }
                Main.projectile[array2[num30].X].Kill();
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return null;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 1f)
            {
                Vector2 center3 = Projectile.Center;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
                int num930 = 5 * Projectile.MaxUpdates;
                bool flag39 = false;
                bool flag40 = false;
                Projectile.localAI[0]++;
                if (Projectile.localAI[0] % 30f == 0f)
                {
                    flag40 = true;
                }
                int num931 = (int)Projectile.ai[1];
                if (Projectile.localAI[0] >= 60 * num930)
                {
                    flag39 = true;
                }
                else if (num931 is < 0 or >= 200)
                {
                    flag39 = true;
                }
                else if (Main.npc[num931].active && !Main.npc[num931].dontTakeDamage)
                {
                    Projectile.Center = Main.npc[num931].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[num931].gfxOffY;
                    if (flag40)
                    {
                        Main.npc[num931].HitEffect(0, 1.0);
                    }
                }
                else
                {
                    flag39 = true;
                }
                if (flag39)
                {
                    Projectile.Kill();
                }
            }
        }
    }
}
