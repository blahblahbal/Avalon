using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Vigorous : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.25f;

    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetAttackSpeed(DamageClass.Melee) += 0.02f;
        player.GetDamage(DamageClass.Generic) += 0.02f;
    }
}
