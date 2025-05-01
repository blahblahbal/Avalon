using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class IckyArrow : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToArrow(8, ModContent.ProjectileType<Projectiles.Ranged.IckyArrow>(), 3.4f, 2f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 0, 8);
	}
	public override void AddRecipes()
	{
		CreateRecipe(5)
			.AddIngredient(ItemID.WoodenArrow, 5)
			.AddIngredient(ModContent.ItemType<YuckyBit>())
			.AddTile(TileID.Anvils)
			.Register();
	}
}
