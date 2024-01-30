using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class OilExplosion : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.SolarCounter);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Oiled, 60 * 8);
    }
    public override void OnKill(int timeLeft)
    {
        Vector2 center4;
        Projectile.maxPenetrate = -1;
        Projectile.penetrate = -1;
        Projectile.Damage();
        SoundEngine.PlaySound(in SoundID.Item14, Projectile.position);
        for (int num83 = 0; num83 < 4; num83++)
        {
            int num84 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num84].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)Projectile.width / 2f;
        }
        for (int num85 = 0; num85 < 30; num85++)
        {
            int num87 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt, 0f, 0f, 200, default(Color), 3.7f);
            Main.dust[num87].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)Projectile.width / 2f;
            Main.dust[num87].noGravity = true;
            Dust dust180 = Main.dust[num87];
            Dust dust334 = dust180;
            dust334.velocity *= 3f;
            Main.dust[num87].shader = GameShaders.Armor.GetSecondaryShader(Main.player[Projectile.owner].ArmorSetDye(), Main.player[Projectile.owner]);
            num87 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num87].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)Projectile.width / 2f;
            dust180 = Main.dust[num87];
            dust334 = dust180;
            dust334.velocity *= 2f;
            Main.dust[num87].noGravity = true;
            Main.dust[num87].fadeIn = 2.5f;
        }
        for (int num88 = 0; num88 < 10; num88++)
        {
            int num89 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt, 0f, 0f, 0, default(Color), 2.7f);
            Dust obj5 = Main.dust[num89];
            Vector2 center11 = Projectile.Center;
            Vector2 spinningpoint10 = Vector2.UnitX.RotatedByRandom(3.1415927410125732);
            double radians18 = Projectile.velocity.ToRotation();
            center4 = default(Vector2);
            obj5.position = center11 + spinningpoint10.RotatedBy(radians18, center4) * (float)Projectile.width / 2f;
            Main.dust[num89].noGravity = true;
            Dust dust178 = Main.dust[num89];
            Dust dust334 = dust178;
            dust334.velocity *= 3f;
        }
        for (int num90 = 0; num90 < 10; num90++)
        {
            int num91 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 0, default(Color), 1.5f);
            Dust obj6 = Main.dust[num91];
            Vector2 center12 = Projectile.Center;
            Vector2 spinningpoint11 = Vector2.UnitX.RotatedByRandom(3.1415927410125732);
            double radians19 = Projectile.velocity.ToRotation();
            center4 = default(Vector2);
            obj6.position = center12 + spinningpoint11.RotatedBy(radians19, center4) * (float)Projectile.width / 2f;
            Main.dust[num91].noGravity = true;
            Dust dust176 = Main.dust[num91];
            Dust dust334 = dust176;
            dust334.velocity *= 3f;
        }
        for (int num92 = 0; num92 < 2; num92++)
        {
            Vector2 val13 = Projectile.position + new Vector2((float)(Projectile.width * Main.rand.Next(100)) / 100f, (float)(Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f;
            center4 = default(Vector2);
            int num93 = Gore.NewGore(Projectile.GetSource_FromThis(), val13, center4, Main.rand.Next(61, 64));
            Main.gore[num93].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)Projectile.width / 2f;
            Gore gore41 = Main.gore[num93];
            Gore gore64 = gore41;
            gore64.velocity *= 0.3f;
            Main.gore[num93].velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
            Main.gore[num93].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
        }
    }
}
