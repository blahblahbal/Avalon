using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

class TomeofDistance : ModItem
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
		Item.value = 15000;
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 2;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Ranged) += 0.15f;
		player.statLifeMax2 += 40;
		player.statManaMax2 += 20;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<MistyPeachBlossoms>())
			.AddIngredient(ModContent.ItemType<TaleoftheRedLotus>())
			.AddIngredient(ModContent.ItemType<MysticalTomePage>())
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
