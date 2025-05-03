using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.ResistantWood;

public class ResistantWoodBathtub : BathtubTemplate
{
	public override bool LavaDeath => false;
}

public class ResistantWoodBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodBed>();
	public override bool LavaDeath => false;
}

public class ResistantWoodBookcase : BookcaseTemplate
{
	public override bool LavaDeath => false;
}

public class ResistantWoodCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodCandelabra>();
	public override Color FlameColor => new(60, 60, 60, 0);
	public override Vector3 LightColor => new(base.LightColor.X / 1.5f, base.LightColor.Y / 1.75f, base.LightColor.Z / 1.75f);
	public override int FlameDust => DustID.Torch;
	public override bool LavaDeath => false;
}

public class ResistantWoodCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodCandle>();
	public override Color FlameColor => new(60, 60, 60, 0);
	public override Vector3 LightColor => new(base.LightColor.X / 1.5f, base.LightColor.Y / 1.75f, base.LightColor.Z / 1.75f);
	//public override int FlameDust => DustID.Torch;
	public override bool LavaDeath => false;
}

public class ResistantWoodChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodChair>();
	public override bool LavaDeath => false;
}

public class ResistantWoodChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodChandelier>();
	public override Color FlameColor => new(60, 60, 60, 0);
	public override Vector3 LightColor => new(base.LightColor.X / 1.5f, base.LightColor.Y / 1.75f, base.LightColor.Z / 1.75f);
	public override FlameDustPlacements FlameDustPositions => FlameDustPlacements.TopLeft | FlameDustPlacements.TopRight | FlameDustPlacements.MiddleLeft | FlameDustPlacements.MiddleRight;
	public override int FlameDust => DustID.Torch;
	public override bool LavaDeath => false;
}

public class ResistantWoodChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodChest>();
	public override bool LavaDeath => false;
}

public class ResistantWoodClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodClock>();
	public override bool LavaDeath => false;
}

public class ResistantWoodDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodDoor>();
	public override bool LavaDeath => false;
}

public class ResistantWoodDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodDoor>();
	public override bool LavaDeath => false;
}

public class ResistantWoodDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodDresser>();
	public override bool LavaDeath => false;
}

public class ResistantWoodLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodLamp>();
	public override Color FlameColor => new(60, 60, 60, 0);
	public override Vector3 LightColor => new(base.LightColor.X / 1.5f, base.LightColor.Y / 1.75f, base.LightColor.Z / 1.75f);
	public override int FlameDust => DustID.Torch;
	public override bool LavaDeath => false;
}

public class ResistantWoodLantern : LanternTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodLantern>();
	public override Color FlameColor => new(60, 60, 60, 0);
	public override Vector3 LightColor => new(base.LightColor.X / 1.5f, base.LightColor.Y / 1.75f, base.LightColor.Z / 1.75f);
	public override float FlameJitterMultX => base.FlameJitterMultX * 0.5f;
	public override float FlameJitterMultY => base.FlameJitterMultY * 0.5f;
	public override bool LavaDeath => false;
}

public class ResistantWoodPiano : PianoTemplate
{
	public override bool LavaDeath => false;
}

public class ResistantWoodPlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<ResistantWoodDust>();
	public override bool LavaDeath => false;
}

public class ResistantWoodSink : SinkTemplate
{
	public override bool LavaDeath => false;
}

public class ResistantWoodSofa : SofaTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodSofa>();
	public override bool LavaDeath => false;
}

public class ResistantWoodTable : TableTemplate
{
	public override bool LavaDeath => false;
}

public class ResistantWoodToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ResistantWood.ResistantWoodToilet>();
	public override bool LavaDeath => false;
}

public class ResistantWoodWorkBench : WorkbenchTemplate
{
	public override bool LavaDeath => false;
}
