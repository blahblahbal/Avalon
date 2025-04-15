using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ObsidianGlove : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 2);
		Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().CloudGlove = true;
		player.GetModPlayer<AvalonPlayer>().ObsidianGlove = true;
	}
	public override void UpdateVanity(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().CloudGlove = true;
		player.GetModPlayer<AvalonPlayer>().ObsidianGlove = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<CloudGlove>())
			.AddIngredient(ItemID.ObsidianSkull)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
