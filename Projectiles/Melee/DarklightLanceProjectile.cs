using ExxoAvalonOrigins.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Projectiles.Melee
{
    public class DarklightLanceProjectile : SpearTemplate
    {
        protected override float HoldoutRangeMax => 200;
        protected override float HoldoutRangeMin => 40;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.AI_019_Spears_GetExtensionHitbox(out var extensionbox);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //DrawPointyStabbyLight(5,new Color(128,0,255,0) * 0.2f,new Vector2(1,2),Vector2.Zero);
            //DrawPointyStabbyLight(3, new Color(255, 255, 255, 0) * 0.1f, new Vector2(0.7f, 1.7f), Vector2.Zero);

            //DrawPointyStabbyLight(3, new Color(255, 0, 0, 0) * 0.1f, new Vector2(1, 2) * 0.8f, new Vector2(10,-16));
            //DrawPointyStabbyLight(1, new Color(255, 255, 255, 0) * 0.05f, new Vector2(0.7f, 1.7f) * 0.8f, new Vector2(10, -16));

            //DrawPointyStabbyLight(3, new Color(255, 0, 0, 0) * 0.1f, new Vector2(1, 2) * 0.8f, new Vector2(-10, -16));
            //DrawPointyStabbyLight(1, new Color(255, 255, 255, 0) * 0.05f, new Vector2(0.7f, 1.7f) * 0.8f, new Vector2(-10, -16));
            DrawProj_Spear(Projectile, new Color(255,0,0,0), SpriteEffects.None, Vector2.Zero);
            return base.PreDraw(ref lightColor);
        }
    }
}
