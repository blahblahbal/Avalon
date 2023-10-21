using Avalon.NPCs.Bosses.PreHardmode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.DesertBeak;

public class ShrapnelEgg : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = true;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.timeLeft = 200;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.scale = 1f;
        DrawOriginOffsetY -= 5;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = oldVelocity.X * -0.95f;
        }
        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = oldVelocity.Y * -0.95f;
        }
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        return false;
    }
    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        fallThrough = Main.rand.NextBool();
        return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }
    public override void AI()
    {
        if (Projectile.velocity.Y == 0f)
        {
            Projectile.velocity.X *= 0.94f;
        }
        Projectile.rotation += Projectile.velocity.X * 0.05f;
        Projectile.velocity.Y += 0.2f;
        Projectile.ai[2] += 0.04f;
        Projectile.scale = 1 + (float)Math.Sin(Math.Pow(Projectile.ai[2], 2)) * 0.1f;
    }
    public override void OnKill(int timeLeft)
    {
        if (Projectile.penetrate == 1)
        {
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;

            int explosionArea = 60;
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
        }

        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DesertTorch, 0, 0, 0, default, 2f);
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
        Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(12, 12) * Main.rand.NextFloat(0.8f, 1.3f), Mod.Find<ModGore>("VultureShell3").Type, 1);
        Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(12, 12) * Main.rand.NextFloat(0.8f, 1.3f), Mod.Find<ModGore>("VultureShell4").Type, 1);

        Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
        Projectile.ai[0] = Main.rand.Next(8, 12);
        Projectile.netUpdate = true;
        for(int i = 0; i < Projectile.ai[0]; i++)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, Main.rand.NextFloat(7,9)).RotatedBy((MathHelper.TwoPi / Projectile.ai[0]) * i).RotatedByRandom(0.4f), ModContent.ProjectileType<EggShrapnel>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
        }
    }
}
