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
        Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        Player player = Main.player[Projectile.owner];
        Vector2 origin = Vector2.Zero;
        if (player.direction == 1)
        {
            dir = SpriteEffects.FlipHorizontally;
            origin.X = texture.Width;
            rotation -= MathHelper.PiOver2;
        }
        if (player.gravDir == -1f)
        {
            if (Projectile.direction == 1)
            {
                dir = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                origin = new Vector2(texture.Width, texture.Height);
                rotation -= MathHelper.PiOver2;
            }
            else if (Projectile.direction == -1)
            {
                dir = SpriteEffects.FlipVertically;
                origin = new Vector2(0f, texture.Height);
                rotation += MathHelper.PiOver2;
            }
        }
        Vector2 basePosition = Projectile.Center + new Vector2(0f, Projectile.gfxOffY);
        Main.EntitySpriteDraw(texture, basePosition - Main.screenPosition, default, Projectile.GetAlpha(Color.White), rotation, origin, Projectile.scale, dir);
    }
}
