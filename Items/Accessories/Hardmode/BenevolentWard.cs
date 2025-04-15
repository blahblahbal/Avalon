using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class BenevolentWard : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.sellPrice(0, 2);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.CobaltShield)
			.AddIngredient(ItemID.LunarBar, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().BenevolentWard = true;
	}
}
