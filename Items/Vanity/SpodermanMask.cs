using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
public class SpodermanMask : ModItem
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
		   .AddIngredient(ItemID.Silk, 10)
		   .AddIngredient(ItemID.FireblossomSeeds, 3)
		   .AddTile(TileID.Loom)
		   .Register();
	}
}
