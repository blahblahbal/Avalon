using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
		Main.projFrames[Type] = 4;
		ProjectileID.Sets.TrailCacheLength[Type] = 8;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.DamageType = DamageClass.Melee;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.alpha = 64;
		Projectile.scale = 0.75f;
		Projectile.tileCollide = false;
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
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit);
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
			Main.EntitySpriteDraw(texture.Value, drawPosOld - Main.screenPosition, sourceRectangle, new Color(255, 125, 255, 225) * (1 - (i / 8f)) * 0.2f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.3f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.1f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.15f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.2f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
		return false;
	}
}
