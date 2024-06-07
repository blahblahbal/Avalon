using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Buffs;

namespace Avalon.Compatability.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvBouncingFlame : ModBuff
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
        ModContent.GetInstance<BouncingFlamePotionBuff>().Update(player, ref buffIndex);
    }
}
