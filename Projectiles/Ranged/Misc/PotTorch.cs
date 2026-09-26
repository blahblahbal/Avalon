using Avalon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Misc;

public class PotTorch : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(12);
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 60;
		Projectile.timeLeft = 600;
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.Y == 0 ? Utils.AngleLerp(Projectile.rotation, MathHelper.PiOver2 * Projectile.direction, 0.4f) : Projectile.rotation + Projectile.direction * 0.2f;

		Projectile.localAI[0] = Projectile.velocity.Y == 0 ? Utils.AngleLerp(Projectile.localAI[0], 0, 0.4f) : Projectile.velocity.ToRotation() - MathHelper.PiOver2;

		Projectile.velocity.Y += 0.2f;

		Projectile.frameCounter++;
		Projectile.frame = (Projectile.frameCounter / 3) % 6;

		if (Main.rand.NextBool(3))
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(0, -2).RotatedBy(Projectile.rotation) + new Vector2(0, 4), DustID.Torch);
			d.noGravity = true;
			d.scale += Main.rand.NextFloat();
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		float minSpeed = 2;
		if (Projectile.oldVelocity.X != Projectile.velocity.X && Projectile.oldVelocity.X > minSpeed || Projectile.oldVelocity.X < -minSpeed)
			Projectile.velocity.X = Projectile.oldVelocity.X * -0.5f;
		minSpeed = 3;
		if (Projectile.oldVelocity.Y != Projectile.velocity.Y && Projectile.oldVelocity.Y > minSpeed || Projectile.oldVelocity.Y < -minSpeed)
			Projectile.velocity.Y = Projectile.oldVelocity.Y * -0.5f;
		if (Projectile.velocity.Y == 0)
		{
			Projectile.velocity.X *= 0.9f;
		}
		return false;
	}
	public override void OnKill(int timeLeft)
	{
		
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 600);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		Vector2 offset = new Vector2(0, 6);

		ulong seed = Main.TileFrameSeed;
		DrawData flame = new(tex, Projectile.Center - Main.screenPosition + offset + new Vector2(0,2) + new Vector2(0,-2).RotatedBy(Projectile.rotation), tex.Frame(1, 7, 0, 1 + Projectile.frame), Color.White, Projectile.localAI[0], new Vector2(7,14), new Vector2(1,Utils.Remap(Projectile.velocity.Length(),2,6,1,3)), SpriteEffects.None, 0);
		Main.EntitySpriteDraw(flame);
		Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + offset, tex.Frame(1, 7, 0, 0), lightColor, Projectile.rotation, tex.Size() / new Vector2(2, 14), 1, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(flame with { color = new Color(128, 128, 128, 0)});
		Main.EntitySpriteDraw(flame with { color = new Color(128, 128, 128, 0), position = flame.position + new Vector2(Utils.RandomInt(ref seed, -2, 3), Utils.RandomInt(ref seed, -2, 3)) });
		return false;
	}
}
