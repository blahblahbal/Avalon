using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class GoblinDagger : ModProjectile
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
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection * Main.player[Projectile.owner].gravDir;
        SetVisualOffsets();
        Projectile.position += Vector2.Normalize(Projectile.velocity) * 26;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit)
        {
            Gore.NewGore(Projectile.GetSource_FromThis(), target.Hitbox.ClosestPointInRect(Projectile.Center), Projectile.velocity, GoreID.ShadowMimicCoins, 1);
        }
        if (target.life < 0)
        {
            target.value *= Main.rand.NextFloat(1.5f, 3f);
            if (hit.Crit)
            {
                target.value *= 2;
            }
            for(int i = 0; i < 15; i++)
            {
                Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.GoldCoin);
                d.noGravity = true;
                d.scale = 2;
                d.fadeIn = Main.rand.NextFloat(1,1.5f);
            }
        }
    }
    private void SetVisualOffsets()
    {
        // 38 is the sprite size (here both width and height equal)
        const int HalfSpriteWidth = 38 / 2;

        int HalfProjWidth = Projectile.width / 2;

        if (Projectile.spriteDirection == 1)
        {
            DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth) * Main.player[Projectile.owner].gravDir;
            DrawOffsetX = (int)-DrawOriginOffsetX * 2;
            DrawOriginOffsetY = -6;
            DrawOffsetX -= Main.player[Projectile.owner].gravDir == 1 ? 0 : 20;
        }
        else
        {
            DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth) * Main.player[Projectile.owner].gravDir;
            DrawOffsetX = Main.player[Projectile.owner].gravDir == 1 ? 0 : -20;
            DrawOriginOffsetY = -6;
        }
        DrawOffsetX -= Projectile.spriteDirection * 4;
    }
}
