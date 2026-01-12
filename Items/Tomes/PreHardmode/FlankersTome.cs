using Avalon.Common.Extensions;
using Avalon.Items.Material.TomeMats;
using Avalon.Tiles.Furniture.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

public class FlankersTome : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(1);
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
			.AddTile(ModContent.TileType<TomeForge>())
			.Register();
	}
}
