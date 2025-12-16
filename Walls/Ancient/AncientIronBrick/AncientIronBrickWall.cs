using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Ancient.AncientIronBrick;


[LegacyName("AncientIronBrickWall")]
public class AncientIronBrickWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<AncientIronBrickWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<Tiles.Ancient.AncientIronBrick.AncientIronBrick>()).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<Tiles.Ancient.AncientIronBrick.AncientIronBrick>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class AncientIronBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(108, 92, 78));
        DustType = DustID.Iron;
    }
}
