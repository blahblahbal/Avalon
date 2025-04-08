using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

class FlankersTome : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Blue;
		Item.width = dims.Width;
		Item.value = 15000;
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 1;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Melee) += 0.1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<StrongVenom>(), 3)
			.AddIngredient(ModContent.ItemType<FineLumber>(), 35)
			.AddIngredient(ModContent.ItemType<RubybeadHerb>(), 3)
			.AddIngredient(ModContent.ItemType<MysticalTomePage>())
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
