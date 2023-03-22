using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Drunken : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.9f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 0.93f;
        damageMult = 1.25f;
        critBonus = -2;
        useTimeMult = 1.1f;
        scaleMult = 1.3f;
    }
}
