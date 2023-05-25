using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class MasterSwordBeam : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 1;
        Projectile.light = 0.2f;
        Projectile.alpha = 0;
        Projectile.friendly = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        if (this.Projectile.localAI[1] >= 15f)
        {
            return new Color(255, 255, 255, this.Projectile.alpha);
        }
        if (this.Projectile.localAI[1] < 5f)
        {
            return Color.Transparent;
        }
        int num7 = (int)((this.Projectile.localAI[1] - 5f) / 10f * 255f);
        return new Color(num7, num7, num7, num7);
    }
    public override void AI()
    {
        if (Projectile.localAI[1] < 15f)
        {
            Projectile.localAI[1] += 1f;
        }
        else
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.scale -= 0.02f;
                Projectile.alpha += 30;
                if (Projectile.alpha >= 250)
                {
                    Projectile.alpha = 255;
                    Projectile.localAI[0] = 1f;
                }
            }
            else if (Projectile.localAI[0] == 1f)
            {
                Projectile.scale += 0.02f;
                Projectile.alpha -= 30;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.localAI[0] = 0f;
                }
            }
        }

        Projectile.rotation += 0.4f;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
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