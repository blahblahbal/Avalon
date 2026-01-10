using Avalon.NPCs.Bosses.Hardmode.Phantasm;
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
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 6;
		Data.Sets.ProjectileSets.DontReflect[Type] = true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		NPCs.Bosses.Hardmode.Phantasm.Phantasm.ApplyShadowCurse(target);
	}
	public override void SetDefaults()
    {
		ProjectileID.Sets.TrailCacheLength[Type] = 10;
		ProjectileID.Sets.TrailingMode[Type] = 2;
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
		Projectile.frameCounter++;
		Projectile.frame = (Projectile.frameCounter / 7) % 6;

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
		int frameHeight = texture.Value.Height / Main.projFrames[Projectile.type];
		Rectangle sourceRectangle = new Rectangle(0, frameHeight * Projectile.frame, texture.Width(), frameHeight);
		Vector2 frameOrigin = sourceRectangle.Size() / 2f;

		Vector2 drawPos = Projectile.Center;

		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 drawPosOld = Projectile.oldPos[i] + (Projectile.Size / 2);
			Main.EntitySpriteDraw(texture.Value, drawPosOld - Main.screenPosition, sourceRectangle, new Color(255, 125, 255, 225) * (1 - (i / 8f)) * 0.2f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.3f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.1f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * 0.15f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.2f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos - Main.screenPosition, sourceRectangle, new Color(255, 255, 255, 225) * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
		return false;
	}
}
