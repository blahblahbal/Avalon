using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Heartstone;

public class HeartstoneBathtub : BathtubTemplate { }

public class HeartstoneBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneBed>();
}

public class HeartstoneBookcase : BookcaseTemplate { }

public class HeartstoneCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneCandelabra>();
	public override Color FlameColor => new(85, 85, 85, 0);
	public override Vector3 LightColor => new(0.9f, 0.5f, 0.7f);
}

public class HeartstoneCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneCandle>();
	public override Color FlameColor => new(85, 85, 85, 0);
	public override Vector3 LightColor => new(0.9f, 0.5f, 0.7f);
}

public class HeartstoneChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChair>();
}

public class HeartstoneChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChandelier>();
	public override Color FlameColor => new(85, 85, 85, 0);
	public override Vector3 LightColor => new(0.9f, 0.5f, 0.7f);
}
public class HeartstoneLantern : LanternTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneLantern>();
	public override Color FlameColor => new(85, 85, 85, 0);
	public override Vector3 LightColor => new(0.9f, 0.5f, 0.7f);
}
public class HeartstoneChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChest>();
}

public class HeartstoneClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneClock>();
}

public class HeartstoneDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneDoor>();
}

public class HeartstoneDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneDoor>();
}

public class HeartstoneDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneDresser>();
}

public class HeartstoneLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneLamp>();
	public override Color FlameColor => new(85, 85, 85, 0);
	public override Vector3 LightColor => new(0.9f, 0.5f, 0.7f);
}

public class HeartstonePiano : PianoTemplate { }

public class HeartstonePlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<Dusts.HeartstoneDust>();
}

public class HeartstoneSink : SinkTemplate { }

public class HeartstoneSofa : SofaTemplate
{
	public override float SittingHeight => 2;
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneSofa>();
}

public class HeartstoneTable : TableTemplate { }

public class HeartstoneToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneToilet>();
}

public class HeartstoneWorkbench : WorkbenchTemplate { }
