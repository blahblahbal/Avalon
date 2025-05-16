using Avalon.Common.Extensions;
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
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(silver: 7, copper: 50);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<Bars.TroxiniumBar>())
			.AddIngredient(Type, 4)
			.AddTile(TileID.AdamantiteForge)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Bars.TroxiniumBar>())
			.Register();
	}
}
