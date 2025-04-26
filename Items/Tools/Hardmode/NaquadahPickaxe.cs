using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class NaquadahPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(150, 15, 5f, 10, 25);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 5);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
