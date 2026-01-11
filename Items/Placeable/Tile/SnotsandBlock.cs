using Avalon.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Avalon.Tiles.Contagion;

namespace Avalon.Items.Placeable.Tile;
public class SnotsandBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
		ItemID.Sets.SandgunAmmoProjectileData[Type] = new(ModContent.ProjectileType<SnotsandSandgunProjectile>(), 5);
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Snotsand>());
		Item.width = 12;
		Item.height = 12;
		Item.ammo = AmmoID.Sand;
		Item.notAmmo = true;
	}
}
