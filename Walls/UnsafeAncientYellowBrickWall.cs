using Avalon.Items.Placeable.Wall;
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
	public class UnsafeAncientYellowBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallDungeon[Type] = true;
			RegisterItemDrop(ModContent.ItemType<Items.Placeable.Wall.AncientYellowBrickWall>());
			AddMapEntry(new Color(65, 61, 42));
			DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
		}
	}
}
