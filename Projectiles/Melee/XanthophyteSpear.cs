using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class XanthophyteSpear : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = 19;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1.1f;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Melee;
    }
    public float movementFactor
    {
        get => Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }
    public override void AI()
    {
        Player projOwner = Main.player[Projectile.owner];
        Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
        Projectile.direction = projOwner.direction;
        projOwner.heldProj = Projectile.whoAmI;
        projOwner.itemTime = projOwner.itemAnimation;
        Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
        Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
        if (!projOwner.frozen)
        {
            if (movementFactor == 0f)
            {
                movementFactor = 3f;
                Projectile.netUpdate = true;
            }
            if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
            {
                movementFactor -= 2.1f;
            }
            else
            {
                movementFactor += 1.8f;
            }
        }
        Projectile.position += Projectile.velocity * movementFactor;
        if (projOwner.itemAnimation == 0)
        {
            Projectile.Kill();
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
        if (Projectile.spriteDirection == -1)
        {
            Projectile.rotation -= MathHelper.ToRadians(90f);
        }
    }
}
