using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class BeeBolt : ModProjectile
{
    private Color color;
    private int dustId;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.SapphireBolt);
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 10 / 16;
        Projectile.height = dims.Height * 10 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.penetrate = 2;
        color = new Color(112, 224, 149) * 0.7f;
        dustId = DustID.Honey;
    }

    public override void AI()
    {
        for (var i = 0; i < 2; i++)
        {
            var dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustId, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
        }
        if (Projectile.ai[1] == 0f)
        {
            Projectile.ai[1] = 1f;
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
        }

        Lighting.AddLight(new Vector2((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f)), color.ToVector3());
    }
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Projectile.ai[0] < 3)
		{
			for (int num194 = 0; num194 < 2; num194++)
			{
				float num195 = Projectile.velocity.X;
				float num196 = Projectile.velocity.Y;
				num195 += Main.rand.Next(-40, 41) * 0.05f;
				num196 += Main.rand.Next(-40, 41) * 0.05f;
				Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, num195, num196, Type, Projectile.damage, Projectile.knockBack, Projectile.Owner().whoAmI, Projectile.ai[0]++);
			}
			if (Projectile.owner == Main.myPlayer)
			{
				int num756 = Main.rand.Next(5, 8);
				for (int num757 = 0; num757 < num756; num757++)
				{
					float speedX = (float)Main.rand.Next(-35, 36) * 0.02f;
					float speedY = (float)Main.rand.Next(-35, 36) * 0.02f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, speedX, speedY, Projectile.Owner().beeType(), Projectile.Owner().beeDamage(Projectile.damage), Projectile.Owner().beeKB(0f), Main.myPlayer);
				}
			}
		}
		Projectile.Kill();
	}
	public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        for (int num453 = 0; num453 < 15; num453++)
        {
            int num454 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustId, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 50, default, 1.2f);
            Main.dust[num454].noGravity = true;
            Dust dust152 = Main.dust[num454];
            Dust dust226 = dust152;
            dust226.scale *= 1.25f;
            dust152 = Main.dust[num454];
            dust226 = dust152;
            dust226.velocity *= 0.5f;
        }
    }
}
