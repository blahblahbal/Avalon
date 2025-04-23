using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class DuskplateBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.TwiliplateBlock>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(40)
			.AddIngredient(ItemID.FallenStar)
			.AddIngredient(ItemID.StoneBlock, 40)
			.AddIngredient(ModContent.ItemType<Material.Ores.BismuthOre>())
			.AddTile(TileID.SkyMill)
			.Register(); //Change when alt added :)
	}
}
