using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Metal;

public class BismuthCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BismuthCandelabra>();
	public override int FlameDust => DustID.Torch;
}
