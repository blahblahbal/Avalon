using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.Snotsandstone;

[LegacyName("SnotsandstoneWall")]
public class SnotsandstoneWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<SnotsandstoneWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<SnotsandstoneBlock>()).AddCondition(Condition.InGraveyard).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<SnotsandstoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class SnotsandstoneWallUnsafe : ModWall
{
	public override string Texture => ModContent.GetInstance<SnotsandstoneWall>().Texture;
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.Sandstone[Type] = true;
		WallID.Sets.AllowsUndergroundDesertEnemiesToSpawn[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<SnotsandstoneWallUnsafe>();
		AddMapEntry(new Color(67, 70, 59));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}
public class SnotsandstoneWall : ModWall
{
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.Sandstone[Type] = true;
		Main.wallHouse[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<SnotsandstoneWall>();
		AddMapEntry(new Color(67, 70, 59));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}