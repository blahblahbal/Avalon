using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class TroxiniumSpear : SpearTemplate
{
	private static Asset<Texture2D> glow;
	public override void SetStaticDefaults()
	{
        glow = ModContent.Request<Texture2D>(Texture + "_Glow");
	}
	public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = 19;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1.3f;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Melee;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor * 4f;
    }
    protected override float HoldoutRangeMax => 180;
    protected override float HoldoutRangeMin => 40;
    public override void PostDraw(Color lightColor)
    {
        SpriteEffects dir = SpriteEffects.None;
        float rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
        Player player = Main.player[Projectile.owner];
        Vector2 origin = Vector2.Zero;
        if (player.direction == 1)
        {
            dir = SpriteEffects.FlipHorizontally;
            origin.X = glow.Value.Width;
            rotation -= MathHelper.PiOver2;
        }
        if (player.gravDir == -1f)
        {
            if (Projectile.direction == 1)
            {
                dir = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                origin = new Vector2(glow.Value.Width, glow.Value.Height);
                rotation -= MathHelper.PiOver2;
            }
            else if (Projectile.direction == -1)
            {
                dir = SpriteEffects.FlipVertically;
                origin = new Vector2(0f, glow.Value.Height);
                rotation += MathHelper.PiOver2;
            }
        }
        Vector2 basePosition = Projectile.Center + new Vector2(0f, Projectile.gfxOffY);
		Main.EntitySpriteDraw(glow.Value, basePosition - Main.screenPosition, default, new Color(255, 255, 255, 0), rotation, origin, Projectile.scale, dir);
    }
}
