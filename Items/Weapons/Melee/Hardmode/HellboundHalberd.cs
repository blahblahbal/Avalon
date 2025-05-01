using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class HellboundHalberd : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return true;
	}
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
	}
	public float scaleMult = 1.35f; // set this to same as in the projectile file
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<Projectiles.Melee.HellboundHalberd>(), 213, 9f, scaleMult, 26, crit: 0, width: 56, height: 62);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 15);
	}
	public override bool MeleePrefix()
	{
		return true;
	}

	public static int swing;
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse != 2)
		{
			Rectangle dims = this.GetDims();
			float posMult = 1 + (dims.Height * scaleMult - 36) / 36 * 0.1f;
			velocity = Vector2.Zero;
			int height = dims.Height;
			if (player.gravDir == -1)
			{
				height = -dims.Height;
			}
			if (swing == 1)
			{
				swing--;
				position = player.Center + new Vector2(0, height * Item.scale * posMult);
			}
			else
			{
				swing++;
				position = player.Center + new Vector2(0, -height * Item.scale * posMult);
			}
		}
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity,
				ModContent.ProjectileType<Projectiles.Melee.HellboundHalberdSpear>(), Item.damage, Item.knockBack);
		}
		else
		{
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity,
				ModContent.ProjectileType<Projectiles.Melee.HellboundHalberd>(), Item.damage, Item.knockBack);
		}
		return false;
	}
}
