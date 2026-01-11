using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Furniture;
public class ContagionChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Contagion.ContagionChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 5);
	}
}
