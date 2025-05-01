using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class NaquadahBar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToBar(10);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 0, 56);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Ores.NaquadahOre>(), 4)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
