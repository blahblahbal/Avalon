using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Colossal : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult,
                                  ref int critBonus) => scaleMult = 1.5f;
}
