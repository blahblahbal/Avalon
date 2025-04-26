using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class DurataniumPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(110, 10, 5f, 13, 25);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 15)
			.AddTile(TileID.Anvils).Register();
	}
}
