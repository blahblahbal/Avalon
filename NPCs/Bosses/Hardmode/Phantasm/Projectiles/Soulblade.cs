using Avalon;
using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode.Phantasm.Projectiles;

public class Soulblade : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Type] = 8;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(32);
		Projectile.hostile = true;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 60 * 8;
		Projectile.alpha = 256;
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		Phantasm.ApplyShadowCurse(target);
	}
	public override void AI()
	{
		if (Projectile.timeLeft == 60 * 8)
		{
			SoundEngine.PlaySound(SoundID.Zombie54, Projectile.Center);
		}

		if (Projectile.timeLeft < 60)
		{
			Projectile.alpha += 8;
		}

		Projectile.rotation += 0.2f;

		if (Projectile.alpha > 0)
			Projectile.alpha -= 4;

		Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.TwoPi / (60f * 8));

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit);
		d.noGravity = true;
		d.velocity = Projectile.velocity;
		d.alpha = Projectile.alpha;
	}
	public override void OnKill(int timeLeft)
	{
		for(int i = 0; i < 15; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit);
			d.noGravity = true;
			d.velocity *= 3;
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Asset<Texture2D> texture = TextureAssets.Projectile[Type];
		int frameHeight = texture.Value.Height / Main.projFrames[Projectile.type];
		Rectangle sourceRectangle = new Rectangle(0, frameHeight * Projectile.frame, texture.Width(), frameHeight);
		Vector2 frameOrigin = sourceRectangle.Size() / 2f;

		Vector2 drawPos = Projectile.Center;

		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 drawPosOld = Projectile.oldPos[i] + (Projectile.Size / 2);
			Main.EntitySpriteDraw(texture.Value, drawPosOld - Main.screenPosition, sourceRectangle, new Color(255, 125, 255, 225) * (1 - (i / 8f)) * 0.2f, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.3f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.1f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.15f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.2f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
		return false;
	}
}
