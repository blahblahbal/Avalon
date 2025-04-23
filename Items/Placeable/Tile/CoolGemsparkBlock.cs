using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class CoolGemsparkBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CoolGemsparkBlock>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(15)
			.AddIngredient(ItemID.Glass, 15)
			.AddIngredient(ItemID.Emerald)
			.AddIngredient(ItemID.Sapphire)
			.AddIngredient(ItemID.Amethyst)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(Tiles.CoolGemsparkBlock.R, Tiles.CoolGemsparkBlock.G, Tiles.CoolGemsparkBlock.B);
	}
}
