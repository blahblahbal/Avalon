using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Longbows;
public class RhodiumLongbowHeld : LongbowTemplate
{
	public override void SetDefaults()
	{
		base.SetDefaults();
		DrawOffsetX = -16;
		DrawOriginOffsetY = -25;
	}
	public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float Power)
	{
		base.Shoot(source, position, velocity, type, damage, knockback, Power);
		int t = ModContent.ProjectileType<RhodiumLongbowEnergyArrow>();
		for(int i = 0; i < Math.Floor(Power * 7); i++)
		{
			Vector2 pos = position + Main.rand.NextVector2CircularEdge(4 + (i * 4), 4 + (i * 2));
			Projectile.NewProjectile(source, pos, pos.DirectionTo(Main.MouseWorld) * velocity.Length(), t, damage / 5, knockback / 4, Projectile.owner, -20 + (i * -6));
		}
	}
	public override void PostDraw(Color lightColor)
	{
		if (Main.player[Projectile.owner].channel)
		{
			Main.EntitySpriteDraw(ArrowDrawData(Color.Lerp(new Color(1f, 0f, 0.2f, 0f), new Color(1f, 0.3f, 0.7f, 0f), Main.masterColor) * Power, Vector2.Zero, true));
		}
	}
}
