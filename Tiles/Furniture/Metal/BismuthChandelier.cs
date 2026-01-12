using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Metal;

public class BismuthChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BismuthChandelier>();
	public override int FlameDust => DustID.Torch;
}
