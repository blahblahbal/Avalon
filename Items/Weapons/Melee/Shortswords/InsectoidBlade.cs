using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Avalon.Projectiles.Melee.Shortsword;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Shortswords;

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
