using Avalon.Items.Weapons.Melee.PreHardmode.WoodenClub;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Turboshrunk : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.05f;

    public override bool CanRoll(Item item)
    {
        if (item.type == ModContent.ItemType<WoodenClub>()) return false;
        if (item.width <= 40 && item.height <= 40) return false;
        return item.useTime > 10;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult,
                                  ref int critBonus)
    {
        scaleMult = 0.5f;
        useTimeMult = 0.5f;
        damageMult = 0.75f;
        shootSpeedMult = 0.8f;
    }
}
