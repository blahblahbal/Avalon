using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Superhardmode;

public class DroneSwarm : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Type] = true;
	}
	public override Vector2? HoldoutOrigin() => new Vector2(10f, 10f);
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<Projectiles.Magic.Drone>(), 25, 4f, 5, 12f, 4, 12, true, width: 36, height: 36);
		Item.scale = 1.2f;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(gold: 5);
		Item.UseSound = SoundID.Item8;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 255);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 pos = player.Center + new Vector2(30, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
		for (int i = 0; i < 3; i++)
		{
			Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.167f, Main.rand.NextFloat(-2f, 2f), random: true);
			Projectile.NewProjectile(source, pos, vel, type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}
}
