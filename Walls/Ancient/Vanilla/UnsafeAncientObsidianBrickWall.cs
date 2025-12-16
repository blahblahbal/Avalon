using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Ancient.Vanilla
{
	[LegacyName("UnsafeAncientObsidianBrickWall")]
	public class UnsafeAncientObsidianBrickWallItem : ModItem
	{
		public override string Texture => $"Terraria/Images/Item_{ItemID.AncientObsidianBrickWall}";
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 400;
			ItemID.Sets.DrawUnsafeIndicator[Type] = true;
			ItemID.Sets.ShimmerTransformToItem[ItemID.AncientObsidianBrickWall] = Type;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientObsidianBrickWall>());
		}
	}
	public class UnsafeAncientObsidianBrickWall : ModWall
	{
		public override string Texture => $"Terraria/Images/Wall_{WallID.AncientObsidianBrickWall}";
		public override void SetStaticDefaults()
		{
			RegisterItemDrop(ItemID.AncientObsidianBrickWall);
			AddMapEntry(new Color(15, 15, 15));
			DustType = DustID.Obsidian;
			Main.wallBlend[Type] = WallID.AncientObsidianBrickWall;
		}
	}
}
