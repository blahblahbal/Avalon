using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class YuckyBit : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Contagion;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.YuckyBit>());
		Rectangle dims = this.GetDims();
		Item.width = dims.Width;
		Item.maxStack = 9999;
		Item.value = 750;
		Item.height = dims.Height;
	}
}
