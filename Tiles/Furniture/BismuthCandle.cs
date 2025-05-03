using Avalon.Common.Templates;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture;

public class BismuthCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BismuthCandle>();
	//public override int FlameDust => DustID.Torch;
}
