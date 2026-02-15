using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Tomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Tomes;

public class TomeoftheDistantPast : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Bone1>(), 30, 4f, 6, 8f, 15, 15);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
		Item.UseSound = SoundID.Item1;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		switch (Main.rand.Next(4))
		{
			case 0:
				type = ModContent.ProjectileType<Bone1>();
				break;
			case 1:
				type = ModContent.ProjectileType<Bone2>();
				break;
			case 2:
				type = ModContent.ProjectileType<Bone3>();
				damage = (damage / 2) * 3;
				knockback *= 2;
				break;
			case 3:
				type = ModContent.ProjectileType<Bone4>();
				break;
		}
		Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.2f, random: true);
		Projectile.NewProjectile(source, position, vel, type, damage, knockback, player.whoAmI);
		return false;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(6, 2);
	}
}