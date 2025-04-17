using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class ScrollofTome : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 2;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.DroppedTomeMats;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTomeMaterial();
		Item.rare = ItemRarityID.Pink;
	}
}
