using Avalon.Common.Extensions;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class RottenApple : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}
	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<Projectiles.Ranged.RottenApple>(), 20, 3f, 9f, 15);
	}
	public override void AddRecipes()
	{
		CreateRecipe(20)
			.AddIngredient(ItemID.Apple)
			.AddIngredient(ModContent.ItemType<Material.Shards.UndeadShard>())
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe(20)
			.AddIngredient(ItemID.Apple)
			.AddIngredient(ModContent.ItemType<Material.RottenFlesh>(), 2)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
