using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Avalon.Projectiles.Melee;

public class HellboundHalberd : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
    }
    public Player player => Main.player[Projectile.owner];
    public int SwingSpeed = 40;
    public override void SetDefaults()
    {
        Projectile.width = 26;
        Projectile.height = 26;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1f;
        Projectile.ownerHitCheck = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.timeLeft = SwingSpeed;
    }
    public Vector2 swingRadius = Vector2.Zero;
    public bool firstFrame = true;
    public float swordVel;
    public float speed = MathF.PI * 1.2f;
    public float posY;
    public float scaleMult = 1.35f; // set this to same as in the item file
    public override void AI()
    {
        if (player.dead) Projectile.Kill();
        Vector2 toMouse = Vector2.Zero;
        player.heldProj = Projectile.whoAmI;

        if (firstFrame)
        {
            SwingSpeed = player.HeldItem.useAnimation;
            Projectile.timeLeft = SwingSpeed;

            toMouse = Vector2.Normalize(Main.MouseWorld - player.MountedCenter) * player.direction;
            posY = player.Center.Y - Projectile.Center.Y;
            posY = MathF.Sign(posY);
            swingRadius = Projectile.Center - player.MountedCenter;
            swingRadius = swingRadius.RotatedBy(toMouse.ToRotation());
            Projectile.scale = player.HeldItem.scale;
            Projectile.Size *= (float)(player.HeldItem.scale < 1 ? 1 : 1 + player.HeldItem.scale - scaleMult); // this isn't ENTIRELY accurate, but oh well
            firstFrame = false;
        }

        swingRadius = swingRadius.RotatedBy(speed * swordVel / SwingSpeed * player.direction * posY);

        if (Projectile.timeLeft < 5)
        {
            swingRadius *= 0.95f;
        }

        swordVel = MathHelper.Lerp(0f, 2f, Projectile.timeLeft / (float)SwingSpeed);
        Vector2 HandPosition = player.MountedCenter + new Vector2(player.direction * -4f, 0);
        Projectile.Center = swingRadius + HandPosition;

        Projectile.rotation = Vector2.Normalize(Projectile.Center - HandPosition).ToRotation() + (45 * (MathHelper.Pi / 180));
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi);

        Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
        d.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
        d.noGravity = true;
        d.fadeIn = Main.rand.NextFloat(0, 1.5f);
        Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
        d2.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
        d2.noGravity = true;
        d2.fadeIn = Main.rand.NextFloat(0, 1.5f);

        if (Main.rand.NextBool(4))
        {
            Dust dSmall = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            dSmall.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
            dSmall.fadeIn = Main.rand.NextFloat(0, 0.7f);
            dSmall.scale *= 0.5f;
            Dust d2Small = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
            d2Small.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
            d2Small.fadeIn = Main.rand.NextFloat(0, 0.7f);
            d2Small.scale *= 0.5f;
        }
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (targetHitbox.Intersects(projHitbox) || targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, Projectile.Center.Distance(player.Center), Projectile.rotation - (45 * (MathHelper.Pi / 180)), MathHelper.Pi / 16))
        {
            return true;
        }
        return false;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit)
        {
            hit.Knockback *= 1.5f;
        }
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        float diff = target.Center.X - player.Center.X;
        if (diff > 0)
        {
            modifiers.HitDirectionOverride = 1;
        }
        else
        {
            modifiers.HitDirectionOverride = -1;
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/HellboundHalberd", AssetRequestMode.ImmediateLoad).Value;
        Texture2D after = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/HellboundHalberd_after", AssetRequestMode.ImmediateLoad).Value;

        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Vector2 offset = new Vector2((float)(texture.Width * 1.2f * 0.25f), -(float)(texture.Height * 1.2f * 0.25f));

        var spriteDirection = SpriteEffects.None;
        float rotationFlip = 0;
        if (Items.Weapons.Melee.Hardmode.HellboundHalberd.swing == 1)
        {
            if (player.direction == 1)
            {
                spriteDirection = SpriteEffects.None;
            }
            if (player.direction == -1)
            {
                spriteDirection = SpriteEffects.FlipHorizontally;
                rotationFlip = MathHelper.ToRadians(90f);
                offset = new Vector2((float)-(texture.Width * 1.2f * 0.25f), -(float)(texture.Height * 1.2f * 0.25f));
            }
        }
        else if (Items.Weapons.Melee.Hardmode.HellboundHalberd.swing == 0)
        {
            if (player.direction == 1)
            {
                spriteDirection = SpriteEffects.FlipHorizontally;
                rotationFlip = MathHelper.ToRadians(90f);
                offset = new Vector2((float)-(texture.Width * 1.2f * 0.25f), -(float)(texture.Height * 1.2f * 0.25f));
            }
            if (player.direction == -1)
            {
                spriteDirection = SpriteEffects.None;
            }
        }

        for (int i = 0; i < Projectile.oldPos.Length; i++)
        {
            Vector2 drawPosOld = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2f;
            Main.EntitySpriteDraw(after, drawPosOld, frame, lightColor * (1 - (i * 0.25f)) * 0.25f, Projectile.oldRot[i] + rotationFlip, frame.Size() / 2f + offset, Projectile.scale, spriteDirection, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation + rotationFlip, frame.Size() / 2f + offset, Projectile.scale, spriteDirection, 0);

        return false;
    }
}
