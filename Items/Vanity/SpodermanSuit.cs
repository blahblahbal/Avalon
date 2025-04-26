using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Body)]
public class SpodermanSuit : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToVanity();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 10);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
		   .AddIngredient(ItemID.Silk, 20)
		   .AddIngredient(ItemID.FireblossomSeeds, 3)
		   .AddIngredient(ItemID.MushroomGrassSeeds, 1)
		   .AddTile(TileID.Loom)
		   .Register();
	}
}
