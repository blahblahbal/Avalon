using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

public class ShadowPulse : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ModContent.RarityType<Rarities.SapphirephlyRarity>();
		Item.value = Item.sellPrice(0, 0, 45);
		Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
	}
	public override void UpdateVanity(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
		player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
		player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<ShadowCharm>())
			.AddIngredient(ModContent.ItemType<PulseVeil>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
