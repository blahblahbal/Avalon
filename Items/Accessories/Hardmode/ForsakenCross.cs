using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Neck)]
public class ForsakenCross : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.longInvince = true;
		ContentSamples.ItemsByType[ModContent.ItemType<ForsakenRelic>()].ModItem.UpdateAccessory(player, hideVisual);
		//ModContent.GetInstance<ForsakenCross>().UpdateAccessory(player, hideVisual);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<ForsakenRelic>())
			.AddIngredient(ItemID.CrossNecklace)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
