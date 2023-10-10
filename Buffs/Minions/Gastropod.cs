using Terraria;
using Terraria.ModLoader;
using Avalon.Projectiles.Summon;
using Avalon.Common.Players;

namespace Avalon.Buffs.Minions;

// TODO: Implement
public class Gastropod : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<GastrominiSummon>()] > 0)
        {
            player.GetModPlayer<AvalonPlayer>().GastroMinion = true;
        }
        if (!player.GetModPlayer<AvalonPlayer>().GastroMinion)
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
