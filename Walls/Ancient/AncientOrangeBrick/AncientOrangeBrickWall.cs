using Avalon.Tiles.Ancient.AncientPurpleBrick;
using Avalon.Tiles.Ancient.AncientYellowBrick;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Ancient.AncientOrangeBrick;

[LegacyName("AncientOrangeBrickWall")]
public class AncientOrangeBrickWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<AncientOrangeBrickWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tiles.Ancient.AncientOrangeBrick.AncientOrangeBrick>())
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
public class AncientOrangeBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(63, 36, 24));
        DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
    }
}
[LegacyName("UnsafeAncientOrangeBrickWall")]
public class UnsafeAncientOrangeBrickWallItem : ModItem
{
	public override string Texture => ModContent.GetInstance<AncientOrangeBrickWallItem>().Texture;
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
		ItemID.Sets.DrawUnsafeIndicator[Type] = true;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientOrangeBrickWallItem>()] = Type;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientOrangeBrickWall>());
	}
}
public class UnsafeAncientOrangeBrickWall : ModWall
{
	public override string Texture => ModContent.GetInstance<AncientOrangeBrickWall>().Texture;
	public override void SetStaticDefaults()
	{
		Main.wallDungeon[Type] = true;
		RegisterItemDrop(ModContent.ItemType<AncientOrangeBrickWallItem>());
		AddMapEntry(new Color(65, 61, 42));
		DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
		Main.wallBlend[Type] = ModContent.WallType<AncientOrangeBrickWall>();
	}
}
