using Avalon.ModSupport.MLL.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;
public class AlkalineJellyfishJar : ModItem
{
	public override void SetDefaults()
	{
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTurn = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.autoReuse = true;
		Item.maxStack = Item.CommonMaxStack;
		Item.consumable = true;
		Item.createTile = ModContent.TileType<AlkalineJellyfishJarTile>();
		Item.width = 12;
		Item.height = 12;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient<AlkalineJellyfish>()
			.AddIngredient(ItemID.BottledWater)
			.SortAfterFirstRecipesOf(ItemID.PinkJellyfishJar)
			.Register();
	}
}
