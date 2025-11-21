using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class NickelFence : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.NickelFence>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>())
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOfIndexShift(ItemID.IronFence, 1)
			.Register();

		Recipe.Create(ModContent.ItemType<Material.Bars.NickelBar>())
			.AddIngredient(this, 4)
			.AddTile(TileID.Anvils)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
