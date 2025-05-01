using Avalon.Common.Extensions;
using Avalon.Items.Material.TomeMats;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

public class UnderestimatedResolve : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(1, 1);
		Item.defense = 4;
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
