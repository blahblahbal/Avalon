using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode;

public class GastropodStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
	}
	public override void SetDefaults()
	{
		Item.DefaultToMinionWeapon(ModContent.ProjectileType<Projectiles.Summon.GastrominiSummon0>(), ModContent.BuffType<Buffs.Minions.Gastropod>(), 40, 4.5f, 30);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 1);
		Item.UseSound = SoundID.Item44;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.CrystalShard, 30)
			.AddIngredient(ItemID.Gel, 100)
			.AddIngredient(ItemID.SoulofLight, 20)
			.AddIngredient(ItemID.PixieDust, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
		position = Main.MouseWorld;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
		player.AddBuff(Item.buffType, 2);

		switch (Main.rand.Next(4))
		{
			case 0:
				type = Item.shoot;
				break;
			case 1:
				type = ModContent.ProjectileType<Projectiles.Summon.GastrominiSummon1>();
				break;
			case 2:
				type = ModContent.ProjectileType<Projectiles.Summon.GastrominiSummon2>();
				break;
			case 3:
				type = ModContent.ProjectileType<Projectiles.Summon.GastrominiSummon3>();
				break;
		}

		// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
		var projectile = Projectile.NewProjectileDirect(source, position, Vector2.Zero, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;

		// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
		return false;
	}
}
