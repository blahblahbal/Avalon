using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Avalon.Gores
{
	public class PhantomLavaDroplet : LiquidDropletGoreBase
	{
		public override int CloneType => GoreID.LavaDrip;
		public override Vector3? LightColor => new(0.9f, 0.55f, 0.65f);
	}
}
