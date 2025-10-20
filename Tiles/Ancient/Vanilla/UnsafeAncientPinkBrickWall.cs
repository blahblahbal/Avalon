using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.Vanilla
{
	public class UnsafeAncientPinkBrickWallItem : ModItem
	{
		public override string Texture => $"Terraria/Images/Item_{ItemID.AncientPinkDungeonBrickWall}";
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 400;
			ItemID.Sets.DrawUnsafeIndicator[Type] = true;
			ItemID.Sets.ShimmerTransformToItem[ItemID.AncientPinkDungeonBrickWall] = Type;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientPinkBrickWall>());
		}
	}
	public class UnsafeAncientPinkBrickWall : ModWall
	{
		public override string Texture => $"Terraria/Images/Wall_{WallID.AncientPinkBrickWall}";
		public override void SetStaticDefaults()
		{
			Main.wallDungeon[Type] = true;
			RegisterItemDrop(ItemID.AncientPinkDungeonBrickWall);
			AddMapEntry(new Color(41, 28, 36));
			DustType = DustID.DungeonPink;
			Main.wallBlend[Type] = WallID.AncientPinkBrickWall;
		}
	}
}
