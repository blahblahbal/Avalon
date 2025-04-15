using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Balloon)]
public class BundleofBalloons : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(gold: 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetJumpState<RocketBottleJump>().Enable();
		player.GetJumpState<TsunamiInABottleJump>().Enable();
		player.GetJumpState<FartInAJarJump>().Enable();
		player.jumpBoost = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<RocketinaBalloon>())
			.AddIngredient(ItemID.SharkronBalloon)
			.AddIngredient(ItemID.FartInABalloon)
			.AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ItemID.BundleofBalloons)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<GaseousTsunamiRocketinaJar>())
			.AddIngredient(ItemID.ShinyRedBalloon, 3)
			.AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ItemID.BundleofBalloons)
			.Register();
	}
}
