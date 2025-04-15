using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class SandyStormcloudinaBottle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetJumpState<CloudInABottleJump>().Enable();
		player.GetJumpState<BlizzardInABottleJump>().Enable();
		player.GetJumpState<SandstormInABottleJump>().Enable();
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.CloudinaBottle)
			.AddIngredient(ItemID.BlizzardinaBottle)
			.AddIngredient(ItemID.SandstorminaBottle)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
