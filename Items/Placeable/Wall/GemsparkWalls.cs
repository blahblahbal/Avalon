using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class PeridotGemsparkWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.PeridotGemsparkWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.PeridotGemsparkBlock>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Tile.PeridotGemsparkBlock>())
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.PeridotGemsparkBlock>()).AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<PeridotGemsparkWall>())
			.Register();
	}
	public override void PostUpdate()
	{
		Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.714f * 0.8f, 1f * 0.8f, 0);
	}
}
public class PeridotGemsparkWallOff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.PeridotGemsparkWallOff>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.PeridotGemsparkBlock>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<PeridotGemsparkWall>())
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.PeridotGemsparkBlock>()).AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<PeridotGemsparkWallOff>())
			.Register();
	}
}

public class TourmalineGemsparkWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.TourmalineGemsparkWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.TourmalineGemsparkBlock>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Tile.TourmalineGemsparkBlock>())
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.TourmalineGemsparkBlock>()).AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<TourmalineGemsparkWall>())
			.Register();
	}
	public override void PostUpdate()
	{
		Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0, 1f * 0.8f, 1f * 0.8f);
	}
}
public class TourmalineGemsparkWallOff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.TourmalineGemsparkWallOff>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.TourmalineGemsparkBlock>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<TourmalineGemsparkWall>())
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.TourmalineGemsparkBlock>()).AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<TourmalineGemsparkWallOff>())
			.Register();
	}
}

public class ZirconGemsparkWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.ZirconGemsparkWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.ZirconGemsparkBlock>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Tile.ZirconGemsparkBlock>())
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.ZirconGemsparkBlock>()).AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<ZirconGemsparkWall>())
			.Register();
	}
	public override void PostUpdate()
	{
		Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 1.1f * 0.8f, 0.8f * 0.8f, 0.4f);
	}
}
public class ZirconGemsparkWallOff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.ZirconGemsparkWallOff>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.ZirconGemsparkBlock>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<ZirconGemsparkWall>())
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.ZirconGemsparkBlock>()).AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<ZirconGemsparkWallOff>())
			.Register();
	}
}
