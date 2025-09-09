using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode
{
	public class EggCannon : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToRangedWeapon(50, 20, ModContent.ProjectileType<ExplosiveEgg>(), AmmoID.None, 35, 8f, 16f, 35, 35, true);
			//Item.useAmmo = ItemID.Grenade; // doing this makes it add the damage of grenades lol
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(silver: 54);
			Item.UseSound = SoundID.Item61;
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (ModContent.GetInstance<Common.AvalonClientConfig>().AdditionalScreenshakes)
			{
				UseStyles.gunStyle(player, 0.03f, 5f, 1.5f);
			}
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.04f), ModContent.ProjectileType<ExplosiveEgg>(), damage, knockback, player.whoAmI, ai2: Main.rand.NextBool(3) ? 1 : 0);
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-35, 0);
		}
	}
}
