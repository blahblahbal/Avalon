using Avalon.Items.Weapons.Melee.Boomerangs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Boomerangs;

public class TetanusChakramProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<TetanusChakram>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<TetanusChakram>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.ThornChakram);
		Projectile.width = Projectile.height = 14;
		Rectangle dims = this.GetDims();
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitAnything();
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitAnything();
	}

	void OnHitAnything()
	{
		for (int i = 0; i < 10; i++)
		{
			int Type = Main.rand.NextBool(5) ? DustID.Iron : DustID.Blood;
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Type);
			if (d.type == DustID.Blood)
			{
				d.alpha = 128;
			}
			d.noGravity = !Main.rand.NextBool(3);
		}
	}
}
