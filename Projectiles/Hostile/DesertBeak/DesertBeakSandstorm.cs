using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Avalon.Data.Sets;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;
using ReLogic.Content;

namespace Avalon.Projectiles.Hostile.DesertBeak;

public class DesertBeakSandstorm : ModProjectile
{
	private static Asset<Texture2D> texture;
	public override void SetStaticDefaults()
	{
		texture = TextureAssets.Projectile[Type] /*TextureAssets.Projectile[ProjectileID.DD2ApprenticeStorm].Value*/;
	}
	public override void SetDefaults()
    {
        Projectile.width = 60;
        Projectile.height = 300;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = false;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.timeLeft = 540;
        Projectile.penetrate = -1;
        Projectile.ignoreWater = true;
        Projectile.alpha = 255;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override void AI()
    {
        Projectile.rotation += 0.1f;
        Projectile.ai[0]++;
        if (Projectile.alpha > -64)
            Projectile.alpha-= 8;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Rectangle frame = new Rectangle(0, 0, texture.Value.Width, texture.Value.Height);
        for (int j = 0; j < 2; j++)
        {
            float opacity = 0;
            float scale = 0.1f;
            float heightDivision = 4;
            for (int i = 0; i < (int)(Math.Floor(Projectile.height / heightDivision)); i++)
            {
                Vector2 drawPos = Projectile.Bottom - Main.screenPosition + new Vector2(j * 4).RotatedBy(i * MathHelper.PiOver2 + Main.timeForVisualEffects * 0.1f);
                opacity = MathHelper.Clamp(opacity + 0.01f, 0, 1);
                scale = MathHelper.Clamp(scale + 0.03f, 0, 1.1f);
                Color color = j == 0 ? Color.Goldenrod * opacity : new Color(255, 255, 255, 128) * opacity * 0.8f;
                Main.EntitySpriteDraw(texture.Value, drawPos + new Vector2(0, i * -heightDivision), frame, color * Projectile.Opacity, Projectile.rotation + MathHelper.PiOver4 * i * -0.1f * Projectile.direction, new Vector2(texture.Value.Width, texture.Value.Height) / 2, scale - (j * 0.1f), SpriteEffects.None, 0);
            }
        }
        return false;
    }
}
