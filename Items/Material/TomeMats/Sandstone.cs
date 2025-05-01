using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class Sandstone : ModItem
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
		Item.rare = ItemRarityID.Green;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type, 5)
			.AddIngredient(ItemID.SandBlock, 10)
			.AddIngredient(ItemID.StoneBlock, 10)
			.AddTile(TileID.Hellforge)
			.Register();
	}
}
