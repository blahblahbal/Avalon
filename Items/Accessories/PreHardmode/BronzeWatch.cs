using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Waist)]
public class BronzeWatch : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.White;
		Item.value = Item.sellPrice(0, 0, 4);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.accWatch < 1) player.accWatch = 1;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 10)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Tables)
			.AddTile(TileID.Chairs)
			.Register();
	}
	public override void UpdateInventory(Player player)
	{
		if (player.accWatch < 1) player.accWatch = 1;
	}
}
