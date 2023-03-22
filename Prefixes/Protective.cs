using Terraria;

namespace Avalon.Prefixes;

public class Protective : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.15f;

    public override void UpdateOwnerPlayer(Player player) => player.statDefense += 2;
}
