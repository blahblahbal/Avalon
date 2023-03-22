using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Dismal : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;

    public override bool CanRoll(Item item) => true;

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.7f;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult,
                                  ref int critBonus) => damageMult = 0.6f;
}
