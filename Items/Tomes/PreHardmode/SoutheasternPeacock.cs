using Avalon.Items.Material.TomeMats;
using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

public class SoutheasternPeacock : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(2);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetCritChance(DamageClass.Generic) += 3;
		player.GetKnockback(DamageClass.Summon) += 0.05f;
		player.GetDamage(DamageClass.Summon) += 0.08f;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TomorrowsPhoenix>())
			.AddIngredient(ModContent.ItemType<ChristmasTome>())
			.AddIngredient(ModContent.ItemType<MysticalTomePage>())
			.AddTile(ModContent.TileType<TomeForge>())
			.Register();
	}
}
