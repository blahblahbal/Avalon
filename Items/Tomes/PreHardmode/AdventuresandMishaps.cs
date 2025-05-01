using Avalon.Common.Extensions;
using Avalon.Items.Material.TomeMats;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

public class AdventuresandMishaps : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(1, 1);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statLifeMax2 += 60;
		player.GetDamage(DamageClass.Generic) += 0.05f;
		player.manaCost -= 0.1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.LifeCrystal, 2)
			.AddIngredient(ModContent.ItemType<FineLumber>(), 10)
			.AddIngredient(ModContent.ItemType<CarbonSteel>(), 5)
			.AddIngredient(ModContent.ItemType<RubybeadHerb>(), 3)
			.AddIngredient(ModContent.ItemType<MysticalTomePage>())
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
