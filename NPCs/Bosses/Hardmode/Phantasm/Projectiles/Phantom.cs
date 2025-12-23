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

public class Phantom : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 4;
		ProjectileID.Sets.TrailCacheLength[Type] = 8;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.hostile = true;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = false;
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(ModContent.BuffType<ShadowCurse>(), 60 * 5);
		}
	}
	public override void AI()
	{
		Player player = Main.player[(int)Projectile.ai[0]];
		Projectile.ai[1]++;
		if (Projectile.ai[1] is > 70 and < 110)
		{
			Projectile.velocity += Projectile.Center.DirectionTo(player.Center) * 0.7f;
		}
		if (Projectile.velocity.Length() < 10 && Projectile.ai[1] > 110)
		{
			Projectile.velocity *= 1.1f;
			Projectile.velocity.LengthClamp(10);
		}
		//Projectile.velocity *= 1.01f;
		Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
		Projectile.frameCounter++;
		Projectile.frame = (Projectile.frameCounter / 3) % 4;

		if (Projectile.ai[1] > 120 && !Projectile.tileCollide && !Collision.SolidCollision(Projectile.position,Projectile.width,Projectile.height))
		{
			Projectile.tileCollide = true;
		}
	}
	public override void OnKill(int timeLeft)
	{
		for(int i = 0; i < 15; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>());
			d.noGravity = true;
			d.velocity *= 3;
		}
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
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
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.3f, Projectile.rotation, frameOrigin, Projectile.scale * 1.1f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.15f, Projectile.rotation, frameOrigin, Projectile.scale * 1.2f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225), Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
		return false;
	}
}
