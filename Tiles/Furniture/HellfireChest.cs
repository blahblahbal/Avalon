using Avalon.Common.Templates;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture;

public class HellfireChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.HellfireChest>();
}
