using Avalon.Items.Material.TomeMats;
using Avalon.Items.Tomes.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Hardmode;

public class LoveUpandDown : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(4, 1);
		Item.defense = 12;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.15f;
		player.GetCritChance(DamageClass.Generic) += 7;
		player.manaCost -= 0.25f;
		player.statLifeMax2 += 80;
		player.statManaMax2 += 80;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<DragonOrb>())
			.AddIngredient(ModContent.ItemType<AdventuresandMishaps>())
			.AddIngredient(ModContent.ItemType<ScrollofTome>(), 2)
			.AddIngredient(ModContent.ItemType<FineLumber>(), 15)
			.AddIngredient(ModContent.ItemType<Gravel>(), 15)
			.AddIngredient(ModContent.ItemType<Sandstone>(), 15)
			.AddIngredient(ModContent.ItemType<CarbonSteel>(), 15)
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
