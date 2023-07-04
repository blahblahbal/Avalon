using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace Avalon.Projectiles.Ranged.Held
{
    public class RhodiumLongbowHeld : LongbowTemplate
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            //DrawOriginOffsetX = 20;
            DrawOffsetX = -16;
            DrawOriginOffsetY = -25;
        }
        public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float Power)
        {
            Projectile P = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Projectile.owner);
            if (Power == 1)
            {
                SoundEngine.PlaySound(SoundID.Item110);
                if(P.penetrate > 0)
                    P.penetrate+= 2;
                P.ai[0] = -100;
                P.usesLocalNPCImmunity= true;
                P.localNPCHitCooldown = 30;
                P.extraUpdates++;
                //P.scale *= 1.5f;
                //P.Resize((int)(P.width * P.scale * 1.5),(int)(P.height * P.scale * 1.5));
                P.netUpdate = true;
                for(int i = 0; i < 20; i++)
                {
                    Dust d = Dust.NewDustDirect(position, 0, 0, DustID.Snow);
                    d.noGravity = true;
                    d.velocity = velocity.RotatedByRandom(0.1f) * i * 0.1f;
                    d.alpha = 25;
                    d.color = new Color(200,200,200);
                }
            }
        }
    }
    public class OsmiumLongbowHeld : RhodiumLongbowHeld { }
    public class IridiumLongbowHeld : RhodiumLongbowHeld { }
}
