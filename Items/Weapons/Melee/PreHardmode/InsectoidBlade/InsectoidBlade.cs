using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Avalon.Items.Weapons.Melee.PreHardmode.InsectoidBlade;

public class InsectoidBlade : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<InsectoidBladeProj>(), 20, 2f, 12, 3.3f, scale: 0.95f, width: 40, height: 40);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 12)
			.AddIngredient(ModContent.ItemType<MosquitoProboscis>(), 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
public class InsectoidBladeProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<InsectoidBlade>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<InsectoidBlade>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.GoldShortswordStab);
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
		SetVisualOffsets();

		if (Main.rand.NextBool(6))
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<MosquitoDust>(), Projectile.velocity * 2);
			d.fadeIn = Main.rand.Next(30);
		}
	}
	private void SetVisualOffsets()
	{
		const int HalfSpriteWidth = 58 / 2;

		int HalfProjWidth = Projectile.width / 2;
		int HalfProjHeight = Projectile.height / 2;
		DrawOriginOffsetX = 0;
		DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
		DrawOriginOffsetY = -(HalfSpriteWidth - HalfProjHeight);
	}
}
