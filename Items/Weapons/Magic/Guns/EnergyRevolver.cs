using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Avalon.Projectiles.Magic.Guns;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Guns;

public class EnergyRevolver : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this);
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeapon(50, 20, ModContent.ProjectileType<EnergyLaser>(), 36, 2f, 6, 5f, 6, 6, true);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 4);
		Item.UseSound = null;
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override float UseSpeedMultiplier(Player player)
	{
		return player.altFunctionUse == 2 ? 0.17f : 1;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse != 2)
			velocity = velocity.RotatedByRandom(0.025f);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 newPos = position + new Vector2(0, -6 * player.direction).RotatedBy(velocity.ToRotation());
		Vector2 beamStartPos = newPos + Vector2.Normalize(velocity) * 48;
		if (player.altFunctionUse != 2)
		{
			Projectile.NewProjectile(source, newPos, velocity, type, damage, knockback, player.whoAmI, beamStartPos.X, beamStartPos.Y);
		}
		else
		{
			Projectile.NewProjectile(source, beamStartPos, velocity * 2f, ModContent.ProjectileType<EnergyBall>(), damage * 2, knockback, player.whoAmI);
		}
		return false;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, -2);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.LaserRifle)
			.AddIngredient(ItemID.Lens, 10)
			.AddIngredient(ModContent.ItemType<Material.BloodshotLens>(), 5)
			.AddIngredient(ItemID.BlackLens)
			.AddIngredient(ItemID.SoulofFright, 16)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}