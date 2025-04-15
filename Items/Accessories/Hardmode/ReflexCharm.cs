using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ReflexCharm : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.defense = 2;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 8);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.CobaltShield)
			.AddIngredient(ItemID.SoulofSight, 8)
			.AddIngredient(ItemID.LightShard, 3)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().Reflex = true;
	}
}
