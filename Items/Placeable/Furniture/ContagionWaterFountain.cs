using Avalon.Tiles.Furniture.Functional;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class ContagionWaterFountain : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<WaterFountains>(), 0);
		Item.width = 26;
		Item.height = 36;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.buyPrice(0, 4);
	}
}
