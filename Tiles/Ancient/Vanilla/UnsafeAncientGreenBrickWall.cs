using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.Vanilla
{
	public class UnsafeAncientGreenBrickWallItem : ModItem
	{
		public override string Texture => $"Terraria/Images/Item_{ItemID.AncientGreenDungeonBrickWall}";
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 400;
			ItemID.Sets.DrawUnsafeIndicator[Type] = true;
			ItemID.Sets.ShimmerTransformToItem[ItemID.AncientGreenDungeonBrickWall] = Type;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientGreenBrickWall>());
		}
	}
	public class UnsafeAncientGreenBrickWall : ModWall
	{
		public override string Texture => $"Terraria/Images/Wall_{WallID.AncientGreenBrickWall}";
		public override void SetStaticDefaults()
		{
			Main.wallDungeon[Type] = true;
			RegisterItemDrop(ItemID.AncientGreenDungeonBrickWall);
			AddMapEntry(new Color(31, 39, 26));
			DustType = DustID.DungeonGreen;
			Main.wallBlend[Type] = WallID.AncientGreenBrickWall;
		}
	}
}
