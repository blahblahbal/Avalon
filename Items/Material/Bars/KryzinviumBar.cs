using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class KryzinviumBar : ModItem
{
	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6));
		ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.autoReuse = true;
		Item.useTurn = true;
		Item.maxStack = 9999;
		Item.consumable = true;
		Item.createTile = ModContent.TileType<Tiles.PlacedBars>();
		Item.placeStyle = 15;
		Item.rare = ItemRarityID.Red;
		Item.width = dims.Width;
		Item.useTime = 10;
		Item.value = Item.sellPrice(0, 1, 30, 0);
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 15;
		Item.height = dims.Height;
	}
	//public override void AddRecipes()
	//{
	//	CreateRecipe()
	//		.AddIngredient(ModContent.ItemType<PyroscoricBar>())
	//		.AddIngredient(ModContent.ItemType<HydrolythBar>())
	//		.AddIngredient(ModContent.ItemType<BerserkerBar>())
	//		.AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//		.Register();

	//	CreateRecipe()
	//		.AddIngredient(ModContent.ItemType<TritanoriumBar>())
	//		.AddIngredient(ModContent.ItemType<HydrolythBar>())
	//		.AddIngredient(ModContent.ItemType<BerserkerBar>())
	//		.AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//		.Register();
	//}
}
