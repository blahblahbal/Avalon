using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Misc;

public class HellboundHalberPlayer : ModPlayer
{
	public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
	{
		#region hellbound halberd weird shit
		if (Main.myPlayer == drawInfo.drawPlayer.whoAmI)
		{
			if (drawInfo.drawPlayer.ownedProjectileCounts[ModContent.ProjectileType<HellboundHalberdSpear>()] > 0 && Main.mouseRight && !Main.mouseLeft)
			{
				if (Math.Sign(Player.Center.X - Main.MouseWorld.X) < 0)
				{
					drawInfo.playerEffect = SpriteEffects.None;
				}
				if (Math.Sign(Player.Center.X - Main.MouseWorld.X) > 0)
				{
					drawInfo.playerEffect = SpriteEffects.FlipHorizontally;
				}
			}
		}
		#endregion
	}
}
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
	public const float ScaleMult = 1.35f;
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<HellboundHalberdProj>(), 213, 9f, ScaleMult, 26, crit: 0, width: 56, height: 62);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 15);
	}
	public override bool MeleePrefix()
	{
		return true;
	}

	public int swing;
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse != 2)
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
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<HellboundHalberdSpear>(), damage, knockback);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<HellboundHalberdProj>(), damage, knockback, Main.myPlayer, swing, Main.LocalPlayer.MountedCenter.AngleTo(Main.MouseWorld));
		}
		return false;
	}
}