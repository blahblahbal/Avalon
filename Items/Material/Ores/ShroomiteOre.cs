using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class ShroomiteOre : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.ShroomiteOre>());
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 0, 40);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.ShroomiteBar)
			.AddIngredient(this, 5)
			.AddTile(TileID.AdamantiteForge)
			.Register();
	}
}
