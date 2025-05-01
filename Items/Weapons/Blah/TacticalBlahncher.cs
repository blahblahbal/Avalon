using Avalon.Rarities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah;

public class TacticalBlahncher : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToLauncher(160, 5f, 11f, 9, 9, crit: 7);
		Item.rare = ModContent.RarityType<BlahRarity>();
		Item.value = Item.sellPrice(1);
		Item.UseSound = SoundID.Item11;

	}

	//public override void AddRecipes() => CreateRecipe()
	//    .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 45)
	//    .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 40)
	//    .AddIngredient(ModContent.ItemType<SoulofTorture>(), 45).AddIngredient(ModContent.ItemType<TacticalExpulsor>())
	//    .AddIngredient(ItemID.RocketLauncher).AddIngredient(ItemID.GrenadeLauncher).AddIngredient(ItemID.Stynger)
	//    .AddTile(ModContent.TileType<SolariumAnvil>()).Register();

	public override Vector2? HoldoutOffset() => new Vector2(-10f, 0f);

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
							   int type, int damage, float knockback)
	{
		for (int i = 0; i < 3; i++)
		{
			float num78 = velocity.X + (Main.rand.Next(-50, 51) * 0.05f);
			float num79 = velocity.Y + (Main.rand.Next(-50, 51) * 0.05f);
			if (Main.rand.NextBool(3))
			{
				num78 *= 1f + (Main.rand.Next(-40, 41) * 0.02f);
				num79 *= 1f + (Main.rand.Next(-40, 41) * 0.02f);
			}

			Projectile.NewProjectile(source, position.X, position.Y, num78, num79,
				ModContent.ProjectileType<Projectiles.Ranged.Blahcket>(), damage, knockback, player.whoAmI);
		}

		return false;
	}

	public override void HoldItem(Player player)
	{
		var vector = new Vector2(player.position.X + (player.width * 0.5f), player.position.Y + (player.height * 0.5f));
		float num70 = Main.mouseX + Main.screenPosition.X - vector.X;
		float num71 = Main.mouseY + Main.screenPosition.Y - vector.Y;
		if (player.gravDir == -1f)
		{
			num71 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
		}

		float num72 = (float)Math.Sqrt((num70 * num70) + (num71 * num71));
		float num73 = num72;
		num72 = player.inventory[player.selectedItem].shootSpeed / num72;
		if (player.inventory[player.selectedItem].type == Item.type)
		{
			num70 += Main.rand.Next(-50, 51) * 0.03f / num72;
			num71 += Main.rand.Next(-50, 51) * 0.03f / num72;
		}

		num70 *= num72;
		num71 *= num72;
		player.itemRotation = (float)Math.Atan2(num71 * player.direction, num70 * player.direction);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.Next(4) >= 3;
}
