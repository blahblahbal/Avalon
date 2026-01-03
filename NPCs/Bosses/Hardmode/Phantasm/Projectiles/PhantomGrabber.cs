using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode.Phantasm.Projectiles;

public class PhantomGrabber : SoulGrabber
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Data.Sets.ProjectileSets.DontReflect[Type] = true;
	}
	public override void AI()
    {
		Projectile.frameCounter++;
		Projectile.frame = (Projectile.frameCounter / 5) % 6;

		if (Projectile.timeLeft == 60 * 5)
		{
			SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
		}

        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.velocity *= 1.02f;


		if (Projectile.timeLeft > (60 * 5) - 255 / 10)
		{
			Projectile.alpha -= 10;
		}

		if (Projectile.timeLeft < (255 / 5))
		{
			alpha += 5;
		}

		if (Projectile.ai[0] == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 3f;
                Main.dust[num893].scale = 2f;
                Main.dust[num893].noGravity = true;
            }
            for (int i = 0; i < 20; i++)
            {
                int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 1f;
                Main.dust[num893].scale = 2f;
                Main.dust[num893].noGravity = true;
            }
            Projectile.ai[0] = 1;
        }
        if(Projectile.velocity.Length() > 20f)
        {
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 20f;
        }
    }
}
