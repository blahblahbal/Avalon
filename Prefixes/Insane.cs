using Terraria;

namespace Avalon.Prefixes;

public class Insane : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.tileSpeed += 0.3f;
        player.wallSpeed += 0.3f;
    }
}
