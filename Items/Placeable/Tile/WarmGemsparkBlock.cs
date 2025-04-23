using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class WarmGemsparkBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.WarmGemsparkBlock>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(15)
			.AddIngredient(ItemID.Glass, 15)
			.AddIngredient(ItemID.Ruby)
			.AddIngredient(ItemID.Amber)
			.AddIngredient(ItemID.Topaz)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, Tiles.WarmGemsparkBlock.G, 0);
	}
}
