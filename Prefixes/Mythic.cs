using Terraria;

namespace Avalon.Prefixes;

public class Mythic : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.1f;

    public override void UpdateOwnerPlayer(Player player) => player.statManaMax2 += 20;
}
