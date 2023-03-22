using Terraria;

namespace Avalon.Prefixes;

public class Overactive : ExxoPrefix
{
    public override bool CanRoll(Item item) => true;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.1f;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult,
                                  ref int critBonus) => manaMult += 0.04f;

    public override void UpdateOwnerPlayer(Player player) => player.statManaMax2 += 20;
}
