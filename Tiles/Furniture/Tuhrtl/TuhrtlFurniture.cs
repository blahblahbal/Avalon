using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Avalon.Common.Templates;

namespace Avalon.Tiles.Furniture.Tuhrtl;

public class TuhrtlDoorClosed : ClosedDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.TuhrtlDoor>();
}
public class TuhrtlDoorOpen : OpenDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.TuhrtlDoor>();
}
public class TuhrtlTable : TableTemplate
{
}
public class TuhrtlWorkBench : WorkbenchTemplate
{
}
public class TuhrtlChair : ChairTemplate
{
}
