using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Accessories.Hardmode;

internal class ForsakenHeartNecklace : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 2);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.honeyCombItem = Item;
		player.panic = true;
		ModContent.GetInstance<ForsakenRelic>().UpdateAccessory(player, hideVisual);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<ForsakenRelic>())
			.AddIngredient(ItemID.SweetheartNecklace)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
