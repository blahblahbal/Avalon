using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Guns
{
	public class Blunderblight : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToGun(9, 0f, 5f, 50, 50, width: 44);
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1, 50);
			//Item.UseSound = SoundID.Item36;
			Item.UseSound = Sounds.Item.BlunderblightShot.Asset with { pitchVariance = 0.2f, pitch = 0.1f, volume = 0.8f, MaxInstances = 5 };
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int amount = Main.rand.Next(3, 5);
			for (int i = 0; i < amount; i++)
			{
				Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.168f, Main.rand.NextFloat(-2.7f, 0f), ItemID.MusketBall, true);
				Projectile.NewProjectile(source, position, vel, type, damage, knockback, player.whoAmI);
			}
			for(int i = 0; i < 5; i++) // this isn't synced in mp but like also that makes it vanilla because bubble wands don't show up either (why????)
			{
				Dust d = Dust.NewDustPerfect(position + Vector2.Normalize(velocity) * 40, ModContent.DustType<ContagionWeapons>(), velocity.RotatedByRandom(0.3f) * Main.rand.NextFloat(0.2f,0.7f));
				d.noGravity = Main.rand.NextBool();
				d.alpha = 128;
				if(d.noGravity)
				{
					d.scale *= 1.2f;
					d.velocity *= 2;
				}
				else
				{
					d.velocity.Y -= 2;
				}
			}
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -1);
		}
	}
}
