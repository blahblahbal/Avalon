using Avalon.Common.Extensions;
using Avalon.Data.Sets;
using Avalon.Projectiles.Melee.Maces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces;

public class HallowedClaymore : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return true;
	}
	public const float scaleMult = 1.35f;
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<HallowedClaymoreProj>(), 160, 12f, scaleMult, 42, width: 56, height: 62);
		Item.ArmorPenetration = 15;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
		Item.UseSound = null;
	}
	public override void SetStaticDefaults()
	{
		ItemSets.Maces[Type] = true;
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
