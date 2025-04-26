using Avalon.Projectiles.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

internal class TorchLauncher : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGun(ModContent.ProjectileType<Torch>(), ItemID.Torch, 1, 0f, 8f, 16, 16, width: 24);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 78);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Torch, 50)
			.AddIngredient(ItemID.IronBar, 10)
			.AddIngredient(ItemID.Wood, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
