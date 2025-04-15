using Avalon.Common.Players;
using Avalon.Items.Accessories.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class RingofReplenishment : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 9);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<RestorationBand>())
			.AddIngredient(ModContent.ItemType<StaminaFlower>())
			.AddIngredient(ItemID.CharmofMyths)
			.AddIngredient(ItemID.ManaFlower)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 60;
		player.manaFlower = true;
		player.GetModPlayer<AvalonStaminaPlayer>().StamFlower = true;
		player.pStone = true;
		player.lifeRegen += 2;
		player.manaRegenDelayBonus += 1f;
		player.manaRegenBonus += 25;
		player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegenCost = (int)(player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegenCost * 0.75f);
	}
}
