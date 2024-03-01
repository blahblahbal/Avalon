using Avalon.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class AstralProjecting : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.immune = true;
        player.immuneAlpha = 130;
        player.noItems = true;
        player.thorns = 0f;

        if (player.whoAmI != Main.myPlayer)
        {
            return;
        }

        foreach (NPC n in Main.npc)
        {
            if (n.townNPC || n.dontTakeDamage)
            {
                continue;
            }

            if (player.getRect().Intersects(n.getRect()))
            {
                n.AddBuff(ModContent.BuffType<AstralCurse>(), 60 * 45);
            }
        }
        if (player.buffTime[buffIndex] == 1)
        {
            player.AddBuff(ModContent.BuffType<Debuffs.AstralProjectingCooldown>(), 60 * 60);
        }
    }
}
