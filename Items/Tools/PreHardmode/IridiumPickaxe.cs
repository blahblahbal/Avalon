using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class IridiumPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(85, 10, 2.6f, 15, 15);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 13)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
