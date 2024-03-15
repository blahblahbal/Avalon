using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace Avalon.Compatability.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvGlowing : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(ThoriumDamageBase<HealerDamage>.Instance) += 0.15f;
    }
}
