using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Balloon)]
public class QuackinaBalloon : ModItem
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
		player.jumpBoost = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<QuackinaBottle>())
			.AddIngredient(ItemID.ShinyRedBalloon)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
