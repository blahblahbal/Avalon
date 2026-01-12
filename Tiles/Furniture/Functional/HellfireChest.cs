using Avalon.Common.Templates;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Functional;

public class HellfireChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.HellfireChest>();
}
