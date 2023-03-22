using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class CrimsonDrain : ModBuff
{
    private const int FrameInterval = 50;
    private const int MaxDistance = 620;
    private int dmg;

    public override void Update(Player player, ref int buffIndex)
    {
        dmg = 1;
        int pposX = (int)player.position.X;
        int pposY = (int)player.position.Y;
        for (int k = 0; k < Main.npc.Length; k++)
        {
            NPC n = Main.npc[k];
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
            if (!n.townNPC && n.active && !n.dontTakeDamage && !n.friendly && n.type != NPCID.TargetDummy && n.life >= 1 && n.lifeMax > 5 &&
                n.position.X >= pposX - MaxDistance && n.position.X <= pposX + MaxDistance && n.position.Y >= pposY - MaxDistance &&
                n.position.Y <= pposY + MaxDistance) // && n.type != ModContent.NPCType<NPCs.Fly>() && n.type != ModContent.NPCType<NPCs.FlySmall>())
            {
                if (player.GetModPlayer<AvalonPlayer>().FrameCount % FrameInterval == 0)
                {
                    n.StrikeNPC(dmg + n.defense / 2, 0f, 1);
                }
            }
        }
    }
}
