using Avalon.Common.Players;
using Avalon.Items.Consumables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class RestorationBand : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.LifeCrystal)
			.AddIngredient(ItemID.ManaCrystal)
			.AddIngredient(ModContent.ItemType<StaminaCrystal>())
			.AddIngredient(ItemID.Shackle, 2)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.lifeRegen++;
		player.manaRegenBonus += 10;
		player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegenCost = (int)(player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegenCost * 0.9f);
	}
}
