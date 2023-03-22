using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Horrific : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Ranged;

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.775f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 1.1f;
        damageMult = 0.9f;
        critBonus = -3;
        shootSpeedMult = 0.9f;
    }
}
