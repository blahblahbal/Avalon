﻿using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Gilded : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.05f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult,
                                  ref int critBonus) => damageMult = 1.1f;
}
