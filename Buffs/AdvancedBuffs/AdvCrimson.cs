using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvCrimson : ModBuff
{
    private const int FrameInterval = 50;
    private const int MaxDistance = 620;
    private int dmg;

    public override void Update(Player player, ref int buffIndex)
    {
        dmg = 0;
        int pposX = (int)player.Center.X;
        int pposY = (int)player.Center.Y;
        for (int k = 0; k < Main.npc.Length; k++)
        {
            NPC n = Main.npc[k];
            if (n.aiStyle == 6)
            {
                if (n.realLife != n.whoAmI && n.realLife >= 0) continue;
            }
            if (!n.townNPC && n.active && !n.dontTakeDamage && !n.friendly && n.type != NPCID.TargetDummy && n.life >= 1 && n.lifeMax > 5 &&
                n.position.X >= pposX - MaxDistance && n.position.X <= pposX + MaxDistance && n.position.Y >= pposY - MaxDistance &&
                n.position.Y <= pposY + MaxDistance) // && n.type != ModContent.NPCType<NPCs.Fly>() && n.type != ModContent.NPCType<NPCs.FlySmall>())
            {
                dmg++;
            }
        }
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC n = Main.npc[i];
            if (n.aiStyle == 6)
            {
                if (n.realLife != n.whoAmI && n.realLife >= 0) continue;
            }
            if (!n.townNPC && n.active && !n.dontTakeDamage && !n.friendly && n.type != NPCID.TargetDummy && n.life >= 1 && n.lifeMax > 5 &&
                n.position.X >= pposX - MaxDistance && n.position.X <= pposX + MaxDistance && n.position.Y >= pposY - MaxDistance &&
                n.position.Y <= pposY + MaxDistance) //&& n.type != ModContent.NPCType<NPCs.Fly>() && n.type != ModContent.NPCType<NPCs.FlySmall>())
            {
                if (player.GetModPlayer<AvalonPlayer>().FrameCount % FrameInterval == 0)
                {
                    n.StrikeNPC(new NPC.HitInfo { Damage = dmg * 2, Knockback = 0, HitDirection = 1});
                    player.GetModPlayer<AvalonPlayer>().FrameCount = 0;
                }
            }
        }
    }
}
