using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Balloon)]
public class MegaBundleofBalloons : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(gold: 5);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetJumpState<RocketBottleJump>().Enable();
		player.GetJumpState<TsunamiInABottleJump>().Enable();
		player.GetJumpState<FartInAJarJump>().Enable();
		player.GetJumpState<SandstormInABottleJump>().Enable();
		player.GetJumpState<BlizzardInABottleJump>().Enable();
		player.GetJumpState<CloudInABottleJump>().Enable();
		player.jumpBoost = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<BundleofBalloons>())
			.AddIngredient(ItemID.BundleofBalloons)
			.AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ModContent.ItemType<BundleofHorseshoeBalloons>())
			.Register();
	}
}
