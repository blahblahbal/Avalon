using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.NPCs.Bosses.Hardmode;

public class HomingRocket : ModNPC
{
    public override void SetDefaults()
    {
        NPC.width = 14;
        NPC.height = 14;
        NPC.aiStyle = -1;
        NPC.lifeMax = 150;
        NPC.value = 0;
    }
    public override void OnKill()
    {
        foreach (Player P in Main.player)
        {
            if (P.getRect().Intersects(NPC.getRect()))
            {
                P.Hurt(PlayerDeathReason.ByProjectile(P.whoAmI, NPC.whoAmI), NPC.damage, 0);
            }
        }
        SoundEngine.PlaySound(SoundID.Item14, NPC.position);
        NPC.position.X = NPC.position.X + NPC.width / 2;
        NPC.position.Y = NPC.position.Y + NPC.height / 2;
        NPC.width = 22;
        NPC.height = 22;
        NPC.position.X = NPC.position.X - NPC.width / 2;
        NPC.position.Y = NPC.position.Y - NPC.height / 2;
        for (int num341 = 0; num341 < 30; num341++)
        {
            int num342 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num342].velocity *= 1.4f;
        }
        for (int num343 = 0; num343 < 20; num343++)
        {
            int num344 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default(Color), 3.5f);
            Main.dust[num344].noGravity = true;
            Main.dust[num344].velocity *= 7f;
            num344 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num344].velocity *= 3f;
        }
        for (int num345 = 0; num345 < 2; num345++)
        {
            float scaleFactor8 = 0.4f;
            if (num345 == 1)
            {
                scaleFactor8 = 0.8f;
            }
            int num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A0B0_cp_0 = Main.gore[num346];
            expr_A0B0_cp_0.velocity.X = expr_A0B0_cp_0.velocity.X + 1f;
            Gore expr_A0D0_cp_0 = Main.gore[num346];
            expr_A0D0_cp_0.velocity.Y = expr_A0D0_cp_0.velocity.Y + 1f;
            num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A153_cp_0 = Main.gore[num346];
            expr_A153_cp_0.velocity.X = expr_A153_cp_0.velocity.X - 1f;
            Gore expr_A173_cp_0 = Main.gore[num346];
            expr_A173_cp_0.velocity.Y = expr_A173_cp_0.velocity.Y + 1f;
            num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A1F6_cp_0 = Main.gore[num346];
            expr_A1F6_cp_0.velocity.X = expr_A1F6_cp_0.velocity.X + 1f;
            Gore expr_A216_cp_0 = Main.gore[num346];
            expr_A216_cp_0.velocity.Y = expr_A216_cp_0.velocity.Y - 1f;
            num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A299_cp_0 = Main.gore[num346];
            expr_A299_cp_0.velocity.X = expr_A299_cp_0.velocity.X - 1f;
            Gore expr_A2B9_cp_0 = Main.gore[num346];
            expr_A2B9_cp_0.velocity.Y = expr_A2B9_cp_0.velocity.Y - 1f;
        }
    }
    //public override void ModifyDamageHitbox(ref Rectangle hitbox)
    //{
    //    base.ModifyDamageHitbox(ref hitbox);
    //}
    public override void AI()
    {
        if (Math.Abs(NPC.velocity.X) >= 5f || Math.Abs(NPC.velocity.Y) >= 5f)
        {
            for (int num264 = 0; num264 < 2; num264++)
            {
                float num265 = 0f;
                float num266 = 0f;
                if (num264 == 1)
                {
                    num265 = NPC.velocity.X * 0.5f;
                    num266 = NPC.velocity.Y * 0.5f;
                }
                int num267 = Dust.NewDust(new Vector2(NPC.position.X + 3f + num265, NPC.position.Y + 3f + num266) - NPC.velocity * 0.5f, NPC.width - 8, NPC.height - 8, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num267].scale *= 2f + Main.rand.Next(10) * 0.1f;
                Main.dust[num267].velocity *= 0.2f;
                Main.dust[num267].noGravity = true;
                num267 = Dust.NewDust(new Vector2(NPC.position.X + 3f + num265, NPC.position.Y + 3f + num266) - NPC.velocity * 0.5f, NPC.width - 8, NPC.height - 8, DustID.Smoke, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num267].fadeIn = 1f + Main.rand.Next(5) * 0.1f;
                Main.dust[num267].velocity *= 0.05f;
            }
        }
        if (Math.Abs(NPC.velocity.X) < 15f && Math.Abs(NPC.velocity.Y) < 15f)
        {
            NPC.velocity *= 1.1f;
        }
        NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
        for (int p = 0; p < Main.player.Length; p++)
        {
            if (Main.player[p].active)
            {
                if (ClassExtensions.NewRectVector2(Main.player[p].position, new Vector2(Main.player[p].width, Main.player[p].height)).Intersects(ClassExtensions.NewRectVector2(NPC.position, new Vector2(NPC.width, NPC.height))))
                {
                    NPC.timeLeft = 3;
                    break;
                }
            }
        }
        if (NPC.timeLeft <= 3)
        {
            NPC.position.X = NPC.position.X + NPC.width / 2;
            NPC.position.Y = NPC.position.Y + NPC.height / 2;
            NPC.width = 128;
            NPC.height = 128;
            NPC.position.X = NPC.position.X - NPC.width / 2;
            NPC.position.Y = NPC.position.Y - NPC.height / 2;
            NPC.life = 0;
            NPC.checkDead();
        }
        float num26 = (float)Math.Sqrt((double)(NPC.velocity.X * NPC.velocity.X + NPC.velocity.Y * NPC.velocity.Y));
        float num27 = NPC.localAI[0];
        if (num27 == 0f)
        {
            NPC.localAI[0] = num26;
            num27 = num26;
        }
        if (NPC.alpha > 0)
        {
            NPC.alpha -= 25;
        }
        if (NPC.alpha < 0)
        {
            NPC.alpha = 0;
        }
        float num28 = NPC.position.X;
        float num29 = NPC.position.Y;
        float num30 = 250f;
        bool flag = false;
        int num31 = 0;
        if (NPC.ai[1] == 0)
        {
            for (int num32 = 0; num32 < Main.player.Length; num32++)
            {
                if (Main.player[num32].active && Main.player[num32].statLife > 0 && (NPC.ai[1] == 0f || NPC.ai[1] == num32 + 1))
                {
                    float num33 = Main.player[num32].position.X + Main.player[num32].width / 2;
                    float num34 = Main.player[num32].position.Y + Main.player[num32].height / 2;
                    float num35 = Math.Abs(NPC.position.X + NPC.width / 2 - num33) + Math.Abs(NPC.position.Y + NPC.height / 2 - num34);
                    if (num35 < num30 && Collision.CanHit(new Vector2(NPC.position.X + NPC.width / 2, NPC.position.Y + NPC.height / 2), 1, 1, Main.player[num32].position, Main.player[num32].width, Main.player[num32].height))
                    {
                        num30 = num35;
                        num28 = num33;
                        num29 = num34;
                        flag = true;
                        num31 = num32;
                    }
                }
            }
            if (flag)
            {
                NPC.ai[1] = num31 + 1;
            }
            flag = false;
        }
        if (NPC.ai[1] != 0f)
        {
            int num36 = (int)(NPC.ai[1] - 1f);
            if (Main.player[num36].active)
            {
                float num37 = Main.player[num36].position.X + Main.player[num36].width / 2;
                float num38 = Main.player[num36].position.Y + Main.player[num36].height / 2;
                float num39 = Math.Abs(NPC.position.X + NPC.width / 2 - num37) + Math.Abs(NPC.position.Y + NPC.height / 2 - num38);
                if (num39 < 1000f)
                {
                    flag = true;
                    num28 = Main.player[num36].position.X + Main.player[num36].width / 2;
                    num29 = Main.player[num36].position.Y + Main.player[num36].height / 2;
                }
            }
        }
        if (flag)
        {
            float num40 = num27;
            Vector2 vector = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num41 = num28 - vector.X;
            float num42 = num29 - vector.Y;
            float num43 = (float)Math.Sqrt((double)(num41 * num41 + num42 * num42));
            num43 = num40 / num43;
            num41 *= num43;
            num42 *= num43;
            int num44 = 8;
            NPC.velocity.X = (NPC.velocity.X * (num44 - 1) + num41) / num44;
            NPC.velocity.Y = (NPC.velocity.Y * (num44 - 1) + num42) / num44;
        }
    }
}
