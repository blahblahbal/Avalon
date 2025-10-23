using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.HellboundHalberd;

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
public class HellboundHalberdProj : MaceTemplate
{
	public override string Texture => ModContent.GetInstance<HellboundHalberd>().Texture;
	public override float MaxRotation => MathF.PI + MathF.PI / 4f;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 104f;
	public override float ScaleMult => HellboundHalberd.ScaleMult;
	public override float EndScaleTime => 0.2f;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2f);
	public override int TrailLength => 6;
	public override Func<(SpriteEffects, float, Vector2), (SpriteEffects, float, Vector2)> SpriteEffectsFunc => FlipSprite;
	public (SpriteEffects, float, Vector2) FlipSprite((SpriteEffects spriteEffects, float rotation, Vector2 offset) t)
	{
		if ((SwingDirection == 1 && Owner.gravDir != -1) || (Owner.gravDir == -1 && SwingDirection == -1))
		{
			if (Owner.direction == 1)
			{
				return t;
			}
			if (Owner.direction == -1)
			{
				return (SpriteEffects.FlipHorizontally, MathHelper.PiOver2, new Vector2(-t.offset.X, t.offset.Y));
			}
		}
		else if ((SwingDirection == -1 && Owner.gravDir != -1) || (Owner.gravDir == -1 && SwingDirection == 1))
		{
			if (Owner.direction == 1)
			{
				return (SpriteEffects.FlipHorizontally, MathHelper.PiOver2, new Vector2(-t.offset.X, t.offset.Y));
			}
			if (Owner.direction == -1)
			{
				return t;
			}
		}
		return t;
	}
	public override void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
		Vector2 offsetFromHand = Projectile.Center - handPosition;
		float dirMod = SwingDirection * Owner.gravDir;
		float progressMult = 2f - (rotationProgress * 2f);

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
		d.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
		d.noGravity = true;
		d.fadeIn = Main.rand.NextFloat(0, 1.5f);

		Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
		d2.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
		d2.noGravity = true;
		d2.fadeIn = Main.rand.NextFloat(0, 1.5f);

		if (Main.rand.NextBool(4))
		{
			Dust dSmall = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
			dSmall.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
			dSmall.fadeIn = Main.rand.NextFloat(0, 0.7f);
			dSmall.scale *= 0.5f;
			Dust d2Small = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
			d2Small.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
			d2Small.fadeIn = Main.rand.NextFloat(0, 0.7f);
			d2Small.scale *= 0.5f;
		}
	}
}
public class HellboundHalberdSpear : SpearTemplate
{
	protected override float HoldoutRangeMax => 230;
	protected override float HoldoutRangeMin => 40;
	public override bool PreDraw(ref Color lightColor)
	{
		return base.PreDraw(ref lightColor);
	}
	public override void AI()
	{
		if (Projectile.Owner().direction == -1)
		{
			Projectile.Owner().SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * Projectile.Owner().gravDir + MathHelper.Pi + MathHelper.PiOver2);
		}
		else
		{
			Projectile.Owner().SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * Projectile.Owner().gravDir + (Projectile.Owner().gravDir == -1 ? MathHelper.Pi : 0));
		}
		base.AI();
	}
	public override void PostAI()
	{
		Projectile.scale = 1.35f;
		if (!Main.rand.NextBool(4))
		{
			int S = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(10, 10), Projectile.width, Projectile.height, DustID.Torch);
			Main.dust[S].noGravity = true;
			Main.dust[S].velocity = Projectile.velocity * 2;
			Main.dust[S].fadeIn = Main.rand.NextFloat(0, 1.5f);
			int H = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(10, 10), Projectile.width, Projectile.height, DustID.SolarFlare);
			Main.dust[H].noGravity = true;
			Main.dust[H].velocity = Projectile.velocity * -3;
			Main.dust[H].fadeIn = Main.rand.NextFloat(0, 1.5f);
		}
		if (Main.rand.NextBool(3))
		{
			int SSmall = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(25, 25), Projectile.width, Projectile.height, DustID.Torch);
			Main.dust[SSmall].noGravity = true;
			Main.dust[SSmall].velocity = Projectile.oldVelocity * 2;
			Main.dust[SSmall].fadeIn = Main.rand.NextFloat(0, 0.7f);
			Main.dust[SSmall].scale = 0.7f;
			int HSmall = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(25, 25), Projectile.width, Projectile.height, DustID.SolarFlare);
			Main.dust[HSmall].noGravity = true;
			Main.dust[HSmall].velocity = Projectile.velocity * -3;
			Main.dust[HSmall].fadeIn = Main.rand.NextFloat(0, 0.7f);
			Main.dust[HSmall].scale = 0.7f;
		}
	}
}
