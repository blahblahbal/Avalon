using Terraria;

namespace Avalon.Prefixes;

public class Slimy : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;

    public override float RollChance(Item item) => 3f;
    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void UpdateOwnerPlayer(Player player) => player.endurance += 0.03f;
}
