using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Efficient : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.1f;

    public override bool CanRoll(Item item) => item.IsTool();

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        useTimeMult = 0.75f;
    }
    //public override void Apply(Item item)
    //{
        
    //    Main.LocalPlayer.GetModPlayer<AvalonPlayer>().EfficiencyPrefix = 5;
    //}
    //public override void UpdateOwnerPlayer(Player player)
    //{
    //    player.GetModPlayer<AvalonPlayer>().EfficiencyPrefix = 5;
    //}
}
