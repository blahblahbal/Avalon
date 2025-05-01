using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class RhodiumHamaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHamaxe(65, 90, 17, 4f, 15, 15);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 10).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2).AddTile(TileID.Anvils).Register();
	}
}
