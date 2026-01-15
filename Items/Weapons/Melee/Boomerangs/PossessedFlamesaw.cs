using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Boomerangs;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Boomerangs;

public class PossessedFlamesaw : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<PossessedFlamesawProj>(), 95, 9f, 20f, 15, 15, false, noMelee: true, width: 46, height: 16);
		Item.noUseGraphic = true;
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.value = Item.sellPrice(0, 40);
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source,position, velocity, type, damage, knockback,player.whoAmI, ai2: player.altFunctionUse);
		return false;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}