using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace Avalon.ModSupport.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvHydration : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
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
