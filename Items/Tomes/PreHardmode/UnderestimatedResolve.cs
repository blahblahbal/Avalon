using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

class UnderestimatedResolve : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Green;
		Item.width = dims.Width;
		Item.value = 20000;
		Item.height = dims.Height;
		Item.defense = 4;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 1;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statLifeMax2 += 20;
		player.GetDamage(DamageClass.Ranged) += 0.05f;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Sandstone>(), 10)
			.AddIngredient(ModContent.ItemType<ElementDust>(), 3)
			.AddIngredient(ModContent.ItemType<MysticalClaw>(), 3)
			.AddIngredient(ModContent.ItemType<MysticalTomePage>())
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
