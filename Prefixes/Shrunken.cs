using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Shrunken : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.9f;

    public override bool CanRoll(Item item) => item.IsTool();

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        useTimeMult = 1.12f;
        scaleMult = 0.9f;
    }
    public override void Apply(Item item)
    {
        item.tileBoost--;
    }
}
