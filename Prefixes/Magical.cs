using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Magical : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;
    public override void ModifyValue(ref float valueMult) => valueMult *= 1.1f;
    public override bool CanRoll(Item item) => true;
    public override void UpdateOwnerPlayer(Player player) => player.statManaMax2 += 40;
}
