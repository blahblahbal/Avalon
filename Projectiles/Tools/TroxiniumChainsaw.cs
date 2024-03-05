using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Projectiles.Tools;

public class TroxiniumChainsaw : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = (int)(dims.Width * 1.15f);
        Projectile.height = (int)(dims.Width * 1.15f);
        Projectile.aiStyle = ProjAIStyleID.Drill;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Melee;
        //Projectile.scale = 1.16f;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor * 4f;
    }
    public override void PostDraw(Color lightColor)
    {
        //Player player = Main.player[Projectile.owner];
        //Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        //Vector2 drawOrigin = new Vector2(0, 0) + (Projectile.spriteDirection != 1 ? new Vector2(48, 0) : Vector2.Zero);
        //Vector2 drawPos = Projectile.Center - Main.screenPosition;
        //Color color = Color.White;
        //Main.EntitySpriteDraw(texture, drawPos, null, Projectile.GetAlpha(color), Projectile.rotation, drawOrigin, Projectile.scale, Projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
    }
}
