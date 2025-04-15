using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class Booger : ModItem
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
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Blue;
		Item.width = dims.Width;
		Item.maxStack = 9999;
		Item.value = 750;
		Item.height = dims.Height;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.consumable = true;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.createTile = ModContent.TileType<Tiles.Booger>();
	}
}
