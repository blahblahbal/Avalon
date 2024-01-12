using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class BloodshotShot : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.BulletDeadeye);
        Projectile.aiStyle = -1;
        Projectile.Size = new Vector2(16);
        Projectile.light = 0;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(1f, 1f, 1f, 0.5f);
    }
    public override void AI()
    {
        Lighting.AddLight(Projectile.Center, new Vector3(Projectile.height / 64f, 0, 0));
        int D = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X, Projectile.velocity.Y);
        Main.dust[D].noGravity = true;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D tex = TextureAssets.Extra[89].Value;
        Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), Color.DarkRed * 0.8f, Projectile.rotation, tex.Size() / 2f, Projectile.height / 16f, SpriteEffects.None);
        return false;
    }
}
