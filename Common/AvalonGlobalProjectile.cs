using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Tiles.GemTrees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Tiles.Contagion;
using Avalon.NPCs.Bosses.PreHardmode;

namespace Avalon.Common;

internal class AvalonGlobalProjectile : GlobalProjectile
{
    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        if (source is EntitySource_Parent parent && parent.Entity is NPC npc && npc.HasBuff(BuffID.Cursed))
        {
            projectile.Kill();
        }
    }
    public static int FindClosestHostile(Vector2 pos, float dist)
    {
        int closest = -1;
        float last = dist;
        for (int i = 0; i < Main.projectile.Length; i++)
        {
            Projectile p = Main.projectile[i];
            if (!p.active || !p.hostile)
            {
                continue;
            }

            if (Vector2.Distance(pos, p.Center) < last)
            {
                last = Vector2.Distance(pos, p.Center);
                closest = i;
            }
        }

        return closest;
    }
    public override void OnKill(Projectile projectile, int timeLeft)
    {
        if (projectile.type == ProjectileID.WorldGlobe && Main.player[projectile.owner].InModBiome<Biomes.Contagion>())
        {
            ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG++;
            if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG > 1)
            {
                ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG = 0;
            }
        }
        base.OnKill(projectile, timeLeft);
    }
    public override bool CanHitPlayer(Projectile projectile, Player target)
    {
        if (target.GetModPlayer<AvalonPlayer>().TrapImmune && (projectile.type == ProjectileID.PoisonDartTrap || projectile.type == ProjectileID.VenomDartTrap ||
            projectile.type == ProjectileID.GasTrap || projectile.type == ProjectileID.Explosives || projectile.type == ProjectileID.Landmine ||
            projectile.type == ProjectileID.SpearTrap || projectile.type == ProjectileID.FlamesTrap || projectile.type == ProjectileID.FlamethrowerTrap ||
            projectile.type == ProjectileID.SpikyBallTrap || projectile.type == ProjectileID.GeyserTrap || ProjectileID.Sets.IsAGravestone[projectile.type] ||
            projectile.type == ProjectileID.Boulder || projectile.type == ProjectileID.BouncyBoulder || projectile.type == ProjectileID.MiniBoulder ||
            projectile.type == ProjectileID.LifeCrystalBoulder))
        {
            return false;
        }
        return base.CanHitPlayer(projectile, target);
    }

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
            // for some reason checking if NOT the hookbonus bool works; also the 14 is arbitrary but ends up working to be 50% boost for each hook
            if (!Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().HookBonus && projectile.ai[2] < 14)
            {
                distMod = 1.25f;
            }
            if (projectile.ai[0] == 1 && projectile.ai[2] < 14)
            {
                if ((distance > 300f * distMod && projectile.type == ProjectileID.Hook) || (distance > 400f * distMod && projectile.type == ProjectileID.IvyWhip) ||
                    (distance > 440f * distMod && projectile.type == ProjectileID.DualHookBlue) || (distance > 440f * distMod && projectile.type == ProjectileID.DualHookRed) ||
                    (distance > 375f * distMod && projectile.type == ProjectileID.Web) || (distance > 350f * distMod && projectile.type == ProjectileID.SkeletronHand) ||
                    (distance > 500f * distMod && projectile.type == ProjectileID.BatHook) || (distance > 550f * distMod && projectile.type == ProjectileID.WoodHook) ||
                    (distance > 400f * distMod && projectile.type == ProjectileID.CandyCaneHook) || (distance > 550f * distMod && projectile.type == ProjectileID.ChristmasHook) ||
                    (distance > 400f * distMod && projectile.type == ProjectileID.FishHook) || (distance > 300f * distMod && projectile.type == ProjectileID.SlimeHook) ||
                    (distance > 550f * distMod && projectile.type >= ProjectileID.LunarHookSolar && projectile.type <= ProjectileID.LunarHookStardust) ||
                    (distance > 600f * distMod && projectile.type == ProjectileID.StaticHook) || (distance > 300f * distMod && projectile.type == ProjectileID.SquirrelHook) ||
                    (distance > 500f * distMod && projectile.type == ProjectileID.QueenSlimeHook) ||
                    (distance > 480f * distMod && projectile.type >= ProjectileID.TendonHook && projectile.type <= ProjectileID.WormHook) ||
                    (distance > 500f * distMod && projectile.type == ProjectileID.AntiGravityHook))
                {
                    projectile.ai[0] = 0;
                    projectile.ai[2]++;
                    if (projectile.ai[2] >= 14)
                    {
                        projectile.ai[0] = 1;
                    }
                }
                else if (projectile.type >= ProjectileID.GemHookAmethyst && projectile.type <= ProjectileID.GemHookDiamond)
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
                else if (projectile.type == ProjectileID.AmberHook)
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
    public override void AI(Projectile projectile)
    {
        #region fertilizer fix
        if (projectile.aiStyle == 6)
        {
            bool flag23 = projectile.type == 1019;
            bool flag34 = Main.myPlayer == projectile.owner;
            if (flag23)
            {
                flag34 = Main.netMode != NetmodeID.MultiplayerClient;
            }
            if (flag34 && flag23)
            {
                int num988 = (int)(projectile.position.X / 16f) - 1;
                int num999 = (int)((projectile.position.X + projectile.width) / 16f) + 2;
                int num1010 = (int)(projectile.position.Y / 16f) - 1;
                int num1021 = (int)((projectile.position.Y + projectile.height) / 16f) + 2;
                if (num988 < 0)
                {
                    num988 = 0;
                }
                if (num999 > Main.maxTilesX)
                {
                    num999 = Main.maxTilesX;
                }
                if (num1010 < 0)
                {
                    num1010 = 0;
                }
                if (num1021 > Main.maxTilesY)
                {
                    num1021 = Main.maxTilesY;
                }
                Vector2 vector57 = default;
                for (int num1032 = num988; num1032 < num999; num1032++)
                {
                    for (int num1043 = num1010; num1043 < num1021; num1043++)
                    {
                        vector57.X = num1032 * 16;
                        vector57.Y = num1043 * 16;
                        if (!(projectile.position.X + projectile.width > vector57.X) || !(projectile.position.X < vector57.X + 16f) || !(projectile.position.Y + projectile.height > vector57.Y) || !(projectile.position.Y < vector57.Y + 16f) || !Main.tile[num1032, num1043].HasTile)
                        {
                            continue;
                        }
                        Tile tile = Main.tile[num1032, num1043];
                        if (tile.TileType == ModContent.TileType<ContagionSapling>())
                        {
                            //if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
                            //{
                            //    ContagionSapling.AttemptToGrowContagionTreeFromSapling(num1032, num1043);
                            //}
                            ContagionSapling.AttemptToGrowContagionTreeFromSapling(num1032, num1043);
                        }

                        if (tile.TileType == ModContent.TileType<TourmalineSapling>())
                        {
                            if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
                            {
                                TourmalineSapling.AttemptToGrowTourmalineFromSapling(num1032, num1043, underground: false);
                            }
                            TourmalineSapling.AttemptToGrowTourmalineFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
                        }
                        if (tile.TileType == ModContent.TileType<PeridotSapling>())
                        {
                            if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
                            {
                                PeridotSapling.AttemptToGrowPeridotFromSapling(num1032, num1043, underground: false);
                            }
                            PeridotSapling.AttemptToGrowPeridotFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
                        }
                        if (tile.TileType == ModContent.TileType<ZirconSapling>())
                        {
                            if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
                            {
                                ZirconSapling.AttemptToGrowZirconFromSapling(num1032, num1043, underground: false);
                            }
                            ZirconSapling.AttemptToGrowZirconFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
                        }
                    }
                }
            }
        }
        #endregion
        #region terra blade sound change
        if (projectile.type == ProjectileID.TerraBlade2)
        {
            if (projectile.localAI[0] == 1)
            {
                projectile.localAI[0] = 2;
                SoundEngine.PlaySound(SoundID.Item8, projectile.position);
            }
        }
        #endregion
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
        #region avalon flasks
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
        #endregion
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
    public static Vector2 RotateAboutOrigin(Vector2 point, float rotation)
    {
        if (rotation < 0f)
        {
            rotation += 12.566371f;
        }

        Vector2 value = point;
        if (value == Vector2.Zero)
        {
            return point;
        }

        float num = (float)Math.Atan2(value.Y, value.X);
        num += rotation;
        return value.Length() * new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
    }
}
