using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.CrystalMines;

public class CrystalStoneDoorClosed : ClosedDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.CrystalStoneDoor>();
}
