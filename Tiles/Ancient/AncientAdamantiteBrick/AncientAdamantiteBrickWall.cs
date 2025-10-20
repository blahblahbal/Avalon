using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.AncientAdamantiteBrick;

[LegacyName("AncientAdamantiteBrickWall")]
public class AncientAdamantiteBrickWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<AncientAdamantiteBrickWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<AncientAdamantiteBrick>()).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<AncientAdamantiteBrick>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class AncientAdamantiteBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(148, 57, 101));
        DustType = DustID.Adamantite;
    }
}
