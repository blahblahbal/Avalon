using System;
using Avalon.Projectiles.Hostile.DesertBeak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class ExplosiveEgg : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 2;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }

    public override void AI()
    {
        Projectile.frame = (int)Projectile.ai[2];
        Projectile.ai[0]++;
        Projectile.rotation += Projectile.velocity.X * 0.03f;
        if (Projectile.ai[0] > 20)
        {
            Projectile.velocity.Y += 0.3f;
            Projectile.velocity.X *= 0.99f;
        }
    }
    public override void Kill(int timeLeft)
    {
        if (Projectile.ai[2] == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, Main.rand.NextFloat(7, 9)).RotatedBy((MathHelper.TwoPi / 8) * i).RotatedByRandom(0.4f), ModContent.ProjectileType<ExplosiveEggShrapnel>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
            }
        }

            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;

            int explosionArea = 75;
            Vector2 oldSize = Projectile.Size;
            Projectile.position = Projectile.Center;
            Projectile.Size += new Vector2(explosionArea);
            Projectile.Center = Projectile.position;

            Projectile.tileCollide = false;
            Projectile.velocity *= 0.01f;
            Projectile.Damage();
            Projectile.scale = 0.01f;

            Projectile.position = Projectile.Center;
            Projectile.Size = new Vector2(10);
            Projectile.Center = Projectile.position;

        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (Projectile.ai[2] == 0) ? DustID.Torch : DustID.DesertTorch, 0, 0, 0, default, 2f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(6, 6);
            Main.dust[d].noGravity = true;
            Main.dust[d].fadeIn = 2.3f;
        }
        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 0, default, 1.4f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-3, 0).RotatedBy(Projectile.velocity.ToRotation());
            Main.dust[d].noGravity = !Main.rand.NextBool(10);
        }
        for (int i = 0; i < 7; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0, 0, 0, default, 1.4f);
            //Main.dust[d].color = Color.Red;
            Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-5, 0).RotatedBy(Projectile.velocity.ToRotation());
            Main.dust[d].noGravity = Main.rand.NextBool(3);
        }
        for (int i = 0; i < 9; i++)
        {
            int g = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(10, 6) + new Vector2(-1, 0).RotatedBy(Projectile.velocity.ToRotation()), Main.rand.Next(61, 63), 0.8f);
            Main.gore[g].alpha = 128;
        }
        Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
    }
}
