using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts
{
    public class SimpleColorableGlowyDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, Main.rand.Next(2) * 10, 10, 10);
            dust.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            Lighting.AddLight(dust.position, dust.color.ToVector3() * (1f - dust.alpha / 255f) * dust.scale);
            return true;
        }
    }
}
