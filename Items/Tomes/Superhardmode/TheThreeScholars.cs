using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Superhardmode;

public class TheThreeScholars : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(5, 3);
		Item.defense = 20;
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<DragonOrb>())
	//        .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 25)
	//        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
	//        .AddIngredient(ItemID.IronskinPotion, 10)
	//        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3)
	//        .AddTile(ModContent.TileType<Tiles.TomeForge>())
	//        .Register();

	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<DragonOrb>())
	//        .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 25)
	//        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
	//        .AddIngredient(ItemID.IronskinPotion, 10)
	//        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3)
	//        .AddTile(ModContent.TileType<Tiles.TomeForge>())
	//        .Register();
	//}
}
