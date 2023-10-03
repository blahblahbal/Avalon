using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles
{
    internal class PlayerCough : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] % 10 == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(Projectile.position + new Vector2(0, Projectile.height / 2), Projectile.width, Projectile.height / 3, DustID.CorruptGibs, 0, 0, 128, default, Main.rand.NextFloat(1, 1.5f));
                    Main.dust[d].noGravity = !Main.rand.NextBool(4);
                    if (Main.dust[d].noGravity)
                        Main.dust[d].fadeIn = 1.5f;
                }
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(Projectile.position + new Vector2(0, Projectile.height / 2), Projectile.width, Projectile.height / 3, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, Main.rand.NextFloat(1, 1.5f));
                    Main.dust[d].noLightEmittence = true;
                    Main.dust[d].noGravity = !Main.rand.NextBool(4);
                    if (Main.dust[d].noGravity)
                        Main.dust[d].fadeIn = 1.5f;
                }
                Projectile.ai[0] = 0;
            }
            
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].getRect().Intersects(Projectile.getRect()))
                {
                    for (int b = 0; b < Main.player[Projectile.owner].buffType.Length; b++)
                    {
                        if (Main.debuff[Main.player[Projectile.owner].buffType[b]])
                        {
                            Main.npc[i].AddBuff(Main.player[Projectile.owner].buffType[b], Main.player[Projectile.owner].buffTime[b] > 3600 ? 3600 : Main.player[Projectile.owner].buffTime[b]);
                        }
                    }
                }
            }
        }
    }
}
