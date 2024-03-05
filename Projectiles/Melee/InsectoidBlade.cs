using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class InsectoidBlade : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = ProjAIStyleID.ShortSword;
        AIType = ProjectileID.TinShortswordStab;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.hide = true;
        Projectile.timeLeft = 360;
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
        SetVisualOffsets();

        if (Main.rand.NextBool(6))
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<MosquitoDust>(), Projectile.velocity * 2);
            d.fadeIn = Main.rand.Next(30);
        }
    }
    private void SetVisualOffsets()
    {
        // 32 is the sprite size (here both width and height equal)
        const int HalfSpriteWidth = 58 / 2;

        int HalfProjWidth = Projectile.width / 2;
        int HalfProjHeight = Projectile.height / 2;

        // Vanilla configuration for "hitbox in middle of sprite"
        DrawOriginOffsetX = 0;
        DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
        DrawOriginOffsetY = -(HalfSpriteWidth - HalfProjHeight);

        // Vanilla configuration for "hitbox towards the end"
        //if (Projectile.spriteDirection == 1) {
        //	DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth);
        //	DrawOffsetX = (int)-DrawOriginOffsetX * 2;
        //	DrawOriginOffsetY = 0;
        //}
        //else {
        //	DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth);
        //	DrawOffsetX = 0;
        //	DrawOriginOffsetY = 0;
        //}
    }
}
