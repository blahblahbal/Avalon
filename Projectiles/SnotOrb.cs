using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class SnotOrb : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 1;
        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.LightPet[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Projectile.light = 0.9f;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }
    public override void AI()
    {
        Projectile.rotation += 0.02f;
        for (int q = 0; q < 4; q++)
        {
            if (Main.rand.NextBool(30))
            {
                int d = Dust.NewDust(Projectile.position, 8, 8, DustID.GreenBlood, 0f, 0f, 0, default, 1f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 3f;
            }
        }
        if (!Main.player[Projectile.owner].active)
        {
            Projectile.active = false;
            return;
        }
        if (Main.player[Projectile.owner].dead)
        {
            Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().SnotOrb = false;
        }
        if (Main.myPlayer == Projectile.owner)
        {
            if (Projectile.type == ModContent.ProjectileType<SnotOrb>())
            {
                if (Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().SnotOrb)
                {
                    Projectile.timeLeft = 2;
                }
            }
        }
        if (!Main.player[Projectile.owner].dead)
        {
            float num179 = 3f;
            Vector2 vector14 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num180 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector14.X;
            float num181 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector14.Y;
            int num182 = 70;
            if (Projectile.type == ModContent.ProjectileType<SnotOrb>())
            {
                if (Main.player[Projectile.owner].controlUp)
                {
                    num181 = Main.player[Projectile.owner].position.Y - 40f - vector14.Y;
                    num180 -= 6f;
                    num182 = 4;
                }
                else if (Main.player[Projectile.owner].controlDown)
                {
                    num181 = Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height + 40f - vector14.Y;
                    num180 -= 6f;
                    num182 = 4;
                }
            }
            float num183 = (float)Math.Sqrt(num180 * num180 + num181 * num181);
            num183 = (float)Math.Sqrt(num180 * num180 + num181 * num181);
            if (num183 > 800f)
            {
                Projectile.position.X = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - Projectile.width / 2;
                Projectile.position.Y = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - Projectile.height / 2;
            }
            else if (num183 > num182)
            {
                num183 = num179 / num183;
                num180 *= num183;
                num181 *= num183;
                Projectile.velocity.X = num180;
                Projectile.velocity.Y = num181;
            }
            else
            {
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
            }
        }
        else
        {
            Projectile.Kill();
        }
    }
}
