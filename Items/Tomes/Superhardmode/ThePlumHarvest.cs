using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Superhardmode;

public class ThePlumHarvest : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Yellow;
		Item.width = dims.Width;
		Item.value = 150000;
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 5;
	}

	//Update Accs not needed - ammo done in ModPlayer

	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<DragonOrb>())
	//        .AddIngredient(ModContent.ItemType<Opal>(), 50)
	//        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
	//        .AddIngredient(ItemID.ShroomiteBar, 12)
	//        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3)
	//        .AddTile(ModContent.TileType<Tiles.TomeForge>())
	//        .Register();
	//}
}
