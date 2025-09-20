using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode
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
				Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.125f) * Main.rand.NextFloat(0.8f, 1f), type, damage, knockback, player.whoAmI);
			}
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, -1);
		}
	}
}
