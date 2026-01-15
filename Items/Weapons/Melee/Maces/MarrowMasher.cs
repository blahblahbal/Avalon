using Avalon.Common.Extensions;
using Avalon.Data.Sets;
using Avalon.Projectiles.Melee.Maces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces;

public class MarrowMasher : ModItem
{
	public const float ScaleMult = 1.25f;
	public override void SetStaticDefaults()
	{
		ItemSets.Maces[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<MarrowMasherProj>(), 58, 6.9f, ScaleMult, 30);
		Item.ArmorPenetration = 15;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 40);
	}
	public override bool MeleePrefix()
	{
		return true;
	}
	public int swing;
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = Vector2.Zero;
		if (swing == 1)
		{
			swing = -1;
		}
		else
		{
			swing = 1;
		}
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, swing, Main.LocalPlayer.MountedCenter.AngleTo(Main.MouseWorld));
		return false;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
