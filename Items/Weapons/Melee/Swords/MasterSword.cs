using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class MasterSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<MasterSwordBeam>(), 90, 5f, 9.5f, 54, 18, width: 54, height: 54);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 9, 63);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.statLife >= player.statLifeMax2 * 0.80f)
		{
			SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/MasterSword"), player.position);
			Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.3f), knockback, player.whoAmI);
			return false;
		}
		return false;
	}
}