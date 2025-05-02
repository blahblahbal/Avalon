using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture;

public class Dirtalier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Dirtalier>();
	public override Color FlameColor => new(60, 60, 60, 0);
	public override Vector3 LightColor => new(1f / 1.5f, 0.95f / 1.75f, 0.65f / 1.75f);
	public override FlameDustPlacements FlameDustPositions => FlameDustPlacements.All;
	public override int FlameDust => DustID.Torch;
}
