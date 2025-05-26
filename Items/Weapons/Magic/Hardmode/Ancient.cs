using Avalon.Common.Extensions;
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
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.AncientSandstorm>(), 46, 4f, 40, 21f, 25, 25, 1f, 2);
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
		// possibly fucky way of 
		float angle = player.AngleTo(Main.MouseWorld);
		float radX = MathF.Cos(angle);
		float radY = MathF.Sin(angle);
		int radDirX = MathF.Sign(radX);
		int radDirY = MathF.Sign(radY);
		// X
		float velMultX = player.velocity.X * (radX * radDirX);
		// Y
		float velMultY = player.velocity.Y * (radY * radDirY);

		Vector2 velMult = new(velMultX, velMultY);

		int projCount = 5;
		for (int i = 1; i <= projCount; i++)
		{
			float distToMouseScaled = position.Distance(Main.MouseWorld);
			float zoomScale = Main.screenHeight / Main.GameViewMatrix.Zoom.Y;
			distToMouseScaled /= zoomScale / 2f;
			if (distToMouseScaled > 1f)
			{
				distToMouseScaled = 1f;
			}
			float radius = MathF.Pow(1f - distToMouseScaled, 4f);
			radius = Utils.Remap(radius, 0f, 0.7f, 0.005f, 0.4f);
			int projNumOffset = (int)(i - 0.5f - (projCount / 2f));
			Vector2 dir = Vector2.Normalize(velocity).RotatedBy(projNumOffset * radius).RotatedByRandom(0.05f);

			float distVelMod = Math.Clamp(distToMouseScaled, 0.3f, 1f);
			float projNumVelMod = (1f - (Math.Abs(projNumOffset) * 0.1f)) * Main.rand.NextFloat(0.5f, 1f);
			Vector2 vel = dir * velocity.Length() * distVelMod * projNumVelMod;

			vel += ((velMult * 0.7f) + (player.velocity * 0.1f)) * Math.Clamp(distToMouseScaled, 0.7f, 1f);

			Projectile.NewProjectile(source, position, vel, type, damage, knockback, player.whoAmI, (i + 1) % 2);
		}
		return false;
	}
	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.GetModPlayer<AvalonPlayer>().AncientLessCost)
		{
			mult *= 0.5f;
		}
	}
}
