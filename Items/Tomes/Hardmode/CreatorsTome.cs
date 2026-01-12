using Avalon.Common.Extensions;
using Avalon.Items.Material.TomeMats;
using Avalon.Items.Tomes.PreHardmode;
using Avalon.Tiles.Furniture.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Hardmode;

public class CreatorsTome : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(4, 1);
		Item.defense = 10;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.2f;
		player.GetCritChance(DamageClass.Generic) += 5;
		player.manaCost -= 0.2f;
		player.statLifeMax2 += 100;
		player.statManaMax2 += 100;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<DragonOrb>())
			.AddIngredient(ModContent.ItemType<TheVoidlands>())
			.AddIngredient(ModContent.ItemType<ScrollofTome>(), 2)
			.AddIngredient(ModContent.ItemType<FineLumber>(), 15)
			.AddIngredient(ModContent.ItemType<Gravel>(), 15)
			.AddIngredient(ModContent.ItemType<Sandstone>(), 15)
			.AddIngredient(ModContent.ItemType<CarbonSteel>(), 15)
			.AddTile(ModContent.TileType<TomeForge>())
			.Register();
	}
}
