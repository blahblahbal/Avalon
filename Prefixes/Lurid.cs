using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Lurid : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

    public override float RollChance(Item item) => 0.8f;

    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetCritChance(DamageClass.Generic) += 2;
        player.statDefense += 3;
    }
}
