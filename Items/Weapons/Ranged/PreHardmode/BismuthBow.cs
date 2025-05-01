using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class BismuthBow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBow(12, 0f, 6.8f, 24, 24, width: 12, height: 28);
		Item.value = Item.sellPrice(silver: 18);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 7).AddTile(TileID.Anvils).Register();
	}
}
