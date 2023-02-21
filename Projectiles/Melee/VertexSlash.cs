using ExxoAvalonOrigins.Common.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Projectiles.Melee
{
    public class VertexSlash : EnergySlashTemplate
    {
        public override bool PreDraw(ref Color lightColor)
        {
            DrawSlash(Color.Wheat, Color.Yellow, Color.Gold, Color.Wheat, 0, 1f, 0f, -MathHelper.Pi / 16, -MathHelper.Pi / 16, true);
            DrawSlash(Color.Pink,Color.Red,Color.Purple,Color.Wheat,0,0.8f,0f,-MathHelper.Pi / 16, -MathHelper.Pi / 16, false);
            DrawSlash(new Color(120,109,204),new Color(61,58,126),new Color(27,25,57), Color.Transparent, 0, 0.6f, 0f, -MathHelper.Pi / 16, -MathHelper.Pi / 16, false);
            return false;
        }
        public override void AI()
        {
            base.AI();
            float num = Projectile.localAI[0] / Projectile.ai[1];
            float num2 = Projectile.ai[0];
            if (Math.Abs(num2) < 0.2f)
            {
                
                Projectile.rotation += (float)Math.PI * 4f * num2 * 10f * num;
                float num7 = Utils.Remap(Projectile.localAI[0], 10f, Projectile.ai[1] - 5f, 0f, 1f);
                Projectile.position += Projectile.velocity.SafeNormalize(Vector2.Zero) * (80f * num7);
                Projectile.scale += num7 * 0.4f;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            int debuffCount = 0;
            for (int i = 0; i < target.buffType.Length; i++)
            {
                if (Main.debuff[target.buffType[i]])
                {
                    debuffCount++;
                }
            }
            if (debuffCount > 0)
            {
                if (target.boss)
                {
                    damage = (int)(damage * 1.2 * debuffCount);
                }
                else
                {
                    damage = (int)(damage * 1.45 * debuffCount);
                }
            }
        }
    }
}
