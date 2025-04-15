using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;
public class SixSidedDie : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 6);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().SixSidedDie = true;
		//player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit = 20; // test
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<BlackWhetstone>())
	//        .AddIngredient(ModContent.ItemType<BloodyWhetstone>())
	//        .AddIngredient(ItemID.SharkToothNecklace)
	//        .AddIngredient(ItemID.SoulofFright, 10)
	//        .AddTile(TileID.MythrilAnvil)
	//        .Register();
	//}
}
