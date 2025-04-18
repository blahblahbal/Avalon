using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
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
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Green;
		Item.width = dims.Width;
		Item.value = Item.sellPrice(0, 0, 40);
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 2;
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
