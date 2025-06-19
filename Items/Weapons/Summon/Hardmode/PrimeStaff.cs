using Avalon.Buffs.Minions;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode;

public class PrimeStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
	}
	public override void SetDefaults()
	{
		Item.DefaultToMinionWeaponUpgradeable(50, 6.5f, 30, 14);
		Item.buffType = ModContent.BuffType<PrimeArms>();
		Item.shoot = ModContent.ProjectileType<PrimeArmsCounter>();
		Item.shootSpeed = 0f;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item44;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 50)
			.AddIngredient(ItemID.HallowedBar, 12)
			.AddIngredient(ItemID.SoulofFright, 20)
			.AddIngredient(ModContent.ItemType<Material.Shards.DemonicShard>(), 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override bool CanUseItem(Player player)
	{
		return true;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
		player.AddBuff(Item.buffType, 2);

		// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
		var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;

		// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
		return false;
	}
}

