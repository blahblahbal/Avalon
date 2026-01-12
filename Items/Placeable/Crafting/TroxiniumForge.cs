using Avalon.Items.Material.OreChunks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class TroxiniumForge : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Crafting.TroxiniumForge>());
		Item.width = 44;
		Item.height = 30;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Ores.TroxiniumOre>(), 30)
			.AddIngredient(ItemID.Hellforge)
			.AddTile(TileID.MythrilAnvil)
			.SortAfterFirstRecipesOf(ItemID.AdamantiteForge)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TroxiniumChunk>(), 30)
			.AddIngredient(ItemID.Hellforge)
			.AddTile(TileID.MythrilAnvil)
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
