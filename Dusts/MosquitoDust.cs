using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class MosquitoDust : ModDust
{
    public override void OnSpawn(Dust dust)
    {
        dust.scale = Main.rand.NextFloat(0.3f, 0.6f);
        dust.noGravity = true;
        dust.frame = new Rectangle(0, 0, 8, 8);
    }
    public override bool Update(Dust dust)
    {
        if (Main.rand.NextBool(3))
        {
            dust.velocity = dust.velocity.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4));
        }
        dust.position += dust.velocity;
        dust.scale -= 0.006f;
        if (dust.scale <= 0)
        {
            dust.active = false;
        }
        return false;
    }
}
public class MosquitoDustImmortal : ModDust
{
    public override void OnSpawn(Dust dust)
    {
        dust.scale = Main.rand.NextFloat(0.3f, 0.6f);
        dust.noGravity = true;
        dust.frame = new Rectangle(0, 0, 8, 8);
    }
    public override bool Update(Dust dust)
    {
        if (Main.rand.NextBool(3))
        {
            dust.velocity = dust.velocity.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4));
        }
        dust.position += dust.velocity;
        dust.scale -= 0.0006f;
        if (dust.scale <= 0)
        {
            dust.active = false;
        }
        return false;
    }
}
