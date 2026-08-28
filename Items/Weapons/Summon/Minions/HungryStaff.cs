using Avalon.Buffs.Summons;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Projectiles.Summon.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Minions;

public class HungryStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMinionWeapon(ModContent.ProjectileType<HungrySummon>(), ModContent.BuffType<Hungry>(), 21, 1.5f, 30);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1);
		Item.UseSound = SoundID.Item44;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		position = Main.MouseWorld;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 14)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		player.AddBuff(Item.buffType, 2);
		var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;
		if (player.GetModPlayer<AvalonPlayer>().FleshArmor)
		{
			projectile.minionSlots = 0.5f;
		}
		return false;
	}
}
