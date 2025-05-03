using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.PurpleDungeon;

public class PurpleDungeonBathtub : BathtubTemplate { }

public class PurpleDungeonBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonBed>();
}

public class PurpleDungeonBookcase : BookcaseTemplate { }

public class PurpleDungeonCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonCandelabra>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.9f, 0.45f, 0.6f);
}

public class PurpleDungeonCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonCandle>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.9f, 0.45f, 0.6f);
}

public class PurpleDungeonChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonChair>();
}

public class PurpleDungeonChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonChandelier>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.9f, 0.45f, 0.6f);
}

public class PurpleDungeonChest : ChestTemplate
{
	protected override bool CanBeLocked => true;
	protected override int ChestKeyItemId => ItemID.GoldenKey;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry1"), MapChestName);
	}
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonChest>();
}

public class PurpleDungeonClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonClock>();
}

public class PurpleDungeonDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonDoor>();
}

public class PurpleDungeonDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonDoor>();
}

public class PurpleDungeonDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonDresser>();
}

public class PurpleDungeonLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonLamp>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.9f, 0.45f, 0.6f);
}

public class PurpleDungeonPiano : PianoTemplate { }

public class PurpleBrickPlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<Dusts.PurpleDungeonDust>();
}

public class PurpleDungeonSink : SinkTemplate { }

public class PurpleDungeonSofa : SofaTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonSofa>();
}

public class PurpleDungeonTable : TableTemplate { }

public class PurpleDungeonToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonToilet>();
}

public class PurpleDungeonWorkbench : WorkbenchTemplate { }
