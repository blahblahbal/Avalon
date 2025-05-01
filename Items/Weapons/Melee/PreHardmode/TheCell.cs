using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class TheCell : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}

	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<Projectiles.Melee.Cell>(), 18, 6.5f, 45, 12f, scale: 1f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 10).AddIngredient(ModContent.ItemType<Material.Booger>(), 2).AddTile(TileID.Anvils).Register();
	}
}
