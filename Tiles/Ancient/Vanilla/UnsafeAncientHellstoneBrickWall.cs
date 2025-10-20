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
	[LegacyName("UnsafeAncientHellstoneBrickWall")]
	public class UnsafeAncientHellstoneBrickWallItem : ModItem
	{
		public override string Texture => $"Terraria/Images/Item_{ItemID.AncientHellstoneBrickWall}";
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 400;
			ItemID.Sets.DrawUnsafeIndicator[Type] = true;
			ItemID.Sets.ShimmerTransformToItem[ItemID.AncientHellstoneBrickWall] = Type;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientHellstoneBrickWall>());
		}
	}
	public class UnsafeAncientHellstoneBrickWall : ModWall
	{
		public override string Texture => $"Terraria/Images/Wall_{WallID.AncientHellstoneBrickWall}";
		public override void SetStaticDefaults()
		{
			RegisterItemDrop(ItemID.AncientHellstoneBrickWall);
			AddMapEntry(new Color(67, 37, 37));
			DustType = DustID.MeteorHead;
			Main.wallBlend[Type] = WallID.AncientHellstoneBrickWall;
		}
	}
}
