using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Fantastic : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Ranged;
    public override void ModifyValue(ref float valueMult) => valueMult *= 1.3f;
    public override bool CanRoll(Item item) => true;

	public override float RollChance(Item item) => 0.8f;

	public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 1.2f;
        damageMult = 1.2f;
        critBonus = 6;
        shootSpeedMult = 1.13f;
        useTimeMult = 0.85f;
    }
}
