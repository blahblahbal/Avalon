using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class HallowedChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(silver: 4);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.HallowedBar)
			.AddIngredient(Type, 5)
			.AddTile(TileID.AdamantiteForge)
			.SortAfterFirstRecipesOf(ItemID.HallowedBar)
			.Register();
	}
}
