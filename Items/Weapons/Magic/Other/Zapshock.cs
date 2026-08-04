using Avalon;
using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Magic.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Other;

public class Zapshock : ModItem
{
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<ZapshockProjectile>(), 55, 6f, 14, 10, 10, true, width: 20, height: 20);
		//Item.DefaultToProjectileSword(ModContent.ProjectileType<ZapshockProjectile>(),55, 6f, 8,10,10,true,true, width: 20, height : 20);
		Item.value = Item.sellPrice(gold: 6);
		Item.rare = ItemRarityID.LightRed;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 AttackPosition = Main.MouseWorld;
		player.LimitPointToPlayerReachableArea(ref AttackPosition);
		float iterationDistance = ContentSamples.ItemsByType[Type].shootSpeed;
		for (float i = 0; i < position.Distance(AttackPosition); i += iterationDistance)
		{
			Vector2 curCheckPos = position + position.SafeDirectionTo(AttackPosition) * i;
			if (position.DistanceSQ(curCheckPos) > position.DistanceSQ(AttackPosition)) break;

			if (!Collision.CanHit(curCheckPos - position.SafeDirectionTo(AttackPosition) * iterationDistance, 0, 0, curCheckPos, 0, 0))
			{
				AttackPosition = curCheckPos;
				break;
			}
		}
		Projectile.NewProjectile(source,position + position.SafeDirectionTo(AttackPosition).RotatedByRandom(0.6f) * Main.rand.NextFloat(30,80), AttackPosition, type,damage,knockback,player.whoAmI, Utils.RandomNextSeed((ulong)Main.timeForVisualEffects));
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.ThunderStaff)
			.AddIngredient(ModContent.ItemType<LivingLightningBlock>(), 25)
			.AddIngredient(ModContent.ItemType<TornadoShard>(), 5)
			.AddIngredient(ItemID.LightShard,2)
			.AddIngredient(ItemID.DarkShard)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
