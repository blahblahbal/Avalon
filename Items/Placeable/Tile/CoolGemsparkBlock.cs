using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
		Rectangle dims = this.GetDims();
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = ModContent.TileType<Tiles.CoolGemsparkBlock>();
		Item.width = dims.Width;
		Item.useTurn = true;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.maxStack = 9999;
		Item.useAnimation = 15;
		Item.height = dims.Height;
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
