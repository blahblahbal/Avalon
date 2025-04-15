using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class GoblinToolbelt : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 5);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.blockRange += 2;
		player.accWatch = 3;
		player.accCompass = 1;
		player.accDepthMeter = 1;
		player.GetModPlayer<AvalonPlayer>().GoblinToolbelt = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.Toolbelt)
			.AddIngredient(ItemID.GPS)
			.AddIngredient(ItemID.TinkerersWorkshop)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override void UpdateInventory(Player player)
	{
		player.accWatch = 3;
		player.accCompass = 1;
		player.accDepthMeter = 1;
	}
}
