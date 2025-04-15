using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Utilities;

namespace Avalon.ModSupport.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvHoly: ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetStaticDefaults()
    {
        Data.Sets.BuffSets.Elixir[Type] = true;
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        rare = 3;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetThoriumPlayer().healBonus += 2;
    }
}
