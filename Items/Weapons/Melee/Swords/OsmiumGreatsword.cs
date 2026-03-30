using Avalon.Common.Extensions;
using Avalon.Common.Interfaces;
using Avalon.Items.Material.Bars;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class OsmiumGreatsword : ModItem, IItemWithReleaseButtonMidSwingEffect
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(48, 5f, 15, crit: 6, scale: 1.2f, useTurn: false);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
		Item.shootSpeed = 15;
		Item.shoot = ModContent.ProjectileType<OsmiumGreatswordThrown>();
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<OsmiumBar>(), 14).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 3).AddTile(TileID.Anvils).Register();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return false;
	}

	public void ReleaseButtonMidSwing(Player player)
	{
		if (player == Main.LocalPlayer)
		{
			int damage = (int)player.GetTotalDamage(Item.DamageType).ApplyTo(Item.damage * 0.25f);
			// static velocity
			//Vector2 velocity = player.Center.DirectionTo(Main.MouseWorld) * Item.shootSpeed;
			//distance based velocity
			Vector2 velocity = new Vector2(
				(Main.MouseWorld.X - player.itemLocation.X) / OsmiumGreatswordThrown.TimeForMaxDamage,
				(Main.MouseWorld.Y - player.itemLocation.Y - (OsmiumGreatswordThrown.Gravity / 2 * OsmiumGreatswordThrown.TimeForMaxDamage * OsmiumGreatswordThrown.TimeForMaxDamage)) / OsmiumGreatswordThrown.TimeForMaxDamage);
			velocity = velocity.LengthClamp(Item.shootSpeed);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.itemLocation, velocity, Item.shoot, damage, Item.knockBack, player.whoAmI, player.GetAdjustedItemScale(player.HeldItem));
		}
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (!CanUseItem(player))
			player.itemLocation = Vector2.Zero;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1 && !player.GetModPlayer<ItemWithReleaseButtonMidSwingEffectPlayer>().HasReleasedButton;
	}
}
