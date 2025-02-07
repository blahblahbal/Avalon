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
	public class UnsafeAncientPinkBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallDungeon[Type] = true;
			RegisterItemDrop(ItemID.AncientPinkDungeonBrickWall);
			AddMapEntry(new Color(41, 28, 36));
			DustType = DustID.DungeonPink;
		}
	}
}
