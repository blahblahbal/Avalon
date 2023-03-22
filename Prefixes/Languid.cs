using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Languid : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.9f;

    public override bool CanRoll(Item item) => true;

    public override void Apply(Item item)
    {
    }

    public override void UpdateOwnerPlayer(Player player) => player.moveSpeed -= 0.02f;
}
