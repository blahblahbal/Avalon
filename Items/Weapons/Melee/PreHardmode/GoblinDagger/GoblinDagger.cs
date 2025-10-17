using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.GoblinDagger;

public class GoblinDagger : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<GoblinDaggerProj>(), 30, 5f, 11, 2.1f, scale: 0.95f);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
	}
}
public class GoblinDaggerProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<GoblinDagger>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<GoblinDagger>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.GoldShortswordStab);
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection * Main.player[Projectile.owner].gravDir;
		SetVisualOffsets();
		Projectile.position += Vector2.Normalize(Projectile.velocity) * 16;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (hit.Crit)
		{
			Gore.NewGore(Projectile.GetSource_FromThis(), target.Hitbox.ClosestPointInRect(Projectile.Center), Projectile.velocity, GoreID.ShadowMimicCoins, 1);
		}
		if (target.life < 0)
		{
			target.value *= Main.rand.NextFloat(1.5f, 3f);
			if (hit.Crit)
			{
				target.value *= 2;
			}
			for (int i = 0; i < 15; i++)
			{
				Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.GoldCoin);
				d.noGravity = true;
				d.scale = 2;
				d.fadeIn = Main.rand.NextFloat(1, 1.5f);
			}
		}
	}
	private void SetVisualOffsets()
	{
		int HalfSpriteWidth = TextureAssets.Projectile[Type].Value.Width / 2;

		int HalfProjWidth = Projectile.width / 2;

		if (Projectile.spriteDirection == 1)
		{
			DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth) * Main.player[Projectile.owner].gravDir;
			DrawOffsetX = (int)-DrawOriginOffsetX * 2;
			DrawOffsetX -= Main.player[Projectile.owner].gravDir == 1 ? 0 : HalfSpriteWidth;
		}
		else
		{
			DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth) * Main.player[Projectile.owner].gravDir;
			DrawOffsetX = Main.player[Projectile.owner].gravDir == 1 ? 0 : -HalfSpriteWidth;
		}
		DrawOriginOffsetY = -(TextureAssets.Projectile[Type].Value.Width / 6);
		DrawOffsetX -= Projectile.spriteDirection * 4;
	}
}
