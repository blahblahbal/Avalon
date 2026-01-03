using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.NPCs.Bosses.Hardmode.WallOfSteel.Projectiles;

public class WoSRocket : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = 1;
        Projectile.alpha = 0;
        //Projectile.friendly = false;
		Projectile.hostile = true;
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
        for (int num369 = 0; num369 < 20; num369++)
        {
            int num370 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num370].velocity *= 1.4f;
        }
        for (int num371 = 0; num371 < 10; num371++)
        {
            int num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2.5f);
            Main.dust[num372].noGravity = true;
            Main.dust[num372].velocity *= 5f;
            num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num372].velocity *= 3f;
        }
        int num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
        Main.gore[num373].velocity *= 0.4f;
        Gore expr_B3F3_cp_0 = Main.gore[num373];
        expr_B3F3_cp_0.velocity.X = expr_B3F3_cp_0.velocity.X + 1f;
        Gore expr_B413_cp_0 = Main.gore[num373];
        expr_B413_cp_0.velocity.Y = expr_B413_cp_0.velocity.Y + 1f;
        num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
        Main.gore[num373].velocity *= 0.4f;
        Gore expr_B497_cp_0 = Main.gore[num373];
        expr_B497_cp_0.velocity.X = expr_B497_cp_0.velocity.X - 1f;
        Gore expr_B4B7_cp_0 = Main.gore[num373];
        expr_B4B7_cp_0.velocity.Y = expr_B4B7_cp_0.velocity.Y + 1f;
        num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
        Main.gore[num373].velocity *= 0.4f;
        Gore expr_B53B_cp_0 = Main.gore[num373];
        expr_B53B_cp_0.velocity.X = expr_B53B_cp_0.velocity.X + 1f;
        Gore expr_B55B_cp_0 = Main.gore[num373];
        expr_B55B_cp_0.velocity.Y = expr_B55B_cp_0.velocity.Y - 1f;
        num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
        Main.gore[num373].velocity *= 0.4f;
        Gore expr_B5DF_cp_0 = Main.gore[num373];
        expr_B5DF_cp_0.velocity.X = expr_B5DF_cp_0.velocity.X - 1f;
        Gore expr_B5FF_cp_0 = Main.gore[num373];
        expr_B5FF_cp_0.velocity.Y = expr_B5FF_cp_0.velocity.Y - 1f;
    }

    //public override bool OnTileCollide(Vector2 oldVelocity)
    //{
    //    if (projectile.type == ModContent.ProjectileType<BlahBeam>())
    //    {
    //        Main.PlaySound(SoundID.Item10, Projectile.position);
    //        projectile.ai[0] += 1f;
    //        if (projectile.ai[0] >= 4f)
    //        {
    //            projectile.position += projectile.velocity;
    //            projectile.Kill();
    //        }
    //        else
    //        {
    //            if (projectile.velocity.Y != oldVelocity.Y)
    //            {
    //                projectile.velocity.Y = -oldVelocity.Y;
    //            }
    //            if (projectile.velocity.X != oldVelocity.X)
    //            {
    //                projectile.velocity.X = -oldVelocity.X;
    //            }
    //        }
    //    }
    //    return false;
    //}
    public override void AI()
    {
        //if (Projectile.type == ModContent.ProjectileType<Blahcket>())
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[1] = Projectile.velocity.ToRotation();
                Projectile.localAI[0] = Projectile.velocity.Length();
            }
            //Projectile.ai[0]++;
            //if (Projectile.ai[0] < 45) Projectile.velocity.Y += 0.02f; //gravity
            //if (Projectile.ai[0] == 45)
            //{
            //    Vector2 dustPoint = Projectile.Center;
            //    Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.direction * -0.4f, -1.4f);
            //    Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.direction * -0.4f, -1.4f, Scale: 1.5f);
            //    Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.direction * -0.4f, 1.4f);
            //    Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.direction * -0.4f, 1.4f, Scale: 1.5f);

            //    Projectile.velocity = Projectile.localAI[1].ToRotationVector2() * Projectile.localAI[0] * 2.1f;
            //}
            //else
            {
                for (int num126 = 0; num126 < 5; num126++)
                {
                    float num127 = Projectile.velocity.X / 3f * (float)num126;
                    float num128 = Projectile.velocity.Y / 3f * (float)num126;
                    int num129 = 4;
                    int num130 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num129, Projectile.position.Y + (float)num129), Projectile.width - num129 * 2, Projectile.height - num129 * 2, DustID.Torch, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num130].noGravity = true;
                    Main.dust[num130].velocity *= 0.1f;
                    Main.dust[num130].velocity += Projectile.velocity * 0.1f;
                    Dust expr_62C2_cp_0 = Main.dust[num130];
                    expr_62C2_cp_0.position.X = expr_62C2_cp_0.position.X - num127;
                    Dust expr_62DD_cp_0 = Main.dust[num130];
                    expr_62DD_cp_0.position.Y = expr_62DD_cp_0.position.Y - num128;
                }
                if (Main.rand.Next(5) == 0)
                {
                    int num131 = 4;
                    int num132 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num131, Projectile.position.Y + (float)num131), Projectile.width - num131 * 2, Projectile.height - num131 * 2, DustID.Smoke, 0f, 0f, 100, default(Color), 0.6f);
                    Main.dust[num132].velocity *= 0.25f;
                    Main.dust[num132].velocity += Projectile.velocity * 0.5f;
                }
            }
        }
        float num26 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
        float num27 = Projectile.localAI[0];
        if (num27 == 0f)
        {
            Projectile.localAI[0] = num26;
            num27 = num26;
        }
        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 25;
        }
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
		Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(Projectile.ai[2]));
	}
}
