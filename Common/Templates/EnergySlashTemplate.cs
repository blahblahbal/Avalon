using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Templates; 

public abstract class EnergySlashTemplate : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.NightsEdge);
        Projectile.penetrate = 3;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        return false;
    }
    public virtual void DrawSlash(Color backColor, Color middleColor, Color frontColor, Color white, int sparkleAlpha, float VisualScaleMultiplier, float sparkleRotation, float splay, float rotationOffset, bool sparkle, bool fullBright = false)
    {
        splay *= Main.player[Projectile.owner].direction;
        rotationOffset *= Main.player[Projectile.owner].direction;
        Projectile proj = Projectile;
        float modifiedProjScale = proj.scale * VisualScaleMultiplier;
        Vector2 vector = proj.Center - Main.screenPosition;
        Asset<Texture2D> val = TextureAssets.Projectile[proj.type];
        Rectangle rectangle = val.Frame(1, 4);
        Vector2 origin = rectangle.Size() / 2f;
        float num = modifiedProjScale * 1.1f;
        SpriteEffects effects = ((!(proj.ai[0] >= 0f)) ? SpriteEffects.FlipVertically : SpriteEffects.None);
        float num2 = proj.localAI[0] / proj.ai[1];
        float num3 = Utils.Remap(num2, 0f, 0.6f, 0f, 1f) * Utils.Remap(num2, 0.6f, 1f, 1f, 0f);
        float num4 = 0.975f;
        float fromValue;
        if (!fullBright)
            fromValue = Lighting.GetColor(proj.Center.ToTileCoordinates()).ToVector3().Length() / (float)Math.Sqrt(3.0);
        else
            fromValue = 1;
        fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);
        Color color = backColor;
        Color color2 = middleColor;
        Color color3 = frontColor;
        Color color4 = white * num3 * 0.5f;
        color4.A = (byte)((float)(int)color4.A * (1f - fromValue));
        Color color5 = color4 * fromValue * 0.5f;
        color5.G = (byte)((float)(int)color5.G * fromValue);
        color5.B = (byte)((float)(int)color5.R * (0.25f + fromValue * 0.75f));

        //Main.spriteBatch.Draw(val.Value, vector, rectangle, color * fromValue * num3, proj.rotation + proj.ai[0] * ((float)Math.PI / 4f) * -1f * (1f - num2), origin, num, effects, 0f);
        //Main.spriteBatch.Draw(val.Value, vector, rectangle, color5 * 0.15f, proj.rotation + proj.ai[0] * 0.01f, origin, num, effects, 0f);
        //Main.spriteBatch.Draw(val.Value, vector, rectangle, color3 * fromValue * num3 * 0.3f, proj.rotation, origin, num, effects, 0f);
        //Main.spriteBatch.Draw(val.Value, vector, rectangle, color2 * fromValue * num3 * 0.5f, proj.rotation, origin, num * num4, effects, 0f);

        Main.spriteBatch.Draw(val.Value, vector, rectangle, color * fromValue * num3, proj.rotation + rotationOffset, origin, num, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, rectangle, color5 * 0.3f, proj.rotation + splay + rotationOffset, origin, num, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, rectangle, color3 * fromValue * num3 * 0.3f, proj.rotation + (splay * 2) + rotationOffset, origin, num, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, rectangle, color2 * fromValue * num3 * 0.5f, proj.rotation + (splay * 4) + rotationOffset, origin, num * num4, effects, 0f);


        Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), white * 0.6f * num3, proj.rotation + rotationOffset + proj.ai[0] * 0.01f, origin, num, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), white * 0.5f * num3, proj.rotation + rotationOffset + proj.ai[0] * -0.05f, origin, num * 0.8f, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), white * 0.4f * num3, proj.rotation + rotationOffset + proj.ai[0] * -0.1f, origin, num * 0.6f, effects, 0f);

        for (float num5 = 0f; num5 < 8f; num5 += 1f)
        {
            float num6 = proj.rotation + proj.ai[0] * num5 * ((float)Math.PI * -2f) * 0.025f + Utils.Remap(num2, 0f, 1f, 0f, (float)Math.PI / 4f) * proj.ai[0];
            Vector2 drawpos = vector + num6.ToRotationVector2() * ((float)val.Width() * 0.5f - 6f) * num;
            float num7 = num5 / 9f;
            if (sparkle)
            {
                DrawPrettyStarSparkle(proj.Opacity, SpriteEffects.None, drawpos, new Color(white.R, white.G, white.B, sparkleAlpha) * num3 * num7, white, num2, 0f, 0.5f, 0.5f, 1f, num6, new Vector2(0f, Utils.Remap(num2, 0f, 1f, 3f, 0f)) * num, Vector2.One * num);
            }
        }
        Vector2 drawpos2 = vector + (proj.rotation + Utils.Remap(num2, 0f, 1f, 0f, (float)Math.PI / 4f) * proj.ai[0]).ToRotationVector2() * ((float)val.Width() * 0.5f - 4f) * num;
        if (sparkle)
        {
            DrawPrettyStarSparkle(proj.Opacity, SpriteEffects.None, drawpos2, new Color(white.R, white.G, white.B, sparkleAlpha) * num3 * 0.5f, white, num2, 0f, 0.5f, 0.5f, 1f, sparkleRotation, new Vector2(Utils.Remap(num2, 0f, 1f, 4f, 1f)) * num, Vector2.One * num);
        }
    }
    public virtual void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
    {
        Texture2D value = TextureAssets.Extra[98].Value;
        Color color = shineColor * opacity * 0.5f;
        color.A = 0;
        Vector2 origin = value.Size() / 2f;
        Color color2 = drawColor * 0.5f;
        float num = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
        Vector2 vector = new Vector2(fatness.X * 0.5f, scale.X) * num;
        Vector2 vector2 = new Vector2(fatness.Y * 0.5f, scale.Y) * num;
        color *= num;
        color2 *= num;
        Main.EntitySpriteDraw(value, drawpos, null, color, (float)Math.PI / 2f + rotation, origin, vector, dir);
        Main.EntitySpriteDraw(value, drawpos, null, color, 0f + rotation, origin, vector2, dir);
        Main.EntitySpriteDraw(value, drawpos, null, color2, (float)Math.PI / 2f + rotation, origin, vector * 0.6f, dir);
        Main.EntitySpriteDraw(value, drawpos, null, color2, 0f + rotation, origin, vector2 * 0.6f, dir);
    }
}
