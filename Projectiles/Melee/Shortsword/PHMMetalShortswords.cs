using Avalon.Items.Weapons.Melee.Shortswords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Shortsword;

public class BismuthShortswordProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<BismuthShortsword>().DisplayName;
	public override string Texture => ModContent.GetInstance<BismuthShortsword>().Texture;
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.GoldShortswordStab);
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection * Main.player[Projectile.owner].gravDir;
		SetVisualOffsets();
	}
	private void SetVisualOffsets()
	{
		const int HalfSpriteWidth = 32 / 2;
		const int HalfSpriteHeight = 32 / 2;

		int HalfProjWidth = Projectile.width / 2;
		int HalfProjHeight = Projectile.height / 2;
		DrawOriginOffsetX = 0;
		DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
		DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
	}
}
public class BronzeShortswordProj : BismuthShortswordProj
{
	public override LocalizedText DisplayName => ModContent.GetInstance<BronzeShortsword>().DisplayName;
	public override string Texture => ModContent.GetInstance<BronzeShortsword>().Texture;
}
public class NickelShortswordProj : BismuthShortswordProj
{
	public override LocalizedText DisplayName => ModContent.GetInstance<NickelShortsword>().DisplayName;
	public override string Texture => ModContent.GetInstance<NickelShortsword>().Texture;
}
public class ZincShortswordProj : BismuthShortswordProj
{
	public override LocalizedText DisplayName => ModContent.GetInstance<ZincShortsword>().DisplayName;
	public override string Texture => ModContent.GetInstance<ZincShortsword>().Texture;
}
