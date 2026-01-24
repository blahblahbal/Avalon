using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Ranged.Misc;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Misc
{
	public class EggCannon : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToRangedWeapon(50, 20, ModContent.ProjectileType<ExplosiveEgg>(), AmmoID.None, 35, 8f, 16f, 35, 35, true);
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(silver: 54);
			Item.UseSound = SoundID.Item61;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.04f, random: true);
			Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<ExplosiveEgg>(), damage, knockback, player.whoAmI, ai2: Main.rand.NextBool(3) ? 1 : 0);
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-35, 0);
		}
	}
}