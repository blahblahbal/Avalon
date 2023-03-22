using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Phantasmal : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Magic;
    public override void ModifyValue(ref float valueMult) => valueMult *= 1.3f;
    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 1.2f;
        damageMult = 1.19f;
        critBonus = 6;
        useTimeMult = 0.85f;
        manaMult = 0.85f;
    }
}
