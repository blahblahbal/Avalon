using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class Ancient : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.AncientSandstorm>(), 46, 4f, 40, 10f, 25, 25, 1f, 2);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 25);
		Item.UseSound = SoundID.Item34;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(5, 0);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num175 = 14f;
		float num176 = (float)Main.mouseX + Main.screenPosition.X - (player.position.X + (float)player.width * 0.5f) + (float)Main.rand.Next(-10, 11);
		float num177 = (float)Main.mouseY + Main.screenPosition.Y - (player.position.Y + (float)player.height * 0.5f) + (float)Main.rand.Next(-10, 11);
		float num178 = (float)Math.Sqrt((double)(num176 * num176 + num177 * num177));
		num178 = num175 / num178;
		num176 *= num178;
		num177 *= num178;
		for (int num179 = 0; num179 < 2; num179++)
		{
			float num180 = velocity.X;
			float num181 = velocity.Y;
			num180 += (float)Main.rand.Next(-15, 16) * 0.05f;
			num181 += (float)Main.rand.Next(-15, 16) * 0.05f;
			Projectile.NewProjectile(source, position.X, position.Y, num176, num177, type, damage, knockback, player.whoAmI, 0f, 0f);
		}

		return false;
	}
	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.GetModPlayer<AvalonPlayer>().AncientLessCost)
			mult *= 0.5f;
	}
}
