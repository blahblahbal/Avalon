using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Expanded : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.1f;

    public override bool CanRoll(Item item) => false; // item.IsTool();

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        useTimeMult = 1.5f;
    }
}
