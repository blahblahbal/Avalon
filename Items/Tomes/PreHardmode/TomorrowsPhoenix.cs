using Avalon.Common.Extensions;
using Avalon.Items.Material.TomeMats;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

public class TomorrowsPhoenix : ModItem
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
		player.GetDamage(DamageClass.Summon) += 0.08f;
		player.GetKnockback(DamageClass.Summon) += 0.05f;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Gel, 75)
			.AddIngredient(ModContent.ItemType<StrongVenom>(), 3)
			.AddIngredient(ItemID.FallenStar, 15)
			.AddIngredient(ModContent.ItemType<MysticalClaw>(), 3)
			.AddIngredient(ModContent.ItemType<MysticalTomePage>())
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
