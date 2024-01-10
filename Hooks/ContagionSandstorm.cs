using Avalon.Common;
using Avalon.Tiles.Contagion;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Hooks
{
    internal class ContagionSandstorm : ModHook
    {
        protected override void Apply()
        {
            //On_Sandstorm.EmitDust += On_Sandstorm_EmitDust;
            On_Sandstorm.ShouldSandstormDustPersist += On_Sandstorm_ShouldSandstormDustPersist;
        }

        private bool On_Sandstorm_ShouldSandstormDustPersist(On_Sandstorm.orig_ShouldSandstormDustPersist orig)
        {
            if (Sandstorm.Happening && Main.LocalPlayer.InModBiome<Biomes.ContagionDesert>())
            {
                return Main.bgDelay < 50;
            }
            return orig.Invoke();
        }

        //private void On_Sandstorm_EmitDust(On_Sandstorm.orig_EmitDust orig)
        //{
        //    if (Main.gamePaused)
        //    {
        //        return;
        //    }
        //    int sandTileCount = Main.SceneMetrics.SandTileCount;
        //    Player player = Main.player[Main.myPlayer];
        //    bool flag = Sandstorm.ShouldSandstormDustPersist();
        //    Sandstorm.HandleEffectAndSky(flag && Main.UseStormEffects);
        //    if (sandTileCount < 100 || (double)player.position.Y > Main.worldSurface * 16.0 || player.ZoneBeach)
        //    {
        //        return;
        //    }
        //    int maxValue = 1;
        //    if (!flag || Main.rand.Next(maxValue) != 0)
        //    {
        //        return;
        //    }
        //    int num = Math.Sign(Main.windSpeedCurrent);
        //    float num8 = Math.Abs(Main.windSpeedCurrent);
        //    if (num8 < 0.01f)
        //    {
        //        return;
        //    }
        //    float num9 = (float)num * MathHelper.Lerp(0.9f, 1f, num8);
        //    float num10 = 8000f / sandTileCount;
        //    float value = 3f / num10;
        //    value = MathHelper.Clamp(value, 0.77f, 1f);
        //    int num11 = (int)num10;
            
        //    float num12 = (float)Main.screenWidth / (float)Main.maxScreenW;
        //    int num13 = (int)(1000f * num12);
        //    float num14 = 20f * Sandstorm.Severity;
        //    float num15 = (float)num13 * (Main.gfxQuality * 0.5f + 0.5f) + (float)num13 * 0.1f - (float)Dust.SandStormCount;
        //    if (num15 <= 0f)
        //    {
        //        return;
        //    }
        //    float num2 = (float)Main.screenWidth + 1000f;
        //    float num3 = Main.screenHeight;
        //    Vector2 vector = Main.screenPosition + player.velocity;
        //    WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
        //    weightedRandom.Add(new Color(200, 160, 20, 180), Main.SceneMetrics.GetTileCount(53) + Main.SceneMetrics.GetTileCount(396) + Main.SceneMetrics.GetTileCount(397));
        //    weightedRandom.Add(new Color(103, 98, 122, 180), Main.SceneMetrics.GetTileCount(112) + Main.SceneMetrics.GetTileCount(400) + Main.SceneMetrics.GetTileCount(398));
        //    weightedRandom.Add(new Color(135, 43, 34, 180), Main.SceneMetrics.GetTileCount(234) + Main.SceneMetrics.GetTileCount(401) + Main.SceneMetrics.GetTileCount(399));
        //    weightedRandom.Add(new Color(213, 196, 197, 180), Main.SceneMetrics.GetTileCount(116) + Main.SceneMetrics.GetTileCount(403) + Main.SceneMetrics.GetTileCount(402));
        //    weightedRandom.Add(new Color(219, 200, 96, 180), Main.SceneMetrics.GetTileCount((ushort)ModContent.TileType<Snotsand>()) +
        //                    Main.SceneMetrics.GetTileCount((ushort)ModContent.TileType<Snotsandstone>()) +
        //                    Main.SceneMetrics.GetTileCount((ushort)ModContent.TileType<HardenedSnotsand>()));
        //    float num4 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
        //    float num5 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
        //    float amount = (value - 0.77f) / 0.230000019f;
        //    int maxValue2 = (int)MathHelper.Lerp(1f, 10f, amount);
        //    Vector2 position = default(Vector2);
        //    for (int i = 0; (float)i < num14; i++)
        //    {
        //        //if (!Main.rand.NextBool(num11 / 4))
        //        //{
        //        //    continue;
        //        //}
        //        position = new(Main.rand.NextFloat() * num2 - 500f, Main.rand.NextFloat() * -50f);
        //        if (Main.rand.Next(3) == 0 && num == 1)
        //        {
        //            position.X = Main.rand.Next(500) - 500;
        //        }
        //        else if (Main.rand.Next(3) == 0 && num == -1)
        //        {
        //            position.X = Main.rand.Next(500) + Main.screenWidth;
        //        }
        //        if (position.X < 0f || position.X > (float)Main.screenWidth)
        //        {
        //            position.Y += Main.rand.NextFloat() * num3 * 0.9f;
        //        }
        //        position += vector;
        //        int num6 = (int)position.X / 16;
        //        int num7 = (int)position.Y / 16;
        //        if (!WorldGen.InWorld(num6, num7, 10) || Main.tile[num6, num7] == null)
        //        {
        //            continue;
        //        }
        //        Tile tile = Main.tile[num6, num7];
        //        if (tile.WallType != 0)
        //        {
        //            continue;
        //        }
        //        for (int j = 0; j < 1; j++)
        //        {
        //            Dust dust = Main.dust[Dust.NewDust(position, 10, 10, 268)];
        //            dust.velocity.Y = 2f + Main.rand.NextFloat() * 0.2f;
        //            dust.velocity.Y *= dust.scale;
        //            dust.velocity.Y *= 0.35f;
        //            dust.velocity.X = num9 * 5f + Main.rand.NextFloat() * 1f;
        //            dust.velocity.X += num9 * num5 * 20f;
        //            dust.fadeIn += num5 * 0.2f;
        //            dust.velocity *= 1f + num4 * 0.5f;
        //            dust.color = weightedRandom;
        //            dust.velocity *= 1f + num4;
        //            dust.velocity *= value;
        //            dust.scale = 0.9f;
        //            num15 -= 1f;
        //            if (num15 <= 0f)
        //            {
        //                break;
        //            }
        //            if (Main.rand.Next(maxValue2) != 0)
        //            {
        //                j--;
        //                position += Utils.RandomVector2(Main.rand, -10f, 10f) + dust.velocity * -1.1f;
        //                num6 = (int)position.X / 16;
        //                num7 = (int)position.Y / 16;
        //                if (WorldGen.InWorld(num6, num7, 10) && Main.tile[num6, num7] != null)
        //                {
        //                    tile = Main.tile[num6, num7];
        //                    _ = ref tile.WallType;
        //                }
        //            }
        //        }
        //        if (num15 <= 0f)
        //        {
        //            break;
        //        }
        //    }
        //}
    }
}
