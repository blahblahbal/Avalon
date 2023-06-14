using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.CrystalMines;

public class CrystalStoneDoorOpen : OpenDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.CrystalStoneDoor>();
}
