using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class OsmiumPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(82, 13, 3f, 13, 13);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<OsmiumBar>(), 13)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
