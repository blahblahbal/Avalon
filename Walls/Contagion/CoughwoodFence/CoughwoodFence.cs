using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.CoughwoodFence;

[LegacyName("CoughwoodFence")]
public class CoughwoodFenceItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<CoughwoodFence>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Coughwood>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOfIndexShift(ItemID.AshWoodFence, 1)
			.Register();

		Recipe.Create(ModContent.ItemType<Coughwood>())
			.AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
public class CoughwoodFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.CoughwoodFence>();
        AddMapEntry(new Color(106, 116, 90));
        DustType = ModContent.DustType<Dusts.CoughwoodDust>();
        Main.wallLight[Type] = true;
        WallID.Sets.AllowsWind[Type] = true;
        WallID.Sets.AllowsPlantsToGrow[Type] = true;
    }
}
