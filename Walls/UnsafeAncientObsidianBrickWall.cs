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
	public class UnsafeAncientObsidianBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			RegisterItemDrop(ItemID.AncientObsidianBrickWall);
			AddMapEntry(new Color(15, 15, 15));
			DustType = DustID.Obsidian;
		}
	}
}
