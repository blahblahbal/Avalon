using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class DurataniumWaraxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAxe(75, 35, 4f, 13, 35);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
