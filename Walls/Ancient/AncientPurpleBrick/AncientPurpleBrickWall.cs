using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Ancient.AncientPurpleBrick;

[LegacyName("AncientPrupleBrickWall")]
public class AncientPurpleBrickWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<AncientPurpleBrickWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tiles.Ancient.AncientPurpleBrick.AncientPurpleBrick>())
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}

public class AncientPurpleBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(40, 28, 69));
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
    }
}
[LegacyName("UnsafeAncientPurpleBrickWall")]
public class UnsafeAncientPurpleBrickWallItem : ModItem
{
	public override string Texture => ModContent.GetInstance<AncientPurpleBrickWallItem>().Texture;
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
		ItemID.Sets.DrawUnsafeIndicator[Type] = true;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientPurpleBrickWallItem>()] = Type;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientPurpleBrickWall>());
	}
}
public class UnsafeAncientPurpleBrickWall : ModWall
{
	public override string Texture => ModContent.GetInstance<AncientPurpleBrickWall>().Texture;
	public override void SetStaticDefaults()
	{
		Main.wallDungeon[Type] = true;
		RegisterItemDrop(ModContent.ItemType<AncientPurpleBrickWallItem>());
		AddMapEntry(new Color(65, 61, 42));
		DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
		Main.wallBlend[Type] = ModContent.WallType<AncientPurpleBrickWall>();
	}
}
