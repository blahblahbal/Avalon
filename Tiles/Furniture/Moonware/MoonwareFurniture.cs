using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Moonware;

public class MoonwareBathtub : BathtubTemplate { }

public class MoonwareBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareBed>();
}

public class MoonwareBookcase : BookcaseTemplate { }

public class MoonwareCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareCandelabra>();
	public override Color FlameColor => new(68, 130, 155, 0);
	public override Vector3 LightColor => new(0.32f, 0.62f, 0.74f);
}

public class MoonwareCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareCandle>();
	public override Color FlameColor => new(68, 130, 155, 0);
	public override Vector3 LightColor => new(0.32f, 0.62f, 0.74f);
}

public class MoonwareChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareChair>();
}

public class MoonwareChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareChandelier>();
	public override Color FlameColor => new(68, 130, 155, 0);
	public override Vector3 LightColor => new(0.32f, 0.62f, 0.74f);
}
public class MoonwareLantern : LanternTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareLantern>();
	public override Color FlameColor => new(68, 130, 155, 0);
	public override Vector3 LightColor => new(0.32f, 0.62f, 0.74f);
}
public class MoonwareChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareChest>();
}

public class MoonwareClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareClock>();
}

public class MoonwareDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareDoor>();
}

public class MoonwareDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareDoor>();
}

public class MoonwareDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareDresser>();
}

public class MoonwareLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareLamp>();
	public override Color FlameColor => new(68, 130, 155, 0);
	public override Vector3 LightColor => new(0.32f, 0.62f, 0.74f);
}

public class MoonwarePiano : PianoTemplate { }

public class MoonwarePlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<Dusts.MoonwareDust>();
}

public class MoonwareSink : SinkTemplate { }

public class MoonwareSofa : SofaTemplate
{
	public override float SittingHeight => 1;
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareSofa>();
}

public class MoonwareTable : TableTemplate { }

public class MoonwareToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Moonware.MoonwareToilet>();
}

public class MoonwareWorkBench : WorkbenchTemplate { }
