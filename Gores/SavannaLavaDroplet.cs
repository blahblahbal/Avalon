using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Avalon.Gores;
public class SavannaLavaDroplet : LiquidDropletGoreBase
{
	public override int CloneType => GoreID.LavaDrip;
	public override Vector3? LightColor => new(0.7f, 0.7f, 0);
}