using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class NaquadahGreataxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAxe(90, 39, 7f, 10, 35);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 5);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
