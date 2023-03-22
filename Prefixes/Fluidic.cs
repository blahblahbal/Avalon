using Terraria;

namespace Avalon.Prefixes;

public class Fluidic : ExxoPrefix
{
    public override ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.Armor;
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.25f;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.moveSpeed += 0.05f;
        player.ignoreWater = true;
    }
}
