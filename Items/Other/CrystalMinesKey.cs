using Avalon.Common.Extensions;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class CrystalMinesKey : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Crystal Mines Key");
		//Tooltip.SetDefault("Opens a Crystal Mines Chest");
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Keys;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 20);
		Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
	}

	// TODO: ADD RECIPE
	//public override void AddRecipes()
	//{
	//    CreateRecipe()
	//        .AddIngredient(ItemID.TempleKey)
	//        .AddIngredient(ModContent.ItemType<ContagionKeyMold>())
	//        .AddIngredient(ItemID.SoulofFright, 5)
	//        .AddIngredient(ItemID.SoulofMight, 5)
	//        .AddIngredient(ItemID.SoulofSight, 5)
	//        .AddTile(TileID.MythrilAnvil)
	//        .Register();
	//}
}
