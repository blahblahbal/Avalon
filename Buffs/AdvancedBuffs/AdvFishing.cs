using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvFishing : ModBuff
{
    private const int FishingIncrease = 30;

    public override void Update(Player player, ref int buffIndex)
    {
        player.fishingSkill += FishingIncrease;
    }
}
