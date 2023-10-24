using System;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class HallowedThorn : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 28 / 32;
        Projectile.height = dims.Height * 28 / 32 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.light = 0.5f;
    }

    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 255 / 255f, 255 / 255f, 0);
        Vector2 vector73 = Projectile.position + new Vector2(Projectile.width / 2, Projectile.height / 2);
        Projectile.position -= Projectile.velocity;
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57079637f;
        if (Projectile.ai[0] == 0f)
        {
            Projectile.alpha -= 50;
            if (Projectile.alpha <= 0)
            {
                Projectile.alpha = 0;
                Projectile.ai[0] = 1f;
                if (Projectile.ai[1] == 0f)
                {
                    Projectile.ai[1] += 1f;
                    Projectile.position += Projectile.velocity * 1f;
                }

                if (Projectile.type == ModContent.ProjectileType<HallowedThorn>() && Main.myPlayer == Projectile.owner)
                {
                    int num928 = ModContent.ProjectileType<HallowedThorn>();
                    if (Projectile.ai[1] >= 11f)
                    {
                        num928 = ModContent.ProjectileType<HallowedThornEnd>();
                    }
                    else
                    {
                        num928 = ModContent.ProjectileType<HallowedThorn>();
                    }

                    if ((int)Projectile.ai[1] % 3 == 0)
                    {
                        var point = new Vector2(Projectile.velocity.X, Projectile.velocity.Y);
                        float num929 = 0.3926991f * (float)Main.rand.NextDouble();
                        Projectile.velocity = AvalonGlobalProjectile.RotateAboutOrigin(point, num929);
                        int num930 = Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                            vector73.X + Projectile.velocity.X, vector73.Y + Projectile.velocity.Y,
                            Projectile.velocity.X, Projectile.velocity.Y, num928, Projectile.damage,
                            Projectile.knockBack, Projectile.owner);
                        Projectile nprojectile = Main.projectile[num930];
                        nprojectile.damage = Projectile.damage;
                        nprojectile.ai[1] = Projectile.ai[1] + 1f;
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.FromLiteral(""), num930);
                        num929 = 0.3926991f * (float)Main.rand.NextDouble();
                        nprojectile.velocity = AvalonGlobalProjectile.RotateAboutOrigin(point, -num929);
                        num930 = Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                            vector73.X + Projectile.velocity.X, vector73.Y + Projectile.velocity.Y,
                            Projectile.velocity.X, Projectile.velocity.Y, num928, Projectile.damage,
                            Projectile.knockBack, Projectile.owner);
                        nprojectile = Main.projectile[num930];
                        nprojectile.damage = Projectile.damage;
                        nprojectile.ai[1] = Projectile.ai[1] + 1f;
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.FromLiteral(""), num930);
                        return;
                    }

                    int num931 = Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                        vector73.X + Projectile.velocity.X, vector73.Y + Projectile.velocity.Y, Projectile.velocity.X,
                        Projectile.velocity.Y, num928, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    Projectile projectile2 = Main.projectile[num931];
                    projectile2.damage = Projectile.damage;
                    projectile2.ai[1] = Projectile.ai[1] + 1f;
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.FromLiteral(""), num931);
                }
            }
        }
        else
        {
            if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
            {
                for (int num932 = 0; num932 < 3; num932++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold,
                        Projectile.velocity.X * 0.025f, Projectile.velocity.Y * 0.025f, 170, default, 1.2f);
                }

                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f,
                    170, default, 1.1f);
            }

            Projectile.alpha += 5;
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}
