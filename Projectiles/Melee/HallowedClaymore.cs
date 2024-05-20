using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Items.Weapons.Melee.Hardmode;
using System.Runtime.CompilerServices;
using Terraria.GameContent.Drawing;
namespace Avalon.Projectiles.Melee;

public class HallowedClaymore : ModProjectile
{
    
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
    }
    public Player player => Main.player[Projectile.owner];
    public int SwingSpeed = 40;
	// unused code
	//public override void SetDefaults()
	//{
	//    Projectile.width = 26;
	//    Projectile.height = 26;
	//    Projectile.aiStyle = -1;
	//    Projectile.DamageType = DamageClass.Melee;
	//    Projectile.alpha = 255;
	//    Projectile.friendly = true;
	//    Projectile.penetrate = -1;
	//    Projectile.tileCollide = false;
	//    Projectile.scale = 1;
	//    Projectile.ownerHitCheck = true;
	//    Projectile.usesLocalNPCImmunity = true;
	//    Projectile.localNPCHitCooldown = -1;
	//    Projectile.timeLeft = SwingSpeed;
	//}
	//public Vector2 swingRadius = Vector2.Zero;
	//public bool firstFrame = true;
	//public float swordVel;
	//public float speed = MathF.PI * 1.5f;
	//public float posY;
	//public float scaleMult = 3; // set this to same as in the item file
	//public float direction;
	//public int maxTime;
	//public override void AI()
	//{
	//    //I basically used scaleMult as a way to check if this is a combo swing
	//    scaleMult = Projectile.ai[0];
	//    if (player.dead) Projectile.Kill();
	//    Vector2 toMouse = Vector2.Zero;
	//    player.heldProj = Projectile.whoAmI;

	//    if (firstFrame)
	//    {
	//        SwingSpeed = player.HeldItem.useAnimation * (int)Projectile.ai[1] / 2;
	//        Projectile.timeLeft = maxTime = SwingSpeed;
	//        direction = player.direction;
	//        toMouse = Vector2.Normalize(Main.MouseWorld - player.MountedCenter) * player.direction;
	//        posY = player.Center.Y - Projectile.Center.Y;
	//        posY = MathF.Sign(posY);
	//        swingRadius = Projectile.Center - player.MountedCenter;
	//        swingRadius = swingRadius.RotatedBy(toMouse.ToRotation());
	//        Projectile.scale = player.HeldItem.scale;
	//        Projectile.Size *= (float)(player.HeldItem.scale < 1 ? 1 : 1 + player.HeldItem.scale - scaleMult); // this isn't ENTIRELY accurate, but oh well
	//        firstFrame = false;
	//    }

	//    swingRadius = swingRadius.RotatedBy(speed * swordVel / SwingSpeed * direction * posY * 2);

	//    if (Projectile.timeLeft < Projectile.timeLeft/2)
	//    {
	//        Projectile.scale *= 0.5f;
	//        swingRadius *= 0.98f;
	//    }

	//    swordVel = MathHelper.Lerp(0f, 2f, (Projectile.timeLeft > maxTime * 0.2f ? Projectile.timeLeft - maxTime * 0.2f: 0) / (float)SwingSpeed);
	//    Vector2 HandPosition = player.RotatedRelativePoint(player.MountedCenter) + new Vector2(player.direction * -4f, 0);
	//    Projectile.Center = swingRadius + HandPosition;

	//    Projectile.rotation = Vector2.Normalize(Projectile.Center - HandPosition).ToRotation() + (45 * (MathHelper.Pi / 180));
	//    player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * player.gravDir + (player.gravDir == -1 ? MathHelper.Pi : 0));

	//    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons);
	//    d.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
	//    Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons);
	//    d2.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;

	//    Dust d3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons);
	//    d3.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
	//    d3.alpha = 128;
	//    d3.noGravity = true;
	//}
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
	public float speed = MathF.PI * 1.5f;
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

		if (Projectile.timeLeft < 20)
		{
			Projectile.scale *= 0.99f;
			swingRadius *= 0.98f;
		}

		swordVel = MathHelper.Lerp(0f, 2f, Projectile.timeLeft / (float)SwingSpeed);
		Vector2 HandPosition = player.RotatedRelativePoint(player.MountedCenter) + new Vector2(player.direction * -4f, 0);
		Projectile.Center = swingRadius + HandPosition;

		Projectile.rotation = Vector2.Normalize(Projectile.Center - HandPosition).ToRotation() + (45 * (MathHelper.Pi / 180));
		player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * player.gravDir + (player.gravDir == -1 ? MathHelper.Pi : 0));

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons);
		d.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
		Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons);
		d2.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;

		Dust d3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons);
		d3.velocity = Vector2.Normalize(swingRadius * posY).RotatedBy(MathHelper.PiOver2 * player.direction) * 3 * swordVel;
		d3.alpha = 128;
		d3.noGravity = true;
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (targetHitbox.Intersects(projHitbox) || targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, Projectile.Center.Distance(player.Center), Projectile.rotation - (45 * (MathHelper.Pi / 180)), MathHelper.Pi / 16))
		{
			return true;
		}
		return false;
	}
	//public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	//{
	//    if (hit.Crit)
	//    {
	//        target.AddBuff(BuffID.BrokenArmor, 60 * 12);
	//        hit.Knockback *= 3f;
	//    }
	//}
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
		Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;

		Rectangle frame = texture.Frame();
		Vector2 drawPos = Projectile.Center - Main.screenPosition;
		Vector2 offset = new Vector2((float)(texture.Width * 1.2f * 0.25f), -(float)(texture.Height * 1.2f * 0.25f));

		Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, frame.Size() / 2f + offset, Projectile.scale, SpriteEffects.None, 0);

		return false;
	}
}
