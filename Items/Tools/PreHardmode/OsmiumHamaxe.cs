using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class OsmiumHamaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHamaxe(68, 95, 19, 4.2f, 14, 14);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<OsmiumBar>(), 12).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 1).AddTile(TileID.Anvils).Register();
	}
}
