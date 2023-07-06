using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace Avalon.Projectiles.Ranged.Held
{
    public class LongboneHeld : LongbowTemplate
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            //DrawOriginOffsetX = 20;
            DrawOffsetX = -16;
            DrawOriginOffsetY = -25;
        }
        public override float HowFarShouldTheBowBeHeldFromPlayer => 28f;
        public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float Power)
        {
            for (int i = -1; i < 2; i++)
            {
                Projectile P = Projectile.NewProjectileDirect(source, position + new Vector2(0, i * 10).RotatedBy(Projectile.rotation), velocity * Main.rand.NextFloat(0.5f + (Power / 2f),1), type, damage, knockback, Projectile.owner);
                P.velocity = P.velocity.RotatedByRandom(1 - Power);
                P.netUpdate= true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            DefaultBowDraw(lightColor, Vector2.Zero);
            if (FullPowerGlow > 0 && Main.myPlayer == Projectile.owner)
            {
                DefaultBowDraw(NotificationColor * FullPowerGlow, Vector2.Zero);
            }
            if (Main.player[Projectile.owner].channel)
            {
                DrawArrow(lightColor, new Vector2(0, 10));
                DrawArrow(lightColor, new Vector2(0,-10));
                DrawArrow(lightColor, new Vector2(-Projectile.frame,0));
            }
            return false;
        }
    }
}
