using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class LightningCloud : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
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
    }
}
