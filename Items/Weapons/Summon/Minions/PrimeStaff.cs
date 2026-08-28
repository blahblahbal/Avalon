using Avalon.Buffs.Summons;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Summon.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Minions;
public class PrimeStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; 
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
		player.AddBuff(Item.buffType, 2);
		var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;
		return false;
	}
}