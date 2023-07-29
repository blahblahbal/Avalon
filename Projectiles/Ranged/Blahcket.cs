using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Ranged;

public class Blahcket : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = 1;
        Projectile.alpha = 0;
        Projectile.friendly = true;
    }
    public override void Kill(int timeLeft)
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
        if (Projectile.type == ModContent.ProjectileType<Blahcket>())
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[1] = Projectile.velocity.ToRotation();
                Projectile.localAI[0] = Projectile.velocity.Length();
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] < 45) Projectile.velocity.Y += 0.02f; //gravity
            if (Projectile.ai[0] == 45)
            {
                Vector2 dustPoint = Projectile.Center;
                Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.direction * -0.4f, -1.4f);
                Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.direction * -0.4f, -1.4f, Scale: 1.5f);
                Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.direction * -0.4f, 1.4f);
                Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.direction * -0.4f, 1.4f, Scale: 1.5f);

                Projectile.velocity = Projectile.localAI[1].ToRotationVector2() * Projectile.localAI[0] * 2.1f;
            }
            else
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
        if (Projectile.alpha < 170)
        {
            for (int n = 0; n < 10; n++)
            {
                float x2 = Projectile.position.X - Projectile.velocity.X / 10f * (float)n;
                float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)n;
                int num25;
                if (Projectile.type == 207)
                {
                    num25 = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.CursedTorch, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num25].alpha = Projectile.alpha;
                    Main.dust[num25].position.X = x2;
                    Main.dust[num25].position.Y = y2;
                    Main.dust[num25].velocity *= 0f;
                    Main.dust[num25].noGravity = true;
                }
                else if (Projectile.type == 428)
                {
                    num25 = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.Ice_Pink, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num25].alpha = Projectile.alpha;
                    Main.dust[num25].position.X = x2;
                    Main.dust[num25].position.Y = y2;
                    Main.dust[num25].velocity *= 0f;
                    Main.dust[num25].noGravity = true;
                }
                else if (Projectile.type == 622 || Projectile.type == 623)
                {
                    num25 = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.Torch, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num25].alpha = Projectile.alpha;
                    Main.dust[num25].position.X = x2;
                    Main.dust[num25].position.Y = y2;
                    Main.dust[num25].velocity *= 0f;
                    Main.dust[num25].noGravity = true;
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
        float num28 = Projectile.position.X;
        float num29 = Projectile.position.Y;
        float num30 = (Projectile.type == 605 ? 250f : 300f);
        bool flag = false;
        int num31 = 0;
        if (Projectile.ai[1] == 0f)
        {
            if (Projectile.type == 605)
            {
                for (int num32 = 0; num32 < Main.player.Length; num32++)
                {
                    if (Main.player[num32].active && Main.player[num32].statLife > 0 && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(num32 + 1)))
                    {
                        float num33 = Main.player[num32].position.X + (float)(Main.player[num32].width / 2);
                        float num34 = Main.player[num32].position.Y + (float)(Main.player[num32].height / 2);
                        float num35 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num33) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num34);
                        if (num35 < num30 && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.player[num32].position, Main.player[num32].width, Main.player[num32].height))
                        {
                            num30 = num35;
                            num28 = num33;
                            num29 = num34;
                            flag = true;
                            num31 = num32;
                        }
                    }
                }
            }
            else
            {
                for (int num32 = 0; num32 < 200; num32++)
                {
                    if (Main.npc[num32].active && !Main.npc[num32].dontTakeDamage && !Main.npc[num32].friendly && Main.npc[num32].lifeMax > 5 && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(num32 + 1)))
                    {
                        float num33 = Main.npc[num32].position.X + (float)(Main.npc[num32].width / 2);
                        float num34 = Main.npc[num32].position.Y + (float)(Main.npc[num32].height / 2);
                        float num35 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num33) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num34);
                        if (num35 < num30 && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[num32].position, Main.npc[num32].width, Main.npc[num32].height))
                        {
                            num30 = num35;
                            num28 = num33;
                            num29 = num34;
                            flag = true;
                            num31 = num32;
                        }
                    }
                }
            }
            if (flag)
            {
                Projectile.ai[1] = (float)(num31 + 1);
            }
            flag = false;
        }
        if (Projectile.ai[1] != 0f)
        {
            int num36 = (int)(Projectile.ai[1] - 1f);
            if (Projectile.type == 605)
            {
                if (Main.player[num36].active)
                {
                    float num37 = Main.player[num36].position.X + (float)(Main.player[num36].width / 2);
                    float num38 = Main.player[num36].position.Y + (float)(Main.player[num36].height / 2);
                    float num39 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num37) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num38);
                    if (num39 < 1000f)
                    {
                        flag = true;
                        num28 = Main.player[num36].position.X + (float)(Main.player[num36].width / 2);
                        num29 = Main.player[num36].position.Y + (float)(Main.player[num36].height / 2);
                    }
                }
            }
            else
            {
                if (Main.npc[num36].active)
                {
                    float num37 = Main.npc[num36].position.X + (float)(Main.npc[num36].width / 2);
                    float num38 = Main.npc[num36].position.Y + (float)(Main.npc[num36].height / 2);
                    float num39 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num37) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num38);
                    if (num39 < 1000f)
                    {
                        flag = true;
                        num28 = Main.npc[num36].position.X + (float)(Main.npc[num36].width / 2);
                        num29 = Main.npc[num36].position.Y + (float)(Main.npc[num36].height / 2);
                    }
                }
            }
        }
        if (flag)
        {
            float num40 = num27;
            Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num41 = num28 - vector.X;
            float num42 = num29 - vector.Y;
            float num43 = (float)Math.Sqrt((double)(num41 * num41 + num42 * num42));
            num43 = num40 / num43;
            num41 *= num43;
            num42 *= num43;
            int num44 = 8;
            Projectile.velocity.X = (Projectile.velocity.X * (float)(num44 - 1) + num41) / (float)num44;
            Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num44 - 1) + num42) / (float)num44;
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
