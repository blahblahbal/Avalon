using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode
{
	public class Blunderblight : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToGun(9, 0f, 5f, 50, 50, width: 44);
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1, 50);
			Item.UseSound = SoundID.Item36;
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (ModContent.GetInstance<Common.AvalonClientConfig>().AdditionalScreenshakes)
			{
				UseStyles.gunStyle(player, 0.05f, 5f, 3f);
			}
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int amount = Main.rand.Next(3, 5);
			for (int i = 0; i < amount; i++)
			{
				Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.168f, Main.rand.NextFloat(-2.7f, 0f), ItemID.MusketBall, true);
				Projectile.NewProjectile(source, position, vel, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -1);
		}
	}
}
