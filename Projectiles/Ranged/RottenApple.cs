using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class RottenApple : ModProjectile
{
    public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 2;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.DamageType = DamageClass.Ranged;
        AIType = ProjectileID.RottenEgg;
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
	}
    public override bool? CanHitNPC(NPC target)
    {
        return target.lifeMax >= 1 && !target.dontTakeDamage;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (target.type == NPCID.Nurse || target.type == NPCID.DoctorBones || target.type == NPCID.WitchDoctor || target.type == NPCID.DrManFly || target.type == NPCID.ZombieDoctor)
        {
            modifiers.FinalDamage *= 3f;
        }
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);
        for (int i = 0; i < 10; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, -Projectile.velocity.X / 3, -Projectile.velocity.Y / 3, Projectile.alpha);
            Main.dust[d].noGravity = !Main.rand.NextBool(3);
            if (Main.dust[d].noGravity)
                Main.dust[d].fadeIn = 1f;
        }
    }
}
