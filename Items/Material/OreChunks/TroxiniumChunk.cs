using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class TroxiniumChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;

		ItemGlowmask.AddGlow(this, 0);
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.width = dims.Width;
		Item.maxStack = 9999;
		Item.value = 100;
		Item.height = dims.Height;
		Item.rare = ItemRarityID.LightRed;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<Material.Bars.TroxiniumBar>())
			.AddIngredient(Type, 4)
			.AddTile(TileID.AdamantiteForge)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Material.Bars.TroxiniumBar>())
			.Register();
	}
}
