using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class HallowedOre : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.HallowedOre>());
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 0, 8);
	}

	public override void AddRecipes()
	{
		Recipe.Create(ItemID.HallowedBar)
			.AddIngredient(this, 5)
			.AddTile(TileID.AdamantiteForge)
			.Register();
	}
}
