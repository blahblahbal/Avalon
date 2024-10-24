using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.Phantasm;

public class SoulGrabber : ModProjectile
{
    public int alpha = 255;
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = false;
        Projectile.alpha = 255;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.scale = 1.35f;
		Projectile.timeLeft = 60 * 5;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override void AI()
    {
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
                int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
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
    public float auraScale = 1.85f;
    public override bool PreDraw(ref Color lightColor)
    {
		Asset<Texture2D> texture = TextureAssets.Projectile[Type];
        auraScale = MathHelper.Clamp(auraScale, 0f, 18.5f);
        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Color color = new Color(alpha, alpha, alpha, alpha);
        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture.Value, drawPos + new Vector2(Projectile.velocity.X * (-i * 3), Projectile.velocity.Y * (-i * 3)), frame, (color * (1 - (i * 0.25f))) * 0.2f * Projectile.Opacity, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 9f), Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture.Value, drawPos, frame, color * Projectile.Opacity, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 9f), Projectile.scale, SpriteEffects.None, 0);
        auraScale -= 0.01f;
        Main.EntitySpriteDraw(texture.Value, drawPos, frame, color * 0.15f * Projectile.Opacity, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 10f), Projectile.scale * auraScale, SpriteEffects.None, 0);
        return false;
    }
}
