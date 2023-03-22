using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Robust : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.35f;

    public override float RollChance(Item item) => 0.5f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult,
                                  ref int critBonus) => damageMult += 0.03f;

    public override void UpdateOwnerPlayer(Player player) => player.statDefense += 3;
}
