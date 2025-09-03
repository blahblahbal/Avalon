using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace Avalon.ModSupport.MLL.Projectiles;
public abstract class LiquidRocketProjBase : LiquidSpawningExplosionBase
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.IsARocketThatDealsDoubleDamageToPrimaryEnemy[Type] = true;
		ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;

		ProjectileID.Sets.Explosive[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(799);
		AIType = 799;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity *= 0f;
		Projectile.alpha = 255;
		Projectile.timeLeft = 3;
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		LiquidExplosiveKill(Projectile);
	}

	// more accurate drawing, for practically the same you can simply add "DrawOriginOffsetY = -7" in SetDefaults.
	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 position = Projectile.Center - Main.screenPosition;
		Texture2D texture = TextureAssets.Projectile[Type].Value;
		Color color = Projectile.GetAlpha(lightColor);
		Vector2 origin = texture.Size() / 2f;
		Main.EntitySpriteDraw(texture, position, null, color, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);
		return false;
	}
}
