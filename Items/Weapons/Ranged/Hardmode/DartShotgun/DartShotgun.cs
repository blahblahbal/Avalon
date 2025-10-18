using Avalon;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode.DartShotgun
{
	public class DartShotgun : ModItem
	{
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.DartPistol);
			Item.damage = 15;
			Item.useTime = 48;
			Item.useAnimation = 48;
			Item.shootSpeed = 11.5f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.125f, Main.rand.NextFloat(-2.7f, 0f), ItemID.PoisonDart, true);
				Projectile.NewProjectile(source, position, vel, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, -1);
		}
	}
}
