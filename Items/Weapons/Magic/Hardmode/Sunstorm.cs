using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class Sunstorm : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<Projectiles.Magic.Sunstorm>(), 50, 3f, 17, 12f, 60, 60, true, width: 30, height: 30);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 5);
		Item.UseSound = SoundID.Item8;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int j = 0; j < 12; j++)
		{
			float x = player.position.X + Main.rand.Next(-400, 400);
			float y = player.position.Y - 90;
			float num9 = player.position.X + player.width / 2 - x;
			float num10 = player.position.Y + player.height / 2 - y;
			num9 += Main.rand.Next(-100, 101);
			int num11 = 23;
			float num12 = (float)Math.Sqrt((double)(num9 * num9 + num10 * num10));
			num12 = num11 / num12;
			num9 *= num12;
			num10 *= num12;
			int num13 = Projectile.NewProjectile(source, x, y, num9, 0, type, damage, knockback, player.whoAmI, 0f, 0f);
			Main.projectile[num13].ai[1] = Main.rand.Next(2);
		}
		return false;
	}
}
