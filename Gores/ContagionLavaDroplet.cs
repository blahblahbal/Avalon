using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Avalon.Gores;
public class ContagionLavaDroplet : LiquidDropletGoreBase
{
	public override int CloneType => GoreID.LavaDrip;
	public override Vector3? LightColor => new(0.5f, 0f, 2f);
}
