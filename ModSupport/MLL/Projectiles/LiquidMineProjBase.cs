using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace Avalon.ModSupport.MLL.Projectiles;
public abstract class LiquidMineProjBase : LiquidSpawningExplosionBase
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.IsAMineThatDealsTripleDamageWhenStationary[Type] = true;
		ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;

		ProjectileID.Sets.Explosive[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(801);
		AIType = 801;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
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
		return false;
	}
}
