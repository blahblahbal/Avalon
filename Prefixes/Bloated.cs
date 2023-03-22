using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Bloated : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.05f;
        player.GetAttackSpeed(DamageClass.Melee) -= 0.02f;
    }
}
