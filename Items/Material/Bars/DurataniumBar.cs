using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class DurataniumBar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToBar(9);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 0, 30);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Ores.DurataniumOre>(), 3)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
