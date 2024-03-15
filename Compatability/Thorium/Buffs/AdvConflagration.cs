using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Buffs;

namespace Avalon.Compatability.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvConflagration : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void Update(Player player, ref int buffIndex)
    {
        ModContent.GetInstance<ConflagrationPotionBuff>().Update(player, ref buffIndex);
    }
}
