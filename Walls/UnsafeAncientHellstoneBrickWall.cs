using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls
{
	public class UnsafeAncientHellstoneBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			RegisterItemDrop(ItemID.AncientHellstoneBrickWall);
			AddMapEntry(new Color(67, 37, 37));
			DustType = DustID.MeteorHead;
		}
	}
}
