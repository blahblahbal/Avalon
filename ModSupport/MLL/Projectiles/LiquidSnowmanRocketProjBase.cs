using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace Avalon.ModSupport.MLL.Projectiles;
public abstract class LiquidSnowmanRocketProjBase : LiquidSpawningExplosionBase
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[Type] = true;

		ProjectileID.Sets.Explosive[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(810);
		AIType = 810;
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

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 position = Projectile.Center - Main.screenPosition;
		Texture2D texture = TextureAssets.Projectile[Type].Value;
		Color color = Projectile.GetAlpha(lightColor);
		Vector2 origin = texture.Size() / 2f;
		Main.EntitySpriteDraw(texture, position, null, color, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);
		DrawOriginOffsetY = -12;
		return true;
	}
}
