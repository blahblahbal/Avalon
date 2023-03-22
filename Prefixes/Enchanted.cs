using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Enchanted : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.125f;

    public override float RollChance(Item item) => 3f;

    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.statManaMax2 += 20;
        player.moveSpeed += 0.03f;
        player.statDefense++;
    }
}
