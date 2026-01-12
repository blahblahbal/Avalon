using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Metal;

public class NickelDoorClosed : ClosedDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.NickelDoor>();
}
