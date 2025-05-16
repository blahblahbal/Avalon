using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class FeroziumChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(copper: 20);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(ModContent.ItemType<Placeable.Bar.FeroziumBar>())
	//        .AddIngredient(Type, 5)
	//        .AddTile(TileID.AdamantiteForge)
	//        .Register();

	//    Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
	//        .AddIngredient(ModContent.ItemType<HydrolythChunk>(), 5)
	//        .AddIngredient(Type)
	//        .AddIngredient(ModContent.ItemType<Ore.SolariumOre>())
	//        .AddTile(TileID.AdamantiteForge)
	//        .Register();
	//}
}
