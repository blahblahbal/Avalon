using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvFishing : ModBuff
{
    private const int FishingIncrease = 30;

    public override void Update(Player player, ref int buffIndex)
    {
        player.fishingSkill += FishingIncrease;
    }
}
