using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class DesertLongswordTornado : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 40;
		Projectile.height = 100;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = false;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.timeLeft = 60 * 2;
		Projectile.penetrate = -1;
		Projectile.ignoreWater = true;
		Projectile.alpha = 255;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 20;
	}
	public override void AI()
	{
		if (Projectile.ai[0] == 0)
			SoundEngine.PlaySound(SoundID.Item100 with { Volume = 0.3f, Pitch = 0.3f}, Projectile.position);
		Projectile.rotation += 0.1f;
		Projectile.ai[0]++;
		if(Projectile.timeLeft < 24)
			Projectile.alpha += 16;
		else if (Projectile.alpha > 0)
			Projectile.alpha -= 16;

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Sand, 0,0);
		d.velocity = new Vector2(Projectile.velocity.X * Main.rand.NextFloat(0.8f,1.2f), Main.rand.NextFloat(-2f, 2f));
		d.noGravity = true;
		d.alpha = 255 - (int)(d.position.Distance(Projectile.Bottom) / Projectile.height * 150f * Projectile.Opacity);
		if (Collision.IsWorldPointSolid(Projectile.Center, true))
		{
			Projectile.velocity *= 0.97f;
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Rectangle frame = new Rectangle(0, 0, TextureAssets.Projectile[Type].Value.Width, TextureAssets.Projectile[Type].Value.Height);
		float opacity = 0;
		float scale = 0.1f;
		for (int i = 0; i < Projectile.height; i++)
		{
			Vector2 drawPos = Projectile.Bottom - Main.screenPosition + new Vector2(4).RotatedBy(i * MathHelper.PiOver2 + Main.timeForVisualEffects * 0.1f);
			opacity = MathHelper.Clamp(opacity + (i < Projectile.height - 32 ? 0.01f : -0.02f), 0, 1);
			scale = MathHelper.Clamp(scale + 0.01f, 0, 0.75f);
			Color color = Utils.MultiplyRGBA(new Color(1f, 0.92f, 0.45f, 0.65f),new Color(Lighting.GetSubLight(Projectile.Center))) * opacity;
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos + new Vector2(0, -i), frame, color * Projectile.Opacity, Projectile.rotation + MathHelper.PiOver4 * i * -0.04f * Projectile.direction, new Vector2(TextureAssets.Projectile[Type].Value.Width, TextureAssets.Projectile[Type].Value.Height) / 2, scale, Projectile.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
		}
		return false;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.velocity.Y = -hit.Knockback * 5.5f;
	}
}
