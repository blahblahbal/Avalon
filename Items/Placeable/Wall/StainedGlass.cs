using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class LimeStainedGlass : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.LimeStainedGlass>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(20)
		.AddIngredient(ItemID.GlassWall, 20)
		.AddIngredient(ModContent.ItemType<Material.Ores.Peridot>())
		.AddTile(TileID.WorkBenches)
		.Register();
	}
}

public class CyanStainedGlass : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.CyanStainedGlass>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(20)
		.AddIngredient(ItemID.GlassWall, 20)
		.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>())
		.AddTile(TileID.WorkBenches)
		.Register();
	}
}

public class BrownStainedGlass : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.BrownStainedGlass>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(20)
		.AddIngredient(ItemID.GlassWall, 20)
		.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>())
		.AddTile(TileID.WorkBenches)
		.Register();
	}
}
