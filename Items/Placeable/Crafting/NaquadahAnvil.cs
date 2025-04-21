using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class NaquadahAnvil : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.NaquadahAnvil>());
		Item.width = 28;
		Item.height = 14;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 50);
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.MythrilAnvil)
			.Register();
	}
}
