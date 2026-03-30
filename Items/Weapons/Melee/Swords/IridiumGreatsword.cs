using Avalon.Common.Extensions;
using Avalon.Common.Interfaces;
using Avalon.Dusts;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class IridiumGreatsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(30, 5.4f, 35, crit: 6);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
		Item.useTime = 2;
		Item.shoot = ModContent.ProjectileType<IridiumGreatswordBubble>();
		Item.shootSpeed = 8;
		Item.channel = true;
		Item.useLimitPerAnimation = 4;
		Item.useTurn = false;
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.OnHitNPC(player, target, hit, damageDone);
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		ClassExtensions.GetPointOnSwungItemPath(64, 64, 0.2f + 0.8f * Main.rand.NextFloat(), player.HeldItem.scale, out var location2, out var outwardDirection2, player);
		Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
		Dust d = Dust.NewDustPerfect(location2 + Main.rand.NextVector2Circular(12, 12), ModContent.DustType<SimpleColorableGlowyDust>(), (vector2 * Main.rand.NextFloat(4,8)) + Main.rand.NextVector2Circular(1, 1));
		d.noGravity = true;
		d.scale *= 1f;
		d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity += Main.rand.NextVector2Circular(3, 3);
		position += Main.rand.NextVector2Circular(6, 16) + velocity * 3;
		damage = (int)(damage * 0.3f);
		knockback *= 0.3f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 14)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
