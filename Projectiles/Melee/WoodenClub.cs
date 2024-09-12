using System;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Trophy;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Avalon.Items.Tools.PreHardmode;
using Terraria.GameContent;

namespace Avalon.Projectiles.Melee;

public class WoodenClub : ModProjectile
{
	private static Asset<Texture2D> texture;
	public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Marrow Masher");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		texture = TextureAssets.Projectile[Type];
    }
    public Player player => Main.player[Projectile.owner];
    public int SwingSpeed = 255;
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
    public float speed = MathF.PI;
    public float posY;
    public float scaleMult = 1.1f; // set this to same as in the item file
    public override void AI()
    {
        if (player.dead) Projectile.Kill();
        Vector2 toMouse = Vector2.Zero;
        player.heldProj = Projectile.whoAmI;

        if (firstFrame)
        {
            Projectile.timeLeft = player.HeldItem.useAnimation;

            toMouse = Vector2.Normalize(Main.MouseWorld - player.MountedCenter) * player.direction;
            posY = player.Center.Y - Projectile.Center.Y;
            posY = MathF.Sign(posY);
            swingRadius = Projectile.Center - player.MountedCenter;
            swingRadius = swingRadius.RotatedBy(toMouse.ToRotation()) * MathF.Pow(0.997f, 21f);
            Projectile.scale = player.HeldItem.scale * MathF.Pow(0.997f, 21f);
            Projectile.Size *= (float)(player.HeldItem.scale < 1 ? 1 : 1 + player.HeldItem.scale - scaleMult); // this isn't ENTIRELY accurate, but oh well
            firstFrame = false;
        }
        if (Projectile.timeLeft > 34)
        {
            Projectile.scale /= 0.997f;
            swingRadius /= 0.997f;
        }
        if (Projectile.timeLeft > player.HeldItem.useAnimation - 40 && SwingSpeed > player.HeldItem.useAnimation)
        {
            SwingSpeed -= (int)(10 - Math.Sqrt(Projectile.timeLeft)) * 5;
        }

        swingRadius = swingRadius.RotatedBy(speed * swordVel / player.HeldItem.useAnimation * player.direction * posY);

        if (Projectile.timeLeft < 20)
        {
            Projectile.scale *= 0.996f;
            swingRadius *= 0.996f;
        }

        swordVel = MathHelper.Lerp(0f, 2.4f, Projectile.timeLeft / (float)SwingSpeed);
        Vector2 HandPosition = player.RotatedRelativePoint(player.MountedCenter) + new Vector2(player.direction * -4f, 0);
        Projectile.Center = swingRadius + HandPosition;

        Projectile.rotation = Vector2.Normalize(Projectile.Center - HandPosition).ToRotation() + (45 * (MathHelper.Pi / 180));
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * player.gravDir + (player.gravDir == -1 ? MathHelper.Pi : 0));
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (targetHitbox.Intersects(projHitbox) || targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, Projectile.Center.Distance(player.Center), Projectile.rotation - (45 * (MathHelper.Pi / 180)), MathHelper.Pi / 16))
        {
            return true;
        }
        return false;
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
        //Texture2D after = ModContent.Request<Texture2D>(Texture + "_after", AssetRequestMode.ImmediateLoad).Value;

        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Vector2 offset = new Vector2((float)(texture.Value.Width * 1.2f * 0.25f), -(float)(texture.Value.Height * 1.2f * 0.25f));

        //for (int i = 0; i < Projectile.oldPos.Length; i++)
        //{
        //    Vector2 drawPosOld = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2f;
        //    Main.EntitySpriteDraw(after, drawPosOld, frame, Color.Black * (1 - (i * 0.25f)) * 0.25f, Projectile.oldRot[i], frame.Size() / 2f + offset, Projectile.scale, SpriteEffects.None, 0);
        //}
        Main.EntitySpriteDraw(texture.Value, drawPos, frame, lightColor, Projectile.rotation, frame.Size() / 2f + offset, Projectile.scale, SpriteEffects.None, 0);

        return false;
    }
}
