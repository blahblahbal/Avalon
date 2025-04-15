using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class GaseousTsunamiRocketinaJar : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetJumpState<RocketBottleJump>().Enable();
		player.GetJumpState<FartInAJarJump>().Enable();
		player.GetJumpState<TsunamiInABottleJump>().Enable();
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.FartinaJar)
			.AddIngredient(ItemID.TsunamiInABottle)
			.AddIngredient(ModContent.ItemType<RocketinaBottle>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
