using Avalon.Common.Templates;
using Avalon.Dusts;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.BleachedEbony;

public class BleachedEbonyBathtub : BathtubTemplate { }

public class BleachedEbonyBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyBed>();
}

public class BleachedEbonyBookcase : BookcaseTemplate { }

public class BleachedEbonyCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyCandelabra>();
	public override int FlameDust => DustID.Torch;
}

public class BleachedEbonyCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyCandle>();
	//public override int FlameDust => DustID.Torch;
}

public class BleachedEbonyChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyChair>();
}

public class BleachedEbonyChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyChandelier>();
	public override int FlameDust => DustID.Torch;
}

public class BleachedEbonyChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyChest>();
}

public class BleachedEbonyClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyClock>();
}

public class BleachedEbonyDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyDoor>();
}

public class BleachedEbonyDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyDoor>();
}

public class BleachedEbonyDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyDresser>();
}

public class BleachedEbonyLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyLamp>();
	public override int FlameDust => DustID.Torch;
}

public class BleachedEbonyLantern : LanternTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyLantern>();
	public override float FlameJitterMultX => base.FlameJitterMultX * 0.5f;
	public override float FlameJitterMultY => base.FlameJitterMultY * 0.5f;
}

public class BleachedEbonyPiano : PianoTemplate { }

public class BleachedEbonySink : SinkTemplate { }

public class BleachedEbonySofa : SofaTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonySofa>();
}

public class BleachedEbonyTable : TableTemplate { }

public class BleachedEbonyToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyToilet>();
}

public class BleachedEbonyWorkBench : WorkbenchTemplate { }

public class BleachedEbonyPlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<BleachedEbonyDust>();
}

