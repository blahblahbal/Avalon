using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class PeridotHook : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.GemHookEmerald);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Tools/PeridotHook_Chain");

        var position = Projectile.Center;
        var mountedCenter = Main.player[Projectile.owner].MountedCenter;
        var sourceRectangle = new Rectangle?();
        var origin = new Vector2(texture.Value.Width * 0.5f, texture.Value.Height * 0.5f);
        float num1 = texture.Value.Height;
        var vector2_4 = mountedCenter - position;
        var rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
        var flag = true;
        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
            flag = false;
        if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
            flag = false;
        while (flag)
        {
            if (vector2_4.Length() < num1 + 1.0)
            {
                flag = false;
            }
            else
            {
                var vector2_1 = vector2_4;
                vector2_1.Normalize();
                position += vector2_1 * num1;
                vector2_4 = mountedCenter - position;
                var color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                color2 = Projectile.GetAlpha(color2);
                Main.EntitySpriteDraw(texture.Value, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
            }
        }

        return true;
    }

    public override float GrappleRange()
    {
        return Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().HookBonus ? 405f * 1.5f : 405f;
    }
    public override void NumGrappleHooks(Player player, ref int numHooks)
    {
        numHooks = 1;
    }

}
