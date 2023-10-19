using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee; 

public class AeonBeam : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 1;
        Projectile.light = 0.2f;
        Projectile.alpha = 0;
        Projectile.friendly = true;
        Projectile.timeLeft = 80;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.extraUpdates = 3;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255,255,255,64);
    }
    public override void AI()
    {
    }
    public override void OnKill(int timeLeft)
    {
        ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
        particleOrchestraSettings.PositionInWorld = Projectile.Center;
        particleOrchestraSettings.MovementVector = Projectile.velocity;
        ParticleOrchestraSettings settings = particleOrchestraSettings;
        ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
        if(timeLeft != 0)
        {
            particleOrchestraSettings.MovementVector = Projectile.oldVelocity * 0.3f;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.Excalibur, settings, Projectile.owner);

            for (int i = 0; i < 30; i++)
            {
                int D = Dust.NewDust(Projectile.Center, 0, 0, DustID.GoldCoin, 0, 0, 0, default, 3);
                Main.dust[D].color = new Color(255, 255, 255, 0);
                Main.dust[D].noGravity = true;
                Main.dust[D].noLightEmittence = true;
                Main.dust[D].fadeIn = Main.rand.NextFloat(0.5f, 1.5f);
                Main.dust[D].velocity = new Vector2(Main.rand.NextFloat(3, 8), 0).RotatedBy(MathHelper.Pi / 15 * i);
            }

            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack * 2, Projectile.owner);
            }
        }

        //SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        //for (int num394 = 4; num394 < 24; num394++)
        //{
        //    float num395 = Projectile.oldVelocity.X * (30f / (float)num394);
        //    float num396 = Projectile.oldVelocity.Y * (30f / (float)num394);
        //    int num397 = Main.rand.Next(3);
        //    if (num397 == 0)
        //    {
        //        num397 = 15;
        //    }
        //    else if (num397 == 1)
        //    {
        //        num397 = 57;
        //    }
        //    else
        //    {
        //        num397 = 58;
        //    }
        //    int num398 = Dust.NewDust(new Vector2(Projectile.position.X - num395, Projectile.position.Y - num396), 8, 8, num397, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f, 100, default(Color), 1.8f);
        //    Main.dust[num398].velocity *= 1.5f;
        //    Main.dust[num398].noGravity = true;
        //}
    }
}
