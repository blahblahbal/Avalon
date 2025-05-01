using Avalon.Common.Extensions;
using Avalon.Items.Material.Ores;
using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class SulphurCrystal : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 150;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Gems;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMisc(20, 20);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(silver: 4);
	}
	public override void AddRecipes()
	{
		CreateRecipe(25)
			.AddIngredient(ModContent.ItemType<Sulphur>(), 50)
			.AddIngredient(ModContent.ItemType<CoreShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
