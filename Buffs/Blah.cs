using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Blah : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().Lucky = true;
        player.enemySpawns = true;
        player.GetModPlayer<AvalonPlayer>().AdvancedBattle = true;
        player.maxMinions += 4;
        player.accMerman = true;
        player.lavaImmune = true;
        player.cratePotion = true;
        player.fishingSkill += 100;
        player.statDefense += 10;
        player.endurance += 0.2f;
        player.ammoPotion = true;
        player.dangerSense = true;
        player.sonarPotion = true;
    }
}
