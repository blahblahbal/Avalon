using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class ChlorophyteChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(silver: 7, copper: 50);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.ChlorophyteBar)
			.AddIngredient(Type, 5)
			.AddTile(TileID.AdamantiteForge)
			.SortAfterFirstRecipesOf(ItemID.ChlorophyteBar)
			.Register();
	}
}
