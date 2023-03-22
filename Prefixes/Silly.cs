using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Silly : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

    public override void UpdateOwnerPlayer(Player player) => player.GetCritChance(DamageClass.Generic) += 2;
}
