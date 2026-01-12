using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class BrokenVigilanteTome : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMisc(24, 28);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.BrokenHeroSword)
			.AddTile(ModContent.TileType<Catalyzer>())
			.Register();

		Recipe.Create(ItemID.BrokenHeroSword)
			.AddIngredient(Type)
			.AddTile(ModContent.TileType<Catalyzer>())
			.Register();
	}
}
