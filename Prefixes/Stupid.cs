using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Stupid : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.8f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 1.16f;
        damageMult = 0.7f;
        useTimeMult = 1.05f;
        scaleMult = 1.25f;
    }
}
