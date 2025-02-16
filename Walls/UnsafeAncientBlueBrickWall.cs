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
	public class UnsafeAncientBlueBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallDungeon[Type] = true;
			RegisterItemDrop(ItemID.AncientBlueDungeonBrickWall);
			AddMapEntry(new Color(27, 31, 42));
			DustType = DustID.DungeonBlue;
		}
	}
}
