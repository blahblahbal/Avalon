using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

class TheVoidlands : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Orange;
		Item.width = dims.Width;
		Item.value = 105000;
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 3;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.15f;
		player.GetCritChance(DamageClass.Generic) += 3;
		player.statLifeMax2 += 60;
		player.statManaMax2 += 40;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<BurningDesire>())
			.AddIngredient(ModContent.ItemType<SoutheasternPeacock>())
			.AddIngredient(ModContent.ItemType<FlankersTome>())
			.AddIngredient(ModContent.ItemType<TomeofDistance>())
			.AddIngredient(ModContent.ItemType<EternitysMoon>())
			.AddIngredient(ModContent.ItemType<MediationsFlame>())
			.AddIngredient(ModContent.ItemType<MysticalTomePage>(), 2)
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<BurningDesire>())
			.AddIngredient(ModContent.ItemType<SoutheasternPeacock>())
			.AddIngredient(ModContent.ItemType<FlankersTome>())
			.AddIngredient(ModContent.ItemType<TomeofDistance>())
			.AddIngredient(ModContent.ItemType<TomeoftheRiverSpirits>())
			.AddIngredient(ModContent.ItemType<MysticalTomePage>(), 2)
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<BurningDesire>())
			.AddIngredient(ModContent.ItemType<SoutheasternPeacock>())
			.AddIngredient(ModContent.ItemType<TaleoftheDolt>())
			.AddIngredient(ModContent.ItemType<TaleoftheRedLotus>())
			.AddIngredient(ModContent.ItemType<EternitysMoon>())
			.AddIngredient(ModContent.ItemType<MediationsFlame>())
			.AddIngredient(ModContent.ItemType<MysticalTomePage>(), 2)
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<BurningDesire>())
			.AddIngredient(ModContent.ItemType<SoutheasternPeacock>())
			.AddIngredient(ModContent.ItemType<TaleoftheDolt>())
			.AddIngredient(ModContent.ItemType<TaleoftheRedLotus>())
			.AddIngredient(ModContent.ItemType<TomeoftheRiverSpirits>())
			.AddIngredient(ModContent.ItemType<MysticalTomePage>(), 2)
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
