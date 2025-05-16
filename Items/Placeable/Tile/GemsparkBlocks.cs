using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class PeridotGemsparkBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PeridotGemspark>());
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type, 20)
			.AddIngredient(ItemID.Glass, 20)
			.AddIngredient(ModContent.ItemType<Material.Ores.Peridot>())
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ItemID.RubyGemsparkBlock)
			.Register();
	}

	public override void PostUpdate()
	{
		Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.714f, 1f, 0);
	}
}

public class TourmalineGemsparkBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.TourmalineGemspark>());
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type, 20)
			.AddIngredient(ItemID.Glass, 20)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>())
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ItemID.SapphireGemsparkBlock)
			.Register();
	}

	public override void PostUpdate()
	{
		Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0, 1f, 1f);
	}
}

public class ZirconGemsparkBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ZirconGemspark>());
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type, 20)
			.AddIngredient(ItemID.Glass, 20)
			.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>())
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ItemID.AmberGemsparkBlock)
			.Register();
	}

	public override void PostUpdate()
	{
		Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 1.1f, 0.8f, 0.4f);
	}
}
