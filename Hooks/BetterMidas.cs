using Avalon.Common;
using Avalon.Common.Players;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class BetterMidas : ModHook
{
    protected override void Apply()
    {
        On_NPC.NPCLoot_DropMoney += OnNPCDropMoney;
    }

    private static void OnNPCDropMoney(On_NPC.orig_NPCLoot_DropMoney orig, NPC npc, Player closestPlayer)
    {
        float num = 0f;
        float luck = closestPlayer.luck;
        int num2 = 1;
        if (Main.rand.NextFloat() < Math.Abs(luck))
        {
            num2 = 2;
        }
        for (int i = 0; i < num2; i++)
        {
            float finalMoney = npc.value;
            if (closestPlayer.GetModPlayer<AvalonPlayer>().GreedyPrefix)
            {
                int amtOfAcc = 0;
                for (int slot = 3; slot <= 9; slot++)
                {
                    if (closestPlayer.armor[slot].prefix == ModContent.PrefixType<Prefixes.Greedy>())
                    {
                        amtOfAcc++;
                    }
                }
                //add for shm accessory slot later
                finalMoney *= 1f + (amtOfAcc * 0.05f);
            }
            if (npc.midas)
            {
                int asdf = Main.rand.Next(8000);
                if (asdf <= 6000)
                {
                    finalMoney *= 2f; // 1f + (float)Main.rand.Next(10, 50) * 0.01f;
                }
                else if (asdf > 6000 && asdf <= 6750)
                {
                    finalMoney *= 2.5f;
                }
                else if (asdf > 6750 && asdf <= 7350)
                {
                    finalMoney *= 3f;
                }
                else if (asdf > 7350 && asdf <= 7675)
                {
                    finalMoney *= 3.5f;
                }
                else if (asdf > 7675 && asdf <= 7875)
                {
                    finalMoney *= 4f;
                }
                else if (asdf > 7875 && asdf <= 7950)
                {
                    finalMoney *= 5f;
                }
                else finalMoney *= 10f;
            }
            finalMoney *= 1f + (float)Main.rand.Next(-20, 76) * 0.01f;
            if (Main.rand.NextBool(2))
            {
                finalMoney *= 1f + (float)Main.rand.Next(5, 11) * 0.01f;
            }
            if (Main.rand.NextBool(4))
            {
                finalMoney *= 1f + (float)Main.rand.Next(10, 21) * 0.01f;
            }
            if (Main.rand.NextBool(8))
            {
                finalMoney *= 1f + (float)Main.rand.Next(15, 31) * 0.01f;
            }
            if (Main.rand.NextBool(16))
            {
                finalMoney *= 1f + (float)Main.rand.Next(20, 41) * 0.01f;
            }
            if (Main.rand.NextBool(32))
            {
                finalMoney *= 1f + (float)Main.rand.Next(25, 51) * 0.01f;
            }
            if (Main.rand.NextBool(64))
            {
                finalMoney *= 1f + (float)Main.rand.Next(50, 101) * 0.01f;
            }
            if (Main.bloodMoon)
            {
                finalMoney *= 1f + (float)Main.rand.Next(101) * 0.01f;
            }
            if (i == 0)
            {
                num = finalMoney;
            }
            else if (luck < 0f)
            {
                if (finalMoney < num)
                {
                    num = finalMoney;
                }
            }
            else if (finalMoney > num)
            {
                num = finalMoney;
            }
        }
        num += (float)npc.extraValue;
        while ((int)num > 0)
        {
            if (num > 1000000f)
            {
                int num4 = (int)(num / 1000000f);
                if (num4 > 50 && Main.rand.NextBool(5))
                {
                    num4 /= Main.rand.Next(3) + 1;
                }
                if (Main.rand.NextBool(5))
                {
                    num4 /= Main.rand.Next(3) + 1;
                }
                int num5 = num4;
                while (num5 > 999)
                {
                    num5 -= 999;
                    Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 74, 999);
                }
                num -= (float)(1000000 * num4);
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 74, num5);
            }
            else if (num > 10000f)
            {
                int num6 = (int)(num / 10000f);
                if (num6 > 50 && Main.rand.NextBool(5))
                {
                    num6 /= Main.rand.Next(3) + 1;
                }
                if (Main.rand.NextBool(5))
                {
                    num6 /= Main.rand.Next(3) + 1;
                }
                num -= (float)(10000 * num6);
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 73, num6);
            }
            else if (num > 100f)
            {
                int num7 = (int)(num / 100f);
                if (num7 > 50 && Main.rand.NextBool(5))
                {
                    num7 /= Main.rand.Next(3) + 1;
                }
                if (Main.rand.NextBool(5))
                {
                    num7 /= Main.rand.Next(3) + 1;
                }
                num -= (float)(100 * num7);
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 72, num7);
            }
            else
            {
                int num8 = (int)num;
                if (num8 > 50 && Main.rand.NextBool(5))
                {
                    num8 /= Main.rand.Next(3) + 1;
                }
                if (Main.rand.NextBool(5))
                {
                    num8 /= Main.rand.Next(4) + 1;
                }
                if (num8 < 1)
                {
                    num8 = 1;
                }
                num -= (float)num8;
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 71, num8);
            }
        }
    }
}
