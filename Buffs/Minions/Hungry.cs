using Avalon.Common.Players;
using Avalon.Players;
using Avalon.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Minions;

// TODO: IMPLEMENT
public class Hungry : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = false;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<HungrySummon>()] > 0)
        {
            player.GetModPlayer<AvalonPlayer>().hungryMinion = true;
        }
        if (!player.GetModPlayer<AvalonPlayer>().hungryMinion)
        {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
        else
        {
            player.buffTime[buffIndex] = 18000;
        }
    }
}
