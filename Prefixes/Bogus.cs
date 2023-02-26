using ExxoAvalonOrigins.Common;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Prefixes;

public class Bogus : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.3f;
    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.06f);
        player.GetCritChance(DamageClass.Generic) += 2;
    }
}
