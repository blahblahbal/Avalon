using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Waist)]
public class BismuthWatch : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 10)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Tables)
			.AddTile(TileID.Chairs).Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.accWatch < 3) player.accWatch = 3;
	}

	public override void UpdateInventory(Player player)
	{
		if (player.accWatch < 3) player.accWatch = 3;
	}
}
