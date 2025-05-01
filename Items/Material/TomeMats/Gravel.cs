using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class Gravel : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.CraftedTomeMats;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTomeMaterial();
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type, 15)
			.AddIngredient(ItemID.SiltBlock, 20)
			.AddIngredient(ItemID.StoneBlock, 5)
			.AddTile(TileID.Anvils).Register();

		Recipe.Create(Type, 15)
			.AddIngredient(ItemID.SlushBlock, 20)
			.AddIngredient(ItemID.StoneBlock, 5)
			.AddTile(TileID.Anvils).Register();
	}
}
