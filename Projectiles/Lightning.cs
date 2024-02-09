using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Projectiles;

public class Lightning : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.aiStyle = -1;
        AIType = ProjectileID.CultistBossLightningOrbArc;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.timeLeft = 180;
        Projectile.friendly = true;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
    }

    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CultistBossLightningOrbArc;

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, 0f, 0f);
        //Projectile.active = false;
        return false;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, 0f, 0f);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Vector2 end = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
        Vector2 scale = new Vector2(Projectile.scale) / 2f;
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    scale = new Vector2(Projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = new Color(115, 204, 219, 0) * 0.5f;
                    break;

                case 1:
                    scale = new Vector2(Projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                    break;

                default:
                    scale = new Vector2(Projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                    break;
            }
            DelegateMethods.f_1 = 1f;
            for (int j = Projectile.oldPos.Length - 1; j > 0; j--)
            {
                if (!(Projectile.oldPos[j] == Vector2.Zero))
                {
                    Vector2 start = Projectile.oldPos[j] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                    Vector2 end2 = Projectile.oldPos[j - 1] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, start, end2, scale, DelegateMethods.LightningLaserDraw);
                }
            }
            if (Projectile.oldPos[0] != Vector2.Zero)
            {
                DelegateMethods.f_1 = 1f;
                Vector2 start2 = Projectile.oldPos[0] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Utils.DrawLaser(Main.spriteBatch, Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, start2, end, scale, DelegateMethods.LightningLaserDraw);
            }
        }
        return false;
    }

    public override void AI()
    {
        Projectile.frameCounter++;
        Lighting.AddLight(Projectile.Center, 0.3f, 0.45f, 0.5f);

        if (Projectile.velocity == Vector2.Zero)
        {
            if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
            {
                Projectile.frameCounter = 0;
                bool flag28 = true;
                for (int i = 1; i < Projectile.oldPos.Length; i++)
                {
                    if (Projectile.oldPos[i] != Projectile.oldPos[0])
                    {
                        flag28 = false;
                    }
                }
                if (flag28)
                {
                    Projectile.Kill();
                    return;
                }
            }
            if (Main.rand.NextBool(Projectile.extraUpdates))
            {
                for (int i = 0; i < 2; i++)
                {
                    float num1054 = Projectile.rotation + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
                    float num1055 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                    Vector2 vector93 = new Vector2((float)Math.Cos(num1054) * num1055, (float)Math.Sin(num1054) * num1055);
                    int num1056 = Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, vector93.X, vector93.Y);
                    Main.dust[num1056].noGravity = true;
                    Main.dust[num1056].scale = 1.2f;
                }
                if (Main.rand.NextBool(5))
                {
                    Vector2 value39 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                    int num1057 = Dust.NewDust(Projectile.Center + value39 - Vector2.One * 4f, 8, 8, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
                    Dust dust64 = Main.dust[num1057];
                    Dust dust189 = dust64;
                    dust189.velocity *= 0.5f;
                    Main.dust[num1057].velocity.Y = 0f - Math.Abs(Main.dust[num1057].velocity.Y);
                }
            }
        }
        else
        {
            if (Projectile.frameCounter < Projectile.extraUpdates * 2)
            {
                return;
            }
            Projectile.frameCounter = 0;
            float num1058 = Projectile.velocity.Length();
            UnifiedRandom unifiedRandom = new UnifiedRandom((int)Projectile.ai[1]);
            int num1059 = 0;
            Vector2 spinningpoint6 = -Vector2.UnitY;
            while (true)
            {
                int randomSeed = unifiedRandom.Next();
                Projectile.ai[1] = randomSeed;
                randomSeed %= 100;
                float f = randomSeed / 100f * ((float)Math.PI * 2f);
                Vector2 vector94 = f.ToRotationVector2();
                if (vector94.Y > 0f)
                {
                    vector94.Y *= -1f;
                }
                bool flag29 = false;
                if (vector94.Y > -0.02f)
                {
                    flag29 = true;
                }
                if (vector94.X * (Projectile.extraUpdates + 1) * 2f * num1058 + Projectile.localAI[0] > 40f)
                {
                    flag29 = true;
                }
                if (vector94.X * (Projectile.extraUpdates + 1) * 2f * num1058 + Projectile.localAI[0] < -40f)
                {
                    flag29 = true;
                }
                if (flag29)
                {
                    if (num1059++ >= 300)
                    {
                        Projectile.velocity = Vector2.Zero;
                        break;
                    }
                    continue;
                }
                spinningpoint6 = vector94;
                break;
            }
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.localAI[0] += spinningpoint6.X * (Projectile.extraUpdates + 1) * 2f * num1058;
                Projectile.velocity = spinningpoint6.RotatedBy(Projectile.ai[0] + (float)Math.PI / 2f) * num1058;
                Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
            }
        }
    }
}
