using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Greedy : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.4f;
    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().GreedyPrefix = true;
    }
}
