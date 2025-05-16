using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class HydrolythChunk : ModItem
{
	// remove after this is added
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ModContent.RarityType<Rarities.TealRarity>();
		Item.value = Item.sellPrice(silver: 7, copper: 50);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
	//        .AddIngredient(Type, 5)
	//        .AddIngredient(ModContent.ItemType<FeroziumChunk>())
	//        .AddIngredient(ModContent.ItemType<Ore.SolariumOre>())
	//        .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
	//        .Register();

	//    Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
	//        .AddIngredient(Type, 5)
	//        .AddIngredient(ModContent.ItemType<Ore.FeroziumOre>())
	//        .AddIngredient(ModContent.ItemType<Ore.SolariumOre>())
	//        .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
	//        .Register();
	//}
}
