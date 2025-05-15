using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class TroxiniumBar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;

		ItemGlowmask.AddGlow(this, 0);
	}
	public override void SetDefaults()
	{
		Item.DefaultToBar(11);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 0, 75);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Ores.TroxiniumOre>(), 4)
			.AddTile(TileID.AdamantiteForge)
			.Register();
	}
}
