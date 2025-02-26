using Terraria;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Thorium.Buffs;

public class AdvFrenzy : ModBuff
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
        player.GetAttackSpeed(DamageClass.Generic) += 0.12f;
    }
}
