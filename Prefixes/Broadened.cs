using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Broadened : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.35f;

    public override bool CanRoll(Item item) => item.IsTool();

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        useTimeMult = 0.9f;
        scaleMult = 1.2f;
    }
    public override void Apply(Item item)
    {
        item.tileBoost += 2;
    }
}
