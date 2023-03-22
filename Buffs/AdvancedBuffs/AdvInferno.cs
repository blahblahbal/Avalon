using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvInferno : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.inferno = true;
        Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
        int num = 24;
        float num2 = 200f;
        bool flag = player.infernoCounter % 60 == 0;
        int num3 = 10;
        if (player.whoAmI == Main.myPlayer)
        {
            for (int l = 0; l < 200; l++)
            {
                NPC nPC = Main.npc[l];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num] &&
                    Vector2.Distance(player.Center, nPC.Center) <= num2)
                {
                    if (nPC.HasBuff(num))
                    {
                        nPC.AddBuff(num, 120);
                    }

                    if (flag)
                    {
                        nPC.StrikeNPC(num3, 0f, 0);
                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            NetMessage.SendData(MessageID.DamageNPC, -1, -1, NetworkText.Empty, l, num3);
                        }
                    }
                }
            }

            if (player.hostile)
            {
                for (int m = 0; m < 255; m++)
                {
                    Player p = Main.player[m];
                    if (p != player && player.active && !player.dead && player.hostile && !player.buffImmune[num] &&
                        (p.team != player.team || player.team == 0) &&
                        Vector2.Distance(player.Center, p.Center) <= num2)
                    {
                        if (p.HasBuff(num))
                        {
                            p.AddBuff(num, 120);
                        }

                        if (flag)
                        {
                            p.Hurt(PlayerDeathReason.ByPlayer(player.whoAmI), num3, 0, true);
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.HurtPlayer, -1, -1,
                                    NetworkText.FromLiteral(PlayerDeathReason.ByPlayer(player.whoAmI).ToString()), m,
                                    0f, num3, 1f);
                            }
                        }
                    }
                }
            }
        }
    }
}
