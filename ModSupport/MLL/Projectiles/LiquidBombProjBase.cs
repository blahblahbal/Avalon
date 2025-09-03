using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Avalon.ModSupport.MLL.Projectiles;
public abstract class LiquidBombProjBase : LiquidSpawningExplosionBase
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;

		ProjectileID.Sets.Explosive[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(904);
		Projectile.aiStyle = 16;
		AIType = 906;
		Projectile.timeLeft = 180;
		DrawOriginOffsetY = -2;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = Projectile.width - 8;
		height = Projectile.height - 8;
		return true;
	}

	//This is to make the bomb emit our liquid's splash at its fuse like other liquid bombs
	public override bool PreAI()
	{
		if (Projectile.owner != Main.myPlayer || Projectile.timeLeft > 3)
		{
			if (Main.rand.NextBool(2))
			{
				int type = DustType();
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, type, 0f, 0f, 100);
				dust.scale = 1f + Main.rand.Next(5) * 0.1f;
				dust.noGravity = true;
				Vector2 center = Projectile.Center;
				Vector2 spinPoint = new Vector2(0f, -Projectile.height / 2 - 6);
				double rot = Projectile.rotation;
				dust.position = center + spinPoint.RotatedBy(rot, default) * 1.1f;
			}
		}
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		LiquidExplosiveKill(Projectile);
	}
}
