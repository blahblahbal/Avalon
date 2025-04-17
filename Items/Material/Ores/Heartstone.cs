using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class Heartstone : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Gems;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.Heartstone>());
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 3, 33);
	}

	public override void AddRecipes()
	{
		Recipe.Create(ItemID.LifeCrystal).AddIngredient(this, 45).AddTile(TileID.Furnaces).AddCondition(Language.GetOrRegister(Language.GetTextValue("Mods.Avalon.RecipeConditions.notRetroWorld")), () => !AvalonWorld.retroWorld).Register();
		Recipe.Create(ItemID.LifeCrystal).AddIngredient(this, 120).AddTile(TileID.Furnaces).AddCondition(Language.GetOrRegister(Language.GetTextValue("Mods.Avalon.RecipeConditions.retroWorld")), () => AvalonWorld.retroWorld).Register();
		Recipe.Create(Type, 45).AddIngredient(ItemID.LifeCrystal).AddTile(TileID.Furnaces).AddCondition(Language.GetOrRegister(Language.GetTextValue("Mods.Avalon.RecipeConditions.notRetroWorld")), () => !AvalonWorld.retroWorld).Register();
		Recipe.Create(Type, 120).AddIngredient(ItemID.LifeCrystal).AddTile(TileID.Furnaces).AddCondition(Language.GetOrRegister(Language.GetTextValue("Mods.Avalon.RecipeConditions.retroWorld")), () => AvalonWorld.retroWorld).Register();
	}
}
