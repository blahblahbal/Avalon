using Avalon.Tiles.Contagion.HardenedSnotsand;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.HardenedSnotsandWall;

[LegacyName("HardenedSnotsandWall")]
public class HardenedSnotsandWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<HardenedSnotsandWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<HardenedSnotsandBlock>())
			.AddCondition(Condition.InGraveyard)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ItemID.CrimsonHardenedSand)
			.Register();

		Recipe.Create(ModContent.ItemType<HardenedSnotsandBlock>())
			.AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(ModContent.ItemType<HardenedSnotsandWallItem>())
			.Register();
	}
}
public class HardenedSnotsandWall : ModWall
{
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.HardenedSand[Type] = true;
		Main.wallHouse[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<HardenedSnotsandWall>();
		AddMapEntry(new Color(67, 70, 59));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}
public class HardenedSnotsandWallUnsafe : ModWall
{
	public override string Texture => ModContent.GetInstance<HardenedSnotsandWall>().Texture;
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.HardenedSand[Type] = true;
		WallID.Sets.AllowsUndergroundDesertEnemiesToSpawn[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<HardenedSnotsandWall>();
		AddMapEntry(new Color(67, 70, 59));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}