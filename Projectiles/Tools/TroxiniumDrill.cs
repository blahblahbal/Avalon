using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Projectiles.Tools;

public class TroxiniumDrill : ModProjectile
{

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 26 / 60;
        Projectile.height = dims.Height * 26 / 60 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = ProjAIStyleID.Drill;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.scale = 1.2f;
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
