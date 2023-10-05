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
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.damage = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 900;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            ClassExtensions.DrawGas(Texture, lightColor, Projectile, -3, 8);
            return false;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.ai[1]++;
            if (Projectile.ai[0] > 15)
            {
                Projectile.velocity.Y += 0.05f;
                Projectile.ai[0] = 0;
            }
            if (Projectile.ai[1] % 10 == 0)
            {
                Projectile.velocity.X *= 0.95f;
            }

            bool[] decreaseDebuffTime = new bool[Main.player[Projectile.owner].buffType.Length];
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (!Main.npc[i].townNPC)
                {
                    if (Main.npc[i].getRect().Intersects(Projectile.getRect()))
                    {
                        for (int b = 0; b < Main.player[Projectile.owner].buffType.Length; b++)
                        {
                            if (Main.debuff[Main.player[Projectile.owner].buffType[b]] && !BuffID.Sets.NurseCannotRemoveDebuff[Main.player[Projectile.owner].buffType[b]] && Main.player[Projectile.owner].buffType[b] != BuffID.Tipsy)
                            {
                                Main.npc[i].AddBuff(Main.player[Projectile.owner].buffType[b], Main.player[Projectile.owner].buffTime[b] > 3600 ? 3600 : Main.player[Projectile.owner].buffTime[b]);
                                decreaseDebuffTime[b] = true;
                            }
                        }
                    }
                }
            }
            if (Projectile.ai[2] == 0)
            {
                for (int j = 0; j < decreaseDebuffTime.Length; j++)
                {
                    if (decreaseDebuffTime[j])
                    {
                        Main.player[Projectile.owner].buffTime[j] = (int)(Main.player[Projectile.owner].buffTime[j] * 0.8f);
                        decreaseDebuffTime[j] = false;
                        continue;
                    }
                }
                Projectile.ai[2]++;
            }
        }
    }
}
