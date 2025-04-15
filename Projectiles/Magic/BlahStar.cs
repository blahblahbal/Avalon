using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace Avalon.Projectiles.Magic;

public class BlahStar : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.aiStyle = -1;
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.tileCollide = false;
		//AIType = ProjectileID.Bullet;
        Projectile.penetrate = 5;
        Projectile.hostile = false;
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 249 / 255, 201 / 255, 77 / 255);
        return true;
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int i = 0; i < 2; i++)
        {
            float speedX = Projectile.velocity.X + Main.rand.Next(-51, 51) * 0.2f;
            float speedY = Projectile.velocity.Y + Main.rand.Next(-51, 51) * 0.2f;
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(speedX, speedY), ModContent.ProjectileType<BlahFire>(), Projectile.damage, Projectile.knockBack);
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].owner = Projectile.owner;
            Main.projectile[proj].timeLeft = 240;
        }
        Projectile.active = false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
		if (Projectile.ai[2] == 0)
		{
			Projectile.Kill();
			return true;
		}

		return false;
    }
    public override void AI()
    {
		if (Projectile.ai[2] == 0)
		{
			Projectile.tileCollide = true;
		}
		if (Projectile.soundDelay == 0)
		{
			Projectile.soundDelay = 20 + Main.rand.Next(40);
			SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
		}
		if (Projectile.localAI[0] == 0f)
			Projectile.localAI[0] = 1f;

		Projectile.alpha += (int)(25f * Projectile.localAI[0]);
		if (Projectile.alpha > 200)
		{
			Projectile.alpha = 200;
			Projectile.localAI[0] = -1f;
		}

		if (Projectile.alpha < 0)
		{
			Projectile.alpha = 0;
			Projectile.localAI[0] = 1f;
		}
		Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f * Projectile.direction;

		{
			Vector2 vector14 = new Vector2(Main.screenWidth, Main.screenHeight);
			if (Projectile.Hitbox.Intersects(Utils.CenteredRectangle(Main.screenPosition + vector14 / 2f, vector14 + new Vector2(400f))) && Main.rand.NextBool(20))
				Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity * 0.2f, Main.rand.Next(16, 18));

			if (Main.rand.NextBool(4))
			{
				Dust dust6 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 86, 0f, 0f, 127);
				Dust dust2 = dust6;
				dust2.velocity *= 0.7f;
				dust6.noGravity = true;
				dust2 = dust6;
				dust2.velocity += Projectile.velocity * 0.3f;
				if (Main.rand.NextBool(2))
				{
					dust2 = dust6;
					dust2.position -= Projectile.velocity * 4f;
				}
			}
		}
		if (Projectile.ai[1] == 1f)
		{
			Projectile.light = 0.9f;
			if (Main.rand.Next(10) == 0)
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default, 1.2f);

			if (Main.rand.Next(20) == 0)
				Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), Main.rand.Next(16, 18));
		}
		 

		Projectile.hostile = false;
        Projectile.friendly = true;
        if (Main.rand.NextBool(100))
        {
            for (int i = 0; i < 3; i++)
            {
                int d = Dust.NewDust(Projectile.position, 10, 10, DustID.Torch);
                Main.dust[d].noGravity = true;
            }
        }
    }
}
