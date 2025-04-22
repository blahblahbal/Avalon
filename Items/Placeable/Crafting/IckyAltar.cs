using Avalon.Items.Material;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class IckyAltar : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.EvilAltarsPlaced>(), 2);
		Item.width = 24;
		Item.height = 14;
		Item.rare = ItemRarityID.Blue;
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<IckyAltar>())
			.AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 50)
			.AddIngredient(ModContent.ItemType<YuckyBit>(), 10)
			.AddIngredient(ModContent.ItemType<Barfbush>(), 5)
			.AddIngredient(ItemID.SoulofNight, 20)
			.AddIngredient(ModContent.ItemType<Booger>(), 20)
			.AddIngredient(ModContent.ItemType<BacciliteBar>(), 25)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
