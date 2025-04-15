using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Balloon)]
public class QuackHorseshoeBalloon : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
		Item.value = Item.sellPrice(0, 1);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetJumpState<QuackBottleJump>().Enable();
		player.noFallDmg = true;
		player.jumpBoost = true;
		player.hasLuck_LuckyHorseshoe = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<QuackinaBalloon>())
			.AddIngredient(ItemID.LuckyHorseshoe)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
