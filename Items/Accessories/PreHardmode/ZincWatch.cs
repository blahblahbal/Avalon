using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Waist)]
public class ZincWatch : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.White;
		Item.value = Item.sellPrice(0, 0, 13);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 10)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Tables)
			.AddTile(TileID.Chairs)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.accWatch < 2) player.accWatch = 2;
	}

	public override void UpdateInventory(Player player)
	{
		if (player.accWatch < 2) player.accWatch = 2;
	}
}
