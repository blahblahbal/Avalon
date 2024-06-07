using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace Avalon.Compatability.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvHydration : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false; // ModLoader.HasMod("ThoriumMod");
    }
    public override void SetStaticDefaults()
    {
        Data.Sets.Buffs.Elixir[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetThoriumPlayer().techRechargeBonus = 40;
        player.GetThoriumPlayer().throwerExhaustionRegenBonus += 0.35f;
    }
}
