using Avalon.Dusts;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Avalon.Buffs.Debuffs;

namespace Avalon.Common.Players;
public class AvalonEnchantPlayer : ModPlayer
{
	public bool PathogenImbue;
	public bool FrostGauntlet;
	public override void ResetEffects()
	{
		PathogenImbue = false;
		FrostGauntlet = false;
	}
	public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (item.DamageType.CountsAsClass<MeleeDamageClass>())
		{
			if (FrostGauntlet)
			{
				target.AddBuff(BuffID.Frostburn2, 60 * 4);
			}
			if (PathogenImbue)
			{
				target.AddBuff(ModContent.BuffType<Pathogen>(), 60 * Main.rand.Next(3, 7));
			}
		}
	}
	public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if ((proj.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[proj.type]) && !proj.noEnchantments)
		{
			if (FrostGauntlet)
			{
				target.AddBuff(BuffID.Frostburn2, 60 * 4);
			}
			if (PathogenImbue)
			{
				target.AddBuff(ModContent.BuffType<Pathogen>(), 60 * Main.rand.Next(3, 7));
			}
		}
	}
	public override void MeleeEffects(Item item, Rectangle hitbox)
	{
		if (item.DamageType.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic)
		{
			if (PathogenImbue)
			{
				if (Main.rand.NextBool(3))
				{
					int d = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<PathogenDust>(), Player.velocity.X * 0.2f + (Player.direction * 3), Player.velocity.Y * 0.2f, 128);
					Main.dust[d].noGravity = true;
					Main.dust[d].fadeIn = 1.5f;
					Main.dust[d].velocity *= 0.25f;
				}
			}
			if (FrostGauntlet)
			{
				if (Main.rand.NextBool(3))
				{
					int d = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch, Player.velocity.X * 0.2f + (Player.direction * 3), Player.velocity.Y * 0.2f, 100);
					Main.dust[d].noGravity = true;
					Main.dust[d].fadeIn = 1.5f;
					Main.dust[d].velocity *= 0.25f;
					Main.dust[d].velocity *= 0.7f;
					Main.dust[d].velocity.Y -= 0.5f;
				}
			}
		}
	}
	public override void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
	{
		if ((projectile.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[projectile.type]) && !projectile.noEnchantments)
		{
			if (PathogenImbue)
			{
				if (Main.rand.NextBool(2))
				{
					int d = Dust.NewDust(boxPosition, boxWidth, boxHeight, ModContent.DustType<PathogenDust>(), projectile.velocity.X * 0.2f + (projectile.direction * 3), projectile.velocity.Y * 0.2f, 128, default, 1.5f);
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity *= 0.7f;
					Main.dust[d].velocity.Y -= 0.5f;
				}
			}
			if (FrostGauntlet)
			{
				if (Main.rand.NextBool(2))
				{
					int d = Dust.NewDust(boxPosition, boxWidth, boxHeight, DustID.IceTorch, projectile.velocity.X * 0.2f + (projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default, 2);
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity *= 0.7f;
					Main.dust[d].velocity.Y -= 0.5f;
				}
			}
		}
	}
}
