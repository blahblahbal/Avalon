using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.SepsisWall;

[LegacyName("SepsisBlockWall")]
public class SepsisBlockWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<SepsisBlockWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<SepsisBlock>()).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<SepsisBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}
public class SepsisBlockWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(75, 79, 46));
        DustType = DustID.BrownMoss;
    }
}
