using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class HellboundRemote : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 3;
	}

	public override void SetDefaults()
	{
		Item.width = 14;
		Item.height = 26;
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(1).AddIngredient(ItemID.BeetleHusk).AddIngredient(ItemID.LunarBar, 10).AddIngredient(ModContent.ItemType<GhostintheMachine>()).AddIngredient(ItemID.GuideVoodooDoll).AddIngredient(ModContent.ItemType<FleshyTendril>(), 5).AddTile(ModContent.TileType<Tiles.HallowedAltar>()).Register();
	//}
}
