using Terraria;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvKinetic : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void SetStaticDefaults()
    {
        Data.Sets.Buffs.Elixir[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.dashType = 1;
    }
}
