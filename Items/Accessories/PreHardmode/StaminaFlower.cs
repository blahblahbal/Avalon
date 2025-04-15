using Avalon.Common.Players;
using Avalon.Items.Potions.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class StaminaFlower : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 0, 54);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(ModContent.ItemType<StaminaPotion>())
			//.AddIngredient(ModContent.ItemType<BandofStamina>())
			.AddIngredient(ItemID.JungleRose)
			.AddTile(TileID.TinkerersWorkbench).Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().StamFlower = true;
		player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 90;
	}
}
