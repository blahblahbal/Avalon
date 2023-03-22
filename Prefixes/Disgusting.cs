using Terraria;

namespace Avalon.Prefixes;

public class Disgusting : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.9f;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.statDefense -= 2;
        player.stinky = true;
    }
}
