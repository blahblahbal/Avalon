using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class ZirconHook : ModProjectile
{
	private static Asset<Texture2D> chainTexture;
	public override void SetStaticDefaults()
    {
        ProjectileID.Sets.SingleGrappleHook[Type] = true;
        chainTexture = ModContent.Request<Texture2D>("Avalon/Projectiles/Tools/ZirconHook_Chain");
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.GemHookDiamond);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        var position = Projectile.Center;
        var mountedCenter = Main.player[Projectile.owner].MountedCenter;
        var sourceRectangle = new Rectangle?();
        var origin = new Vector2(chainTexture.Value.Width * 0.5f, chainTexture.Value.Height + 1);
        float num1 = chainTexture.Value.Height;
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
                Main.EntitySpriteDraw(chainTexture.Value, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
            }
        }

        return true;
    }
    public override bool PreDrawExtras()
    {
        return false;
    }

    public override float GrappleRange()
    {
        return Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().HookBonus ? 480f * 1.5f : 480f;
    }
    public override void GrappleRetreatSpeed(Player player, ref float speed)
    {
        speed = 15f;
    }
    public override void NumGrappleHooks(Player player, ref int numHooks)
    {
        numHooks = 1;
    }
}
