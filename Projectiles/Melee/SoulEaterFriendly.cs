using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class SoulEaterFriendly : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 8;
		ProjectileID.Sets.TrailCacheLength[Type] = 8;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	private const int initialTimeLeft = 3600;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.DamageType = DamageClass.Melee;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.alpha = 64;
		Projectile.scale = 0.75f;
		Projectile.tileCollide = false;
		Projectile.timeLeft = initialTimeLeft;
	}
	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[1] == 1)
		{
			Projectile.ai[0] = Projectile.FindClosestNPC(600, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
		}
		if (Projectile.ai[1] is > 30 and < 60 && Projectile.ai[0] != -1)
		{
			Projectile.velocity = Vector2.Lerp(Main.npc[(int)Projectile.ai[0]].Center.DirectionFrom(Projectile.Center) * 32, Projectile.velocity, 0.98f);
		}
		if (Projectile.velocity.Length() < 8 && Projectile.ai[1] > 60)
		{
			Projectile.velocity *= 1.1f;
			Projectile.velocity.LengthClamp(16);
		}

		Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
		Projectile.frameCounter++;
		Projectile.frame = (Projectile.frameCounter / 7) % 4;
	}
	public override void OnKill(int timeLeft)
	{
		for(int i = 0; i < 15; i++)
		{
			int DustType = Main.rand.NextFloat() < Utils.Remap(initialTimeLeft - timeLeft, 0, 50, 0, 1, true) ? DustID.DungeonSpirit : ModContent.DustType<PhantoplasmDust>();
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustType);
			d.noGravity = true;
			d.velocity *= 3;
		}
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
		Rectangle sourceRectangle = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Width(), frameHeight);
		Vector2 frameOrigin = sourceRectangle.Size() / 2f;

		Vector2 drawPos = Projectile.Center;

		float colorFade = Utils.Remap(initialTimeLeft - Projectile.timeLeft, 0, 60, 0, 1, true);
		Color color = new(255, 255, 255, 225);
		Color colorTrail = new(255, 125, 255, 225);
		Color colorTrailFromRed = colorTrail * (1f - colorFade);
		for (int i = 0; i < 1 + (int)MathF.Ceiling(1f - colorFade); i++)
		{
			if (i == 1)
			{
				color *= 1f - colorFade;
				colorTrail = colorTrailFromRed;
				sourceRectangle.Y += Main.projFrames[Type] * frameHeight / 2;
			}
			else
			{
				colorTrail *= colorFade;
			}
			for (int j = 0; j < Projectile.oldPos.Length; j++)
			{
				Vector2 drawPosOld = Projectile.oldPos[j] + (Projectile.Size / 2);
				Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPosOld - Main.screenPosition, sourceRectangle, colorTrail * (1 - (j / 8f)) * 0.2f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos - Main.screenPosition, sourceRectangle, color * 0.3f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.1f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos - Main.screenPosition, sourceRectangle, color * 0.15f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.2f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos - Main.screenPosition, sourceRectangle, color * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
		}
		return false;
	}
}
