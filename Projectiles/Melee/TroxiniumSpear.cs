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
    protected override float HoldoutRangeMax => 180;
    protected override float HoldoutRangeMin => 40;
    public override void PostDraw(Color lightColor)
    {
        SpriteEffects dir = SpriteEffects.None;
        float num = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
        Texture2D val = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        Player player = Main.player[Projectile.owner];
        Rectangle value = val.Frame();
        Rectangle rect = Projectile.getRect();
        Vector2 vector = Vector2.Zero;
        if (player.direction > 0)
        {
            dir = SpriteEffects.FlipHorizontally;
            vector.X = val.Width;
            num -= (float)Math.PI / 2f;
        }
        if (player.gravDir == -1f)
        {
            if (Projectile.direction == 1)
            {
                dir = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                vector = new Vector2(val.Width, val.Height);
                num -= (float)Math.PI / 2f;
            }
            else if (Projectile.direction == -1)
            {
                dir = SpriteEffects.FlipVertically;
                vector = new Vector2(0f, val.Height);
                num += (float)Math.PI / 2f;
            }
        }
        Vector2 vector2 = Projectile.Center + new Vector2(0f, Projectile.gfxOffY);
        Main.EntitySpriteDraw(val, vector2 - Main.screenPosition, value, Projectile.GetAlpha(Color.White), num, vector, Projectile.scale, dir);
    }
}
