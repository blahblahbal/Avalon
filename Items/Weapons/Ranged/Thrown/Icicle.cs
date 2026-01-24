using Avalon.Common.Extensions;
using Avalon.Projectiles.Ranged.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Thrown;

public class Icicle : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<IcicleProj>(), 11, 3f, 9f, 15, false);
	}
	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(ModContent.ItemType<Material.Shards.FrostShard>()).AddTile(TileID.Anvils).Register();
	}
}
