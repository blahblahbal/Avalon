using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class BismuthBroadsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(16, 6.5f, 18, false, width: 24, height: 28);
		Item.value = Item.sellPrice(silver: 24);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 8).AddTile(TileID.Anvils).Register();
	}
}
