using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace Avalon.ModSupport.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvEarworm : ModBuff
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
        player.GetThoriumPlayer().bardBuffDurationX += 0.35f;
    }
}
