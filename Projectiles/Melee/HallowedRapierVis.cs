using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Items.Weapons.Melee.Hardmode;
using System.Runtime.CompilerServices;
using Terraria.GameContent.Drawing;
using Terraria.Audio;
using Terraria.GameContent;
namespace Avalon.Projectiles.Melee;

public class HallowedRapierVis : ModProjectile
{
    public Color color = new Color(237, 232, 255, 0);
    public static readonly Color[] hallowPallete = new Color[]
        {
            new Color(237, 232, 255, 0),
            new Color(237, 232, 255, 0),
            new Color(138, 155, 230, 0),
            new Color(52, 59, 153, 0),

        };
    public override Color? GetAlpha(Color lightColor)
    {
        if (Projectile.ai[1] == 1)
        {
            return new Color(255, 255, 255, 90) ;

        }
        return new Color(255, 255, 255, 90) * Projectile.Opacity * 2 *((float)Math.Sin(Projectile.ai[0] * 0.4f) * 0.25f + 0.75f);
    }
    public override void SetDefaults()
    {
        Projectile.width = 40;
        Projectile.height = 40;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.ownerHitCheck = true;
        Projectile.damage = 0;
        switch (Main.rand.Next(0, 3))
        {
            case 0:
                color = new Color(237, 232, 255, 0);
                break;
            case 1:
                color = new Color(237, 232, 255, 0);
                break;
            case 2:
                color = new Color(138, 155, 230, 0);
                break;
            case 3:
                color = new Color(52, 59, 153, 0);
                break;
        }
    }
    private int soundDelay;
    public override bool PreDraw(ref Color lightColor)
    {
        
        Projectile.rotation = Projectile.velocity.ToRotation();
        Main.instance.LoadProjectile(ProjectileID.PiercingStarlight);
        Texture2D texture = TextureAssets.Projectile[ProjectileID.PiercingStarlight].Value;
        Rectangle frame = texture.Frame();
        Rectangle graht = new Rectangle((int)Projectile.Center.X - (int)Main.screenPosition.X, (int)Projectile.Center.Y - (int)Main.screenPosition.Y, texture.Width * (Main.rand.Next(5,14) * Projectile.ai[1] == 1? 12:2), 40);
        Main.spriteBatch.Draw(texture, graht, frame, color, Projectile.rotation, texture.Size() / 2f, SpriteEffects.None, 0f);
        return false;

    }
    public override void AI()
    {
        
        Projectile.Opacity -= 0.15f;
        
        Projectile.rotation = Projectile.velocity.ToRotation();
        if (Projectile.Opacity == 0)
        {
            Projectile.Kill();
        }
        
    }

    public override void CutTiles()
    {
        Vector2 end = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitX) * 220f * Projectile.scale;
        Utils.PlotTileLine(Projectile.Center, end, 80f * Projectile.scale, DelegateMethods.CutTiles);
    }
    
}
