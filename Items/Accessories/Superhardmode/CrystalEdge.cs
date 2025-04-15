using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

public class CrystalEdge : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 4);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().CrystalEdge = true;
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ItemID.CrystalShard, 50)
	//        .AddIngredient(ItemID.SoulofMight, 10)
	//        .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 5)
	//        .AddTile(TileID.TinkerersWorkbench).Register();
	//}
}
