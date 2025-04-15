using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Balloon)]
public class RocketHorseshoeBalloon : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(gold: 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetJumpState<RocketBottleJump>().Enable();
		player.jumpBoost = true;
		player.noFallDmg = true;
		player.hasLuck_LuckyHorseshoe = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<RocketinaBalloon>())
			.AddIngredient(ItemID.LuckyHorseshoe)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
