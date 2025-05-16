using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class HellstoneChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 1, copper: 25);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.HellstoneBar)
			.AddIngredient(Type, 3)
			.AddIngredient(ItemID.Obsidian)
			.AddTile(TileID.Hellforge)
			.SortAfterFirstRecipesOf(ItemID.HellstoneBar)
			.Register();
	}
}
