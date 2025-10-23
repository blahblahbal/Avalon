using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.AncientYellowBrick;

[LegacyName("AncientYellowBrickWall")]
public class AncientYellowBrickWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<AncientYellowBrickWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<AncientYellowBrick>())
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
public class AncientYellowBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(65, 61, 42));
        DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
    }
}
[LegacyName("UnsafeAncientYellowBrickWall")]
public class UnsafeAncientYellowBrickWallItem : ModItem
{
	public override string Texture => ModContent.GetInstance<AncientYellowBrickWallItem>().Texture;
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
		ItemID.Sets.DrawUnsafeIndicator[Type] = true;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientYellowBrickWallItem>()] = Type;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<UnsafeAncientYellowBrickWall>());
	}
}
public class UnsafeAncientYellowBrickWall : ModWall
{
	public override string Texture => ModContent.GetInstance<AncientYellowBrickWall>().Texture;
	public override void SetStaticDefaults()
	{
		Main.wallDungeon[Type] = true;
		RegisterItemDrop(ModContent.ItemType<AncientYellowBrickWallItem>());
		AddMapEntry(new Color(65, 61, 42));
		DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
		Main.wallBlend[Type] = ModContent.WallType<AncientYellowBrickWall>();
	}
}
