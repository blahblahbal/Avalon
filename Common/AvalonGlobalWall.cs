using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common
{
	public class AvalonGlobalWall : GlobalWall
	{
		public override void KillWall(int i, int j, int type, ref bool fail)
		{
			if (!fail)
			{
				Main.tile[i, j].Get<AvalonTileData>().IsWallActupainted = false;
			}
		}
	}
}
