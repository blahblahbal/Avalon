using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Woozy : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Ranged;

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.9f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.05f;
        shootSpeedMult = 0.93f;
        useTimeMult = 1.12f;
    }
}
