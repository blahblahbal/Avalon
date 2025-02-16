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
	public class UnsafeAncientPurpleBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallDungeon[Type] = true;
			RegisterItemDrop(ModContent.ItemType<Items.Placeable.Wall.AncientPurpleBrickWall>());
			AddMapEntry(new Color(40, 28, 69));
			DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
		}
	}
}
