using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Bogus : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.3f;
    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.04f);
        player.GetModPlayer<AvalonPlayer>().AllMaxCrit(1);
        player.GetCritChance(DamageClass.Generic) += 2;
    }
}
