using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.OreSwords;

public class BismuthShortsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<BismuthShortswordProj>(), 14, 5f, 11, 2.1f, scale: 0.95f, width: 50, height: 18);
		Item.value = Item.sellPrice(silver: 18);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 6).AddTile(TileID.Anvils).Register();
	}
}
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
