using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Common.Templates;

namespace Avalon.Projectiles.Melee;

public class Shurikerang : PiercingBoomerangTemplate
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Ranged;
		AIType = ProjectileID.EnchantedBoomerang;
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));

		ReturnSpeed = 15f;
		ReturnAccel = 0.8f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		int num34 = 10;
		int num35 = 10;
		Vector2 vector7 = new Vector2(Projectile.position.X + Projectile.width / 2 - num34 / 2, Projectile.position.Y + Projectile.height / 2 - num35 / 2);
		Projectile.velocity = Collision.TileCollision(vector7, Projectile.velocity, num34, num35, true, true, 1);
		Projectile.ai[0] = 1f;
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		return false;
	}
}
