using Avalon.Items.Material.Herbs;
using Avalon.Tiles.Furniture.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class CrimsonAltar : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<EvilAltarsPlaced>(), 1);
		Item.width = 24;
		Item.height = 14;
		Item.rare = ItemRarityID.Blue;
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<CrimsonAltar>())
			.AddIngredient(ItemID.CrimstoneBlock, 50)
			.AddIngredient(ItemID.Vertebrae, 10)
			.AddIngredient(ModContent.ItemType<Bloodberry>(), 5)
			.AddIngredient(ItemID.SoulofNight, 20)
			.AddIngredient(ItemID.TissueSample, 20)
			.AddIngredient(ItemID.CrimtaneBar, 25)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
