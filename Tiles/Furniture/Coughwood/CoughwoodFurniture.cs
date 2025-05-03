using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Coughwood;

public class CoughwoodBathtub : BathtubTemplate { }

public class CoughwoodBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodBed>();
}

public class CoughwoodBookcase : BookcaseTemplate { }

public class CoughwoodCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodCandelabra>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.77f, 0.67f, 0.42f);
}

public class CoughwoodCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodCandle>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.77f, 0.67f, 0.42f);
}

public class CoughwoodChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodChair>();
}

public class CoughwoodChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodChandelier>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.77f, 0.67f, 0.42f);
}

public class CoughwoodChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodChest>();
	//public override IEnumerable<Item> GetItemDrops(int i, int j)
	//{
	//    Tile tile = Main.tile[i, j];
	//    int style = TileObjectData.GetTileStyle(tile);
	//    if (style == 0)
	//    {
	//        yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodChest>());
	//    }
	//    if (style == 1)
	//    {
	//        // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
	//        // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
	//        yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodChest>());
	//    }
	//}
}

public class CoughwoodClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodClock>();
}

public class CoughwoodDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodDoor>();
}

public class CoughwoodDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodDoor>();
}

public class CoughwoodDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodDresser>();
}

public class CoughwoodLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodLamp>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.77f, 0.67f, 0.42f);
}

public class CoughwoodLantern : LanternTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodLantern>();
	public override bool HasFlameTexture => false;
	public override Vector3 LightColor => new(0.77f, 0.67f, 0.42f);
}

public class CoughwoodPiano : PianoTemplate { }

public class CoughwoodPlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<Dusts.CoughwoodDust>();
}

public class CoughwoodSink : SinkTemplate { }

public class CoughwoodSofa : SofaTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodSofa>();
}

public class CoughwoodTable : TableTemplate { }

public class CoughwoodToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodToilet>();
}

public class CoughwoodWorkBench : WorkbenchTemplate { }
