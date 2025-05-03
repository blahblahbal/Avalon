using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.OrangeDungeon;

public class OrangeDungeonBathtub : BathtubTemplate { }

public class OrangeDungeonBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonBed>();
}

public class OrangeDungeonBookcase : BookcaseTemplate { }

public class OrangeDungeonCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonCandelabra>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.69f, 0.32f, 0.77f);
}

public class OrangeDungeonCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonCandle>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.69f, 0.32f, 0.77f);
}

public class OrangeDungeonChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonChair>();
}

public class OrangeDungeonChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonChandelier>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.69f, 0.32f, 0.77f);
}

public class OrangeDungeonChest : ChestTemplate
{
	protected override bool CanBeLocked => true;
	protected override int ChestKeyItemId => ItemID.GoldenKey;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry1"), MapChestName);
	}
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonChest>();
}

public class OrangeDungeonClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonClock>();
}

public class OrangeDungeonDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonDoor>();
}

public class OrangeDungeonDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonDoor>();
}

public class OrangeDungeonDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonDresser>();
}

public class OrangeDungeonLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonLamp>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.69f, 0.32f, 0.77f);
}

public class OrangeDungeonPiano : PianoTemplate { }

public class OrangeBrickPlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<Dusts.OrangeDungeonDust>();
}

public class OrangeDungeonSink : SinkTemplate { }

public class OrangeDungeonSofa : SofaTemplate
{
	public override float SittingHeight => 0;
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonSofa>();
}

public class OrangeDungeonTable : TableTemplate { }

public class OrangeDungeonToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonToilet>();
}

public class OrangeDungeonWorkbench : WorkbenchTemplate { }
