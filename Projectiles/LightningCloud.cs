using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class LightningCloud : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 5;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height / Main.projFrames[Projectile.type];
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 3600;
        Projectile.alpha = 20;
        Projectile.aiStyle = -1;
        DrawOriginOffsetX = Projectile.width / 2;
        DrawOriginOffsetY = Projectile.height / 2;
    }

    public override void AI()
    {
        Projectile.frameCounter++;
        if (Projectile.frameCounter == 11)
        {
            SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Thunder2"), new Vector2(Projectile.position.X, Projectile.position.Y + 10));
        }
        if (Projectile.frameCounter > 11f)
        {
            Projectile.alpha += 4;
            if (Projectile.alpha > 200)
            {
                Projectile.active = false;
            }
        }
        if (Projectile.frameCounter == 6 || Projectile.frameCounter == 12)
        {
            Projectile.frame++;
        }
        if (Projectile.frameCounter > 18f)
        {
            Projectile.frame++;
            if (Projectile.frame > 4)
            {
                Projectile.frame = 2;
            }
            Projectile.frameCounter = 13;
        }
    }
}
