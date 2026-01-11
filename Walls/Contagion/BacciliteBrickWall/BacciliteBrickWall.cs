using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.BacciliteBrickWall;

[LegacyName("BacciliteBrickWall")]
public class BacciliteBrickWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.DemoniteBrickWall);
		Item.createWall = ModContent.WallType<BacciliteBrickWall>();
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<BacciliteBrick>()).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<BacciliteBrick>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}
public class BacciliteBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(59, 70, 47));
        Main.wallLight[Type] = true;
        DustType = ModContent.DustType<Dusts.ChunkstoneBrickDust>();
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.1f;
        g = 0.15f;
        b = 0f;
    }
}
