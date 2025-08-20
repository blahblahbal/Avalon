using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Avalon.Gores
{
	public class CaesiumLavaDroplet : LiquidDropletGoreBase
	{
		public override int CloneType => GoreID.LavaDrip;
		public override Vector3? LightColor => new(0.7f, 0.4f, 0.4f);
	}
}
