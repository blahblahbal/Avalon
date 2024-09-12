using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Avalon.Data.Sets;
using Avalon.Dusts;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace Avalon.Projectiles.Ranged;

public class BoompipeShrapnel : ModProjectile
{
	private static Asset<Texture2D> glow;
	private static Asset<Texture2D> texture;
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 4;
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
		texture = TextureAssets.Projectile[Type];
	}
	public override void SetDefaults()
    {
        Projectile.Size = new Vector2(8);
        Projectile.aiStyle = 1;
        Projectile.tileCollide = true;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Ranged;
	}
	float rotation;
	float oldRotation;
	int initialTimeLeft;
	float glowLerpDiv;
	public override void OnSpawn(IEntitySource source)
	{
		Projectile.frame = Main.rand.Next(4);
		glowLerpDiv = Main.rand.NextFloat(35f, 60f);
		initialTimeLeft = Projectile.timeLeft;
		rotation = Main.rand.NextFromList(Main.rand.NextFloat(-0.35f, -0.2f), Main.rand.NextFloat(0.2f, 0.35f));
	}
	public override bool PreAI()
	{
		oldRotation = Projectile.rotation;
		return true;
	}
	public override void AI()
	{
		Projectile.rotation = oldRotation + rotation;
		if ((initialTimeLeft - Projectile.timeLeft) / glowLerpDiv * 10f < 15f && Main.rand.NextBool(1 + (int)((initialTimeLeft - Projectile.timeLeft) / glowLerpDiv * 15f)))
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 1f);
			if (!Main.rand.NextBool(4))
			{
				Main.dust[d].scale = 1.3f;
				Main.dust[d].noGravity = true;
			}
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		for (int i = 0; i < 4; i++)
		{
			int dustType = ModContent.DustType<BrimstoneDust>();
			// just a lazy way of making the dust colour "fade" like the projectile does
			if (initialTimeLeft - Projectile.timeLeft < glowLerpDiv / 4)
			{
				dustType = ModContent.DustType<BoompipeShrapnelDust>();
			}
			else if (initialTimeLeft - Projectile.timeLeft < glowLerpDiv / 2 && Main.rand.NextBool(2))
			{
				dustType = ModContent.DustType<BoompipeShrapnelDust>();
			}
			else if (initialTimeLeft - Projectile.timeLeft < glowLerpDiv && Main.rand.NextBool(4))
			{
				dustType = ModContent.DustType<BoompipeShrapnelDust>();
			}
			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0, 0, 0, default, 0.7f);
		}
		SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
		return base.OnTileCollide(oldVelocity);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		int frameHeight = texture.Value.Height / Main.projFrames[Projectile.type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Value.Width, frameHeight);
		Color glowLerp = Color.Lerp(new Color(255, 255, 255, 0), new Color(0, 0, 0, 0), (initialTimeLeft - Projectile.timeLeft) / glowLerpDiv);

		Main.EntitySpriteDraw(texture.Value, Projectile.position - Main.screenPosition + (Projectile.Size / 2f), frame, lightColor, Projectile.rotation, new Vector2(texture.Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(glow.Value, Projectile.position - Main.screenPosition + (Projectile.Size / 2f), frame, glowLerp, Projectile.rotation, new Vector2(texture.Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
