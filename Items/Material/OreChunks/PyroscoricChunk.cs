using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class PyroscoricChunk : ModItem
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
		Item.rare = ModContent.RarityType<Rarities.MagentaRarity>();
		Item.value = Item.sellPrice(silver: 10);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(ModContent.ItemType<Material.Bars.PyroscoricBar>())
	//        .AddIngredient(Type, 5)
	//        .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
	//        .Register();
	//}
}
