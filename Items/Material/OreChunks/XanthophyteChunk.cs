using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class XanthophyteChunk : ModItem
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
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(silver: 7, copper: 50);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(ModContent.ItemType<Material.Bars.XanthophyteBar>())
	//        .AddIngredient(Type, 5)
	//        .AddTile(TileID.AdamantiteForge)
	//        .Register();
	//}
}
