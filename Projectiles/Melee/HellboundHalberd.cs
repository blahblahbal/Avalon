using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;

namespace Avalon.Projectiles.Melee;

public class HellboundHalberd : SpearTemplate
{
    protected override float HoldoutRangeMax => 200;
    protected override float HoldoutRangeMin => 40;
    public override void SetDefaults()
    {
        base.SetDefaults();
    }
    public override bool PreDraw(ref Color lightColor)
    {
        //DrawPointyStabbyLight(5, new Color(128, 0, 255, 0) * 0.2f, new Vector2(1, 2), Vector2.Zero);
        //DrawPointyStabbyLight(3, new Color(255, 255, 255, 0) * 0.1f, new Vector2(0.7f, 1.7f), Vector2.Zero);

        //DrawPointyStabbyLight(3, new Color(255, 0, 0, 0) * 0.1f, new Vector2(1, 2) * 0.8f, new Vector2(10, -16));
        //DrawPointyStabbyLight(1, new Color(255, 255, 255, 0) * 0.05f, new Vector2(0.7f, 1.7f) * 0.8f, new Vector2(10, -16));

        //DrawPointyStabbyLight(3, new Color(255, 0, 0, 0) * 0.1f, new Vector2(1, 2) * 0.8f, new Vector2(-10, -16));
        //DrawPointyStabbyLight(1, new Color(255, 255, 255, 0) * 0.05f, new Vector2(0.7f, 1.7f) * 0.8f, new Vector2(-10, -16));
        //DrawProj_Spear(Projectile, new Color(255,0,0,0), SpriteEffects.None, Vector2.Zero);
        return base.PreDraw(ref lightColor);
    }

    public override void PostAI()
    {
        int S = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
        Main.dust[S].noGravity = true;
        Main.dust[S].velocity = Projectile.velocity * 2;
        Main.dust[S].fadeIn = Main.rand.NextFloat(0, 1.5f);
        int H = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
        Main.dust[H].noGravity = true;
        Main.dust[H].velocity = Projectile.velocity * -3;
        Main.dust[H].fadeIn = Main.rand.NextFloat(0, 1.5f);
    }
}
