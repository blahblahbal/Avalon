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
	public class UnsafeAncientOrangeBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallDungeon[Type] = true;
			RegisterItemDrop(ModContent.ItemType<Items.Placeable.Wall.AncientOrangeBrickWall>());
			AddMapEntry(new Color(63, 36, 24));
			DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
		}
	}
}
