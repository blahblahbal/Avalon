using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace Avalon.ModSupport.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvGlowing : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetStaticDefaults()
    {
        Data.Sets.BuffSets.Elixir[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(ThoriumDamageBase<HealerDamage>.Instance) += 0.15f;
    }
}
