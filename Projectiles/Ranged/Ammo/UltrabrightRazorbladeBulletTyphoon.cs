using System;
using Avalon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Ammo;

public class UltrabrightRazorbladeBulletTyphoon : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 2;
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
    }
    public override void SetDefaults()
    {
        Projectile.width = 12;
        Projectile.height = 12;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.timeLeft = 540;
        Projectile.penetrate = -1;
        Projectile.tileCollide = true;
        Projectile.MaxUpdates = 2;
        Projectile.scale = 1f;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.light = 0.75f;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = -oldVelocity.Y;
        }
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X;
        }
        return false;
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        height = 30;
        width = 30;
        return true;
    }
    public override void AI()
    {
        Projectile.frameCounter++;
        if (Projectile.frameCounter >= 3)
        {
            Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            Projectile.frameCounter = 0;
        }
        Projectile.localAI[1]++;
        if (Projectile.localAI[1] > 10f && Main.rand.NextBool(8))
        {
            int num586 = 2;
            for (int num587 = 0; num587 < num586; num587++)
            {
                Vector2 spinningpoint4 = Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width, Projectile.height) / 2f;
                spinningpoint4 = spinningpoint4.RotatedBy((num587 - (num586 / 2 - 1)) * Math.PI / num586) + Projectile.Center;
                Vector2 vector54 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * Main.rand.Next(3, 8);
                int num588 = Dust.NewDust(spinningpoint4 + vector54, 0, 0, DustID.DungeonSpirit, vector54.X * 2f, vector54.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[num588].noGravity = true;
                Main.dust[num588].noLight = true;
                Dust dust = Main.dust[num588];
                dust.velocity *= 0.05f;
                dust = Main.dust[num588];
                dust.velocity -= Projectile.velocity;
            }
            Projectile.alpha -= 5;
            if (Projectile.alpha < 50)
            {
                Projectile.alpha = 50;
            }
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.frame = (int)(Projectile.localAI[1] / 3f) % 3;
            Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.1f, 0.4f, 0.6f);
        }
        int num589 = -1;
        Vector2 vector55 = Projectile.Center;
        float num590 = 500f;
        if (Projectile.localAI[0] > 0f)
        {
            Projectile.localAI[0]--;
        }
        if (Projectile.ai[0] == 0f && Projectile.localAI[0] == 0f)
        {
            for (int num591 = 0; num591 < 200; num591++)
            {
                NPC nPC4 = Main.npc[num591];
                if (nPC4.CanBeChasedBy(this) && (Projectile.ai[0] == 0f || Projectile.ai[0] == num591 + 1))
                {
                    Vector2 center6 = nPC4.Center;
                    float num592 = Vector2.Distance(center6, vector55);
                    if (num592 < num590 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC4.position, nPC4.width, nPC4.height))
                    {
                        num590 = num592;
                        vector55 = center6;
                        num589 = num591;
                    }
                }
            }
            if (num589 >= 0)
            {
                Projectile.ai[0] = num589 + 1;
                Projectile.netUpdate = true;
            }
            num589 = -1;
        }
        if (Projectile.localAI[0] == 0f && Projectile.ai[0] == 0f)
        {
            Projectile.localAI[0] = 30f;
        }
        bool flag28 = false;
        if (Projectile.ai[0] != 0f)
        {
            int num593 = (int)(Projectile.ai[0] - 1f);
            if (Main.npc[num593].active && !Main.npc[num593].dontTakeDamage && Main.npc[num593].immune[Projectile.owner] == 0)
            {
                float num594 = Main.npc[num593].position.X + Main.npc[num593].width / 2;
                float num595 = Main.npc[num593].position.Y + Main.npc[num593].height / 2;
                float num596 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num594) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num595);
                if (num596 < 1000f)
                {
                    flag28 = true;
                    vector55 = Main.npc[num593].Center;
                }
            }
            else
            {
                Projectile.ai[0] = 0f;
                flag28 = false;
                Projectile.netUpdate = true;
            }
        }
        if (flag28)
        {
            Vector2 v2 = vector55 - Projectile.Center;
            float num597 = Projectile.velocity.ToRotation();
            float num598 = v2.ToRotation();
            double num599 = num598 - num597;
            if (num599 > Math.PI)
            {
                num599 -= Math.PI * 2.0;
            }
            if (num599 < -Math.PI)
            {
                num599 += Math.PI * 2.0;
            }
            Projectile.velocity = Projectile.velocity.RotatedBy(num599 * 0.10000000149011612);
        }
        float num600 = Projectile.velocity.Length();
        Projectile.velocity.Normalize();
        Projectile.velocity *= num600 + 0.0025f;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Vector2 drawOrigin = new Vector2(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle(0, Projectile.height * Projectile.frame, Projectile.width, Projectile.height), color, Projectile.rotation, drawOrigin, Projectile.scale * 0.9f, SpriteEffects.None, 0);
        }
        return true;
    }
}
