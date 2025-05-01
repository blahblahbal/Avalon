using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class XanthophyteBar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToBar(23);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 0, 15);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(ModContent.ItemType<Ores.XanthophyteOre>(), 5).AddTile(TileID.AdamantiteForge).Register();
	}
}
