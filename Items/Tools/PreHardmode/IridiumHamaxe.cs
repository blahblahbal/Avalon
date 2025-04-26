using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class IridiumHamaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHamaxe(69, 100, 20, 4.5f, 13, 13);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 12)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>())
			.AddTile(TileID.Anvils)
			.Register();
	}
}
