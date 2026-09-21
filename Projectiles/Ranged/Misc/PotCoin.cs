using Avalon.Items.Weapons.Ranged.Thrown;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Projectiles.Ranged.Misc;

public class PotCoin : ModProjectile
{
	public override string Texture => $"Terraria/Images/Item_{ItemID.GoldCoin}";
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(12);
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
	}
	public override void AI()
	{
		Projectile.velocity.Y += 0.2f;
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

		Projectile.frameCounter++;
		Projectile.frame = (Projectile.frameCounter / 3) % 8;

		int dustType = DustID.CopperCoin + (int)Projectile.ai[0];
		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType);
		d.velocity *= 0.1f;
		d.noGravity = !Main.rand.NextBool(15);
	}
	public override void OnKill(int timeLeft)
	{
		int dustType = DustID.CopperCoin + (int)Projectile.ai[0];
		for (int i = 0; i < 5; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType);
			d.noGravity = true;
			d.scale += Main.rand.NextFloat();
			d.fadeIn = Main.rand.NextFloat(1.5f);
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Coin[(int)Projectile.ai[0]];
		Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, tex.Frame(1, 8, 0, Projectile.frame),lightColor,Projectile.rotation,tex.Size() / new Vector2(2,16),Projectile.scale,SpriteEffects.None);

		return false;
	}
}
