using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Motivated : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;
    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegen -= 40;
        player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegenCount -= 40;
    }
}
