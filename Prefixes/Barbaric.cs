using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Barbaric : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.25f;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetDamage<GenericDamageClass>() += 0.04f;
        player.GetKnockback<GenericDamageClass>() += 0.06f;
    }
}
