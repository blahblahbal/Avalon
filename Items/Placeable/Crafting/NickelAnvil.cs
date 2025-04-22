using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class NickelAnvil : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.NickelAnvil>());
		Item.value = Item.sellPrice(0, 0, 13, 0);
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 5)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ItemID.IronAnvil)
			.Register();
	}
}
