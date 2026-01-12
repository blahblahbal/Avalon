using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class ImperviousBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Hellcastle.ImperviousBrick>());
		Item.rare = ItemRarityID.Pink;
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type, 5)
	//        .AddIngredient(ItemID.SoulofMight)
	//        .AddIngredient(ItemID.BlueBrick, 5)
	//        .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

	//    Recipe.Create(Type, 5)
	//        .AddIngredient(ItemID.SoulofMight)
	//        .AddIngredient(ItemID.PinkBrick, 5)
	//        .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

	//    Recipe.Create(Type, 5)
	//        .AddIngredient(ItemID.SoulofMight)
	//        .AddIngredient(ItemID.GreenBrick, 5)
	//        .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

	//    Recipe.Create(Type, 5)
	//        .AddIngredient(ItemID.SoulofMight)
	//        .AddIngredient(ModContent.ItemType<OrangeBrick>(), 5)
	//        .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();
	//}
}
