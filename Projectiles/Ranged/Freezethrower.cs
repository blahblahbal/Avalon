using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Projectiles.Ranged;

public class Freezethrower : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 7;
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Flames);
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 6 / 16;
        Projectile.height = dims.Height * 6 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.alpha = 255;
        Projectile.penetrate = 4;
        Projectile.MaxUpdates = 2;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ArmorPenetration = 30;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        DrawProj_Flamethrower(Projectile);
        return true;
    }
    private static void DrawProj_Flamethrower(Projectile proj)
    {
        bool flag = proj.ai[0] == 1f;
        float num = 60f;
        float num2 = 12f;
        float fromMax = num + num2;
        Texture2D value = ModContent.Request<Texture2D>("ExxoAvalonOrigins/Projectiles/Ranged/Freezethrower").Value;
        Color transparent = Color.Transparent;
        Color color = new Color(87, 85, 233, 200);
        Color color2 = new Color(116, 243, 255, 70);
        Color color3 = Color.Lerp(new Color(87, 85, 233, 100), color2, 0.25f);
        Color color4 = new Color(227, 227, 242, 100);
        float num3 = 0.35f;
        float num4 = 0.7f;
        float num5 = 0.85f;
        float num6 = (proj.localAI[0] > num - 10f) ? 0.175f : 0.2f;
        if (flag)
        {
            color = new Color(95, 120, 255, 200);
            color2 = new Color(50, 180, 255, 70);
            color3 = new Color(95, 160, 255, 100);
            color4 = new Color(33, 125, 202, 100);
        }
        color *= 0.2f;
        color2 *= 0.2f;
        color4 *= 0.2f;
        int verticalFrames = 7;
        float num7 = Utils.Remap(proj.localAI[0], num, fromMax, 1f, 0f, true);
        float num8 = Math.Min(proj.localAI[0], 20f);
        float num9 = Utils.Remap(proj.localAI[0], 0f, fromMax, 0f, 1f, true);
        float num10 = Utils.Remap(num9, 0.2f, 0.5f, 0.25f, 1f, true);
        Rectangle rectangle = (!flag) ? value.Frame(1, verticalFrames, 0, 3, 0, 0) : value.Frame(1, verticalFrames, 0, (int)Utils.Remap(num9, 0.5f, 1f, 3f, 5f, true), 0, 0);
        if (num9 >= 1f)
        {
            return;
        }
        for (int i = 0; i < 2; i++)
        {
            for (float num11 = 1f; num11 >= 0f; num11 -= num6)
            {
                Color value2 = (num9 < 0.1f) ? Color.Lerp(Color.Transparent, color, Utils.GetLerpValue(0f, 0.1f, num9, true)) : ((num9 < 0.2f) ? Color.Lerp(color, color2, Utils.GetLerpValue(0.1f, 0.2f, num9, true)) : ((num9 < num3) ? color2 : ((num9 < num4) ? Color.Lerp(color2, color3, Utils.GetLerpValue(num3, num4, num9, true)) : ((num9 < num5) ? Color.Lerp(color3, color4, Utils.GetLerpValue(num4, num5, num9, true)) : ((num9 >= 1f) ? Color.Transparent : Color.Lerp(color4, Color.Transparent, Utils.GetLerpValue(num5, 1f, num9, true)))))));
                float num12 = (1f - num11) * Utils.Remap(num9, 0f, 0.2f, 0f, 1f, true);
                Vector2 vector = proj.Center - Main.screenPosition + proj.velocity * (0f - num8) * num11;
                Color color5 = value2 * num12;
                Color color6 = color5;
                if (flag)
                {
                    color6.G /= 2;
                    color6.B /= 2;
                    color6.A = (byte)Math.Min((float)color5.A + 80f * num12, 255f);
                    Utils.Remap(proj.localAI[0], 20f, fromMax, 0f, 1f, true);
                }
                float num13 = 1f / num6 * (num11 + 1f);
                float num14 = proj.rotation + num11 * 1.5707964f + Main.GlobalTimeWrappedHourly * num13 * 2f;
                float num15 = proj.rotation - num11 * 1.5707964f - Main.GlobalTimeWrappedHourly * num13 * 2f;
                if (i != 0)
                {
                    if (i == 1)
                    {
                        if (!flag)
                        {
                            Main.EntitySpriteDraw(value, vector + proj.velocity * (0f - num8) * num6 * 0.2f, new Rectangle?(rectangle), color5 * num7 * 0.25f, num14 + 1.5707964f, rectangle.Size() / 2f, num10 * 0.75f, SpriteEffects.None, 0f);
                            Main.EntitySpriteDraw(value, vector, new Rectangle?(rectangle), color5 * num7, num15 + 1.5707964f, rectangle.Size() / 2f, num10 * 0.75f, SpriteEffects.None, 0f);
                        }
                    }
                }
                else
                {
                    Main.EntitySpriteDraw(value, vector + proj.velocity * (0f - num8) * num6 * 0.5f, new Rectangle?(rectangle), color6 * num7 * 0.25f, num14 + 0.7853982f, rectangle.Size() / 2f, num10, SpriteEffects.None, 0f);
                    Main.EntitySpriteDraw(value, vector, new Rectangle?(rectangle), color6 * num7, num15, rectangle.Size() / 2f, num10, SpriteEffects.None, 0f);
                }
            }
        }
    }
    public override void AI()
    {
        Projectile.localAI[0]++;
        int num = 60;
        int num2 = 12;
        int num3 = num + num2;
        if (Projectile.localAI[0] >= num3)
        {
            Projectile.Kill();
        }
        if (Projectile.localAI[0] >= num)
        {
            Projectile.velocity *= 0.95f;
        }
        bool flag = Projectile.ai[0] == 1f;
        int num4 = 50;
        int num5 = num4;
        if (flag)
        {
            num4 = 0;
            num5 = num;
        }
        if (Projectile.localAI[0] < (float)num5 && Main.rand.NextFloat() < 0.25f)
        {
            short num6 = (short)ModContent.DustType<Dusts.FreezethrowerDust>();
            Dust dust = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(60f, 60f) * Utils.Remap(Projectile.localAI[0], 0f, 72f, 0.5f, 1f), 4, 4, num6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
            dust.noGravity = true;
            if (Main.rand.NextBool(2))
            {
                dust.scale *= 1f;
                dust.velocity.X *= 1.5f;
                dust.velocity.Y *= 1.5f;
            }
            else
            {
                dust.scale *= 1.5f;
            }
            dust.scale *= 1.2f;
            dust.velocity *= 1.1f;
            dust.velocity += Projectile.velocity * 1f * Utils.Remap(Projectile.localAI[0], 0f, (float)num * 0.75f, 1f, 0.1f) * Utils.Remap(Projectile.localAI[0], 0f, (float)num * 0.1f, 0.1f, 1f);
            dust.customData = 1;
        }
        if (num4 > 0 && Projectile.localAI[0] >= (float)num4 && Main.rand.NextFloat() < 0.5f)
        {
            Vector2 center = Main.player[Projectile.owner].Center;
            Vector2 vector = (Projectile.Center - center).SafeNormalize(Vector2.Zero).RotatedByRandom(0.19634954631328583) * 7f;
            short num7 = DustID.Snow;
            Dust dust2 = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(50f, 50f) - vector * 2f, 4, 4, num7, 0f, 0f, 150, new Color(255, 255, 255));
            dust2.noGravity = true;
            dust2.velocity = vector;
            dust2.scale *= 1.1f + Main.rand.NextFloat() * 0.2f;
            dust2.customData = -0.3f - 0.15f * Main.rand.NextFloat();
        }
        //if (Projectile.timeLeft > 60)
        //{
        //    Projectile.timeLeft = 60;
        //}
        //if (Projectile.ai[0] > 6f)
        //{
        //    var num349 = 1f;
        //    if (Projectile.ai[0] == 8f)
        //    {
        //        num349 = 0.25f;
        //    }
        //    else if (Projectile.ai[0] == 9f)
        //    {
        //        num349 = 0.5f;
        //    }
        //    else if (Projectile.ai[0] == 10f)
        //    {
        //        num349 = 0.75f;
        //    }
        //    Projectile.ai[0] += 1f;
        //    var num350 = 6;
        //    if (Projectile.type == ProjectileID.EyeFire)
        //    {
        //        num350 = 75;
        //    }
        //    if (Projectile.type == ModContent.ProjectileType<Freezethrower>())
        //    {
        //        num350 = ModContent.DustType<Dusts.FreezethrowerDust>();
        //    }
        //    if (num350 == 6 || num350 == 181 || Main.rand.Next(2) == 0)
        //    {
        //        for (var num351 = 0; num351 < 1; num351++)
        //        {
        //            var num352 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num350, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
        //            if (Main.rand.Next(3) != 0 || (num350 == 75 && Main.rand.Next(3) == 0))
        //            {
        //                Main.dust[num352].noGravity = true;
        //                Main.dust[num352].scale *= 3f;
        //                var dust53 = Main.dust[num352];
        //                dust53.velocity.X = dust53.velocity.X * 2f;
        //                var dust54 = Main.dust[num352];
        //                dust54.velocity.Y = dust54.velocity.Y * 2f;
        //            }
        //            else
        //            {
        //                Main.dust[num352].scale *= 1.5f;
        //            }
        //            var dust55 = Main.dust[num352];
        //            dust55.velocity.X = dust55.velocity.X * 1.2f;
        //            var dust56 = Main.dust[num352];
        //            dust56.velocity.Y = dust56.velocity.Y * 1.2f;
        //            Main.dust[num352].scale *= num349;
        //            if (num350 == 75)
        //            {
        //                Main.dust[num352].velocity += Projectile.velocity;
        //                if (!Main.dust[num352].noGravity)
        //                {
        //                    Main.dust[num352].velocity *= 0.5f;
        //                }
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    Projectile.ai[0] += 1f;
        //}
        //Projectile.rotation += 0.3f * Projectile.direction;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity *= 0;
        return false;
    }
}
