using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.Vanilla;
[LegacyName("UnsafeAncientBlueBrickWall")]
public class UnsafeAncientBlueBrickWallItem : ModItem
{
	public override string Texture => $"Terraria/Images/Item_{ItemID.AncientBlueDungeonBrickWall}";
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
		ItemID.Sets.DrawUnsafeIndicator[Type] = true;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientBlueDungeonBrickWall] = Type;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientBlueBrickWall>());
	}
}
public class UnsafeAncientBlueBrickWall : ModWall
{
	public override string Texture => $"Terraria/Images/Wall_{WallID.AncientBlueBrickWall}";
	public override void SetStaticDefaults()
	{
		Main.wallDungeon[Type] = true;
		RegisterItemDrop(ItemID.AncientBlueDungeonBrickWall);
		AddMapEntry(new Color(27, 31, 42));
		DustType = DustID.DungeonBlue;
		Main.wallBlend[Type] = WallID.AncientBlueBrickWall;
	}
}
