using Avalon.Common.Extensions;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class Icicle : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<Projectiles.Ranged.Icicle>(), 11, 3f, 9f, 15, false);
	}
	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(ModContent.ItemType<Material.Shards.FrostShard>()).AddTile(TileID.Anvils).Register();
	}
}
