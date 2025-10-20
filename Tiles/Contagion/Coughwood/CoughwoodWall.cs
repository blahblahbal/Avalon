using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.Coughwood;
[LegacyName("CoughwoodWall")]
public class CoughwoodWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<CoughwoodWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<Coughwood>()).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<Coughwood>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}

public class CoughwoodWall : ModWall
{
	public override void SetStaticDefaults()
	{
		Main.wallHouse[Type] = true;
		//ItemDrop = ModContent.ItemType<Items.Placeable.Wall.CoughwoodWall>();
		AddMapEntry(new Color(106, 116, 90));
		DustType = ModContent.DustType<Dusts.CoughwoodDust>();
	}
}