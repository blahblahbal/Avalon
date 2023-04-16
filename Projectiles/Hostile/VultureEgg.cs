using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class VultureEgg : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 24;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = true;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.timeLeft = 100;
        Projectile.light = 1f;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.scale = 1.6f;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.ai[0] < 2)
        {
            Projectile.ai[0]++;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -0.75f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1.5)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.7f;
            }
        }
        else Projectile.Kill();
        return false;
    }
    public override void Kill(int timeLeft)
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
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 2f);
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

        NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, NPCID.Vulture);
    }
    public override void AI()
    {
        if (Projectile.velocity.Y == 0f)
        {
            Projectile.velocity.X *= 0.94f;
        }
        Projectile.rotation += Projectile.velocity.X * 0.1f;
        Projectile.velocity.Y += 0.2f;
    }
}
