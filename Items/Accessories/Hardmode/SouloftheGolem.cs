using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class SouloftheGolem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().EtherealHeart = true;
		player.GetModPlayer<AvalonPlayer>().HeartGolem = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<EtherealHeart>())
			.AddIngredient(ModContent.ItemType<HeartoftheGolem>())
			.AddTile(TileID.TinkerersWorkbench).Register();
	}
}
