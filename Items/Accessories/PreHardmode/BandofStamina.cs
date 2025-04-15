using Avalon.Common.Players;
using Avalon.Items.Consumables;
using Avalon.Items.Potions.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.HandsOn)]
public class BandofStamina : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 90;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<StaminaCrystal>(), 3)
			.AddRecipeGroup("GoldBar", 4)
			.AddIngredient(ModContent.ItemType<StaminaPotion>(), 2)
			.AddTile(TileID.TinkerersWorkbench).Register();
	}
}
