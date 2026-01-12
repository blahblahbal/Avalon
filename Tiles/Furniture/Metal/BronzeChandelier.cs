using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Metal;

public class BronzeChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BronzeChandelier>();
	public override int FlameDust => DustID.Torch;
}
