using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvForceField : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        var value = new Rectangle((int)player.Center.X - 32, (int)player.Center.Y - 32, 64, 64);
        Projectile[] projectile = Main.projectile;
        for (int l = 0; l < projectile.Length; l++)
        {
            Projectile Pr = projectile[l];
            if (!Pr.friendly && !Pr.bobber && !Data.Sets.Projectile.DontReflect[Pr.type])
            {
                var rectangle = new Rectangle((int)Pr.position.X, (int)Pr.position.Y, Pr.width, Pr.height);
                if (rectangle.Intersects(value))
                {
                    for (int m = 0; m < 5; m++)
                    {
                        int num = Dust.NewDust(Pr.position, Pr.width, Pr.height, DustID.MagicMirror, 0f, 0f, 100);
                        Main.dust[num].noGravity = true;
                    }

                    Pr.hostile = false;
                    Pr.friendly = true;
                    Projectile expr_605_cp_0 = Pr;
                    expr_605_cp_0.velocity.X = expr_605_cp_0.velocity.X * -1f;
                    Projectile expr_61D_cp_0 = Pr;
                    expr_61D_cp_0.velocity.Y = expr_61D_cp_0.velocity.Y * -1f;
                }
            }
        }

        NPC[] npc = Main.npc;
        for (int l = 0; l < npc.Length; l++)
        {
            NPC nPC = npc[l];
            if (!nPC.friendly && nPC.aiStyle == 9)
            {
                var rectangle2 = new Rectangle((int)nPC.position.X, (int)nPC.position.Y, nPC.width, nPC.height);
                if (rectangle2.Intersects(value))
                {
                    for (int n = 0; n < 5; n++)
                    {
                        int num2 = Dust.NewDust(nPC.position, nPC.width, nPC.height, DustID.MagicMirror, 0f, 0f, 100);
                        Main.dust[num2].noGravity = true;
                    }

                    nPC.friendly = true;
                    NPC expr_721_cp_0 = nPC;
                    expr_721_cp_0.velocity.X = expr_721_cp_0.velocity.X * -1f;
                    NPC expr_739_cp_0 = nPC;
                    expr_739_cp_0.velocity.Y = expr_739_cp_0.velocity.Y * -1f;
                }
            }
        }
    }
}
