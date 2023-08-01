using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Items.Accessories.Hardmode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common; 

internal class AvalonGlobalProjectile : GlobalProjectile
{
    
    public override bool PreAI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.TerraBlade2 && projectile.localAI[0] == 0)
        {
            projectile.localAI[0] = 1;
            SoundEngine.PlaySound(SoundID.Item8, projectile.position);
            return true;
        }
        if (projectile.aiStyle == 7)
        {
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            float xpos = mountedCenter.X - projectile.Center.X;
            float ypos = mountedCenter.Y - projectile.Center.Y;
            float distance = (float)Math.Sqrt(xpos * xpos + ypos * ypos);
            float distMod = 1f;
            if (!Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().HookBonus && projectile.ai[2] < 14)
            {
                distMod = 1.25f;
            }
            if (projectile.ai[0] == 1 && projectile.ai[2] < 14)
            {
                //distance

                //if ((distance > 300f * distMod - 300f && projectile.type == 13) || (distance > 400f * distMod - 400f && projectile.type == 32) ||
                //   (distance > 440f * distMod - 440f && projectile.type == 73) || (distance > 440f * distMod - 440f && projectile.type == 74) ||
                //   (distance > 375f * distMod - 375f && projectile.type == 165) || (distance > 350f * distMod - 350f && projectile.type == 256) ||
                //   (distance > 500f * distMod - 500f && projectile.type == 315) || (distance > 550f * distMod - 550f && projectile.type == 322) ||
                //   (distance > 400f * distMod - 400f && projectile.type == 331) || (distance > 550f * distMod - 550f && projectile.type == 332) ||
                //   (distance > 400f * distMod - 400f && projectile.type == 372) || (distance > 300f * distMod - 300f && projectile.type == 396) ||
                //   (distance > 550f * distMod - 550f && projectile.type >= 646 && projectile.type <= 649) ||
                //   (distance > 600f * distMod - 600f && projectile.type == 652) || (distance > 300f * distMod - 300f && projectile.type == 865) ||
                //   (distance > 500f * distMod - 500f && projectile.type == 935) ||
                //   (distance > 480f * distMod - 480f && projectile.type >= 486 && projectile.type <= 489) ||
                //   (distance > 500f * distMod - 500f && projectile.type == 446))
                //{
                //    projectile.ai[0] = 1f;
                //}
                if ((distance > 300f * distMod && projectile.type == 13) || (distance > 400f * distMod && projectile.type == 32) ||
                    (distance > 440f * distMod && projectile.type == 73) || (distance > 440f * distMod && projectile.type == 74) ||
                    (distance > 375f * distMod && projectile.type == 165) || (distance > 350f * distMod && projectile.type == 256) ||
                    (distance > 500f * distMod && projectile.type == 315) || (distance > 550f * distMod && projectile.type == 322) ||
                    (distance > 400f * distMod && projectile.type == 331) || (distance > 550f * distMod && projectile.type == 332) ||
                    (distance > 400f * distMod && projectile.type == 372) || (distance > 300f * distMod && projectile.type == 396) ||
                    (distance > 550f * distMod && projectile.type >= 646 && projectile.type <= 649) ||
                    (distance > 600f * distMod && projectile.type == 652) || (distance > 300f * distMod && projectile.type == 865) ||
                    (distance > 500f * distMod && projectile.type == 935) ||
                    (distance > 480f * distMod && projectile.type >= 486 && projectile.type <= 489) ||
                    (distance > 500f * distMod && projectile.type == 446))
                {
                    projectile.ai[0] = 0;
                    projectile.ai[2]++;
                    if (projectile.ai[2] >= 14)
                    {
                        projectile.ai[0] = 1;
                    }
                }
                else if (projectile.type >= 230 && projectile.type <= 235)
                {
                    int num18 = 300 + (projectile.type - 230) * 30;
                    num18 = (int)(num18 * distMod);
                    if (distance > (float)num18)
                    {
                        projectile.ai[0] = 0;
                        projectile.ai[2]++;
                        if (projectile.ai[2] >= 14)
                        {
                            projectile.ai[0] = 1;
                        }
                    }
                }
                else if (projectile.type == 753)
                {
                    int num19 = (int)(420 * distMod - 420);
                    if (distance > num19)
                    {
                        projectile.ai[0] = 0;
                        projectile.ai[2]++;
                        if (projectile.ai[2] >= 14)
                        {
                            projectile.ai[0] = 1;
                        }
                    }
                }
            }
        }
        return base.PreAI(projectile);
    }

    public override void PostAI(Projectile projectile)
    {
        if ((projectile.type != 10 && projectile.type != 145 /* && projectile.type != ModContent.ProjectileType<Projectiles.LimeSolution>()*/) || projectile.owner != Main.myPlayer)
        {
            return;
        }
        int num = (int)(projectile.Center.X / 16f);
        int num2 = (int)(projectile.Center.Y / 16f);
        bool flag = projectile.type == 10;
        for (int i = num - 1; i <= num + 1; i++)
        {
            for (int j = num2 - 1; j <= num2 + 1; j++)
            {
                if (projectile.type == ProjectileID.PureSpray || projectile.type == ProjectileID.PurificationPowder)
                {
                    AvalonWorld.ConvertFromThings(i, j, 0, !flag);
                }
                //if (projectile.type == ModContent.ProjectileType<Projectiles.LimeSolution>())
                //{
                //    AvalonWorld.ConvertFromThings(i, j, 1, !flag);
                //}
                NetMessage.SendTileSquare(-1, i, j, 1, 1);
            }
        }
    }
    //public override void UseGrapple(Player player, ref int type)
    //{
    //    if (ProjectileID.Sets.SingleGrappleHook[type])
    //    {

    //    }
    //}
    public override void AI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.TerraBlade2)
        {
            if (projectile.localAI[0] == 1)
            {
                projectile.localAI[0] = 2;
                SoundEngine.PlaySound(SoundID.Item8, projectile.position);
            }
        }
        if (projectile.type == ProjectileID.PaladinsHammerFriendly)
        {
            if(Main.timeForVisualEffects % 2 == 0 && projectile.ai[1] != 0 && projectile.timeLeft > 3590)
            {
                ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
                particleOrchestraSettings.PositionInWorld = projectile.Center;
                particleOrchestraSettings.MovementVector = projectile.velocity * 1f;
                ParticleOrchestraSettings settings = particleOrchestraSettings;
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PaladinsHammer, settings, projectile.owner);
            }
        }
        if (!projectile.npcProj && !projectile.noEnchantments && !projectile.noEnchantmentVisuals && (projectile.DamageType == DamageClass.Melee || ProjectileID.Sets.IsAWhip[projectile.type]))
        {
            Vector2 boxPosition = projectile.position;
            int boxWidth = projectile.width;
            int boxHeight = projectile.height;
            if (projectile.aiStyle == 190 || projectile.aiStyle == 191)
            {
                for (float num = -(float)Math.PI / 4f; num <= (float)Math.PI / 4f; num += (float)Math.PI / 2f)
                {
                    Rectangle r = Utils.CenteredRectangle(projectile.Center + (projectile.rotation + num).ToRotationVector2() * 70f * projectile.scale, new Vector2(60f * projectile.scale, 60f * projectile.scale));
                    EmitAvalonEnchants(r.TopLeft(), r.Width, r.Height, projectile);
                }
            }
            else if (ProjectileID.Sets.IsAWhip[projectile.type])
            {
                projectile.WhipPointsForCollision.Clear();
                Projectile.FillWhipControlPoints(projectile, projectile.WhipPointsForCollision);
                Vector2 vector = projectile.WhipPointsForCollision[projectile.WhipPointsForCollision.Count - 1];
                EmitAvalonEnchants(new Vector2(vector.X - (float)(projectile.width / 2), vector.Y - (float)(projectile.height / 2)), projectile.width, projectile.height, projectile);
            }
            else
            {
                EmitAvalonEnchants(boxPosition, boxWidth, boxHeight, projectile);
            }
        }
    }

    public void EmitAvalonEnchants(Vector2 boxPosition, int boxWidth, int boxHeight, Projectile projectile)
    {
        if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().PathogenImbue)
        {
            if (Main.rand.NextBool(2))
            {
                int num5 = Dust.NewDust(boxPosition, boxWidth, boxHeight, ModContent.DustType<PathogenDust>(), projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 128, default, 1.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.7f;
                Main.dust[num5].velocity.Y -= 0.5f;
            }
        }
        if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().FrostGauntlet)
        {
            if (Main.rand.NextBool(2))
            {
                int num5 = Dust.NewDust(boxPosition, boxWidth, boxHeight, DustID.IceTorch, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default, 2);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.7f;
                Main.dust[num5].velocity.Y -= 0.5f;
            }
        }
    }
    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!projectile.npcProj && !projectile.noEnchantments && (projectile.DamageType == DamageClass.Melee || ProjectileID.Sets.IsAWhip[projectile.type]))
        {
            if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().FrostGauntlet)
            {
                target.AddBuff(BuffID.Frostburn2, 60 * 4);
            }
            if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().PathogenImbue)
            {
                target.AddBuff(ModContent.BuffType<Pathogen>(), 60 * Main.rand.Next(3, 7));
            }
        }
            base.OnHitNPC(projectile, target, hit, damageDone);
    }

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[44] = 6;
        ProjectileID.Sets.TrailingMode[44] = 2;
        ProjectileID.Sets.TrailCacheLength[45] = 6;
        ProjectileID.Sets.TrailingMode[45] = 2;
    }
    public override bool PreDraw(Projectile projectile, ref Color lightColor)
    {
        if ((ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement))
        {
            if (projectile.type is 45 or 44) // Demon Scythes
            {
                float rotationMultiplier = 0.7f;

                Texture2D texture = Mod.Assets.Request<Texture2D>("Assets/Vanilla/Projectiles/DemonScythe").Value;
                int frameHeight = texture.Height;
                Rectangle frame = new Rectangle(0, frameHeight * projectile.frame, texture.Width, frameHeight);
                int length = ProjectileID.Sets.TrailCacheLength[projectile.type];
                for (int i = 0; i < length; i++)
                {
                    //float multiply = ((float)(length - i) / length) * projectile.Opacity * 0.2f;
                    float multiply = (float)(length - i) / length * 0.5f;
                    Main.EntitySpriteDraw(texture, projectile.oldPos[i] - Main.screenPosition + (projectile.Size / 2f), frame, new Color(128, 128, 255, 128) * multiply, projectile.oldRot[i] * rotationMultiplier, new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);
                }

                Main.EntitySpriteDraw(texture, projectile.position - Main.screenPosition + (projectile.Size / 2f), frame, new Color(255, 255, 255, 175) * 0.7f, projectile.rotation * rotationMultiplier, new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);
                return false;
            }
        }
        return base.PreDraw(projectile, ref lightColor);
    }
}
