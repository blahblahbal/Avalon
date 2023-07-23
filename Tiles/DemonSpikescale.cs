using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class DemonSpikescale : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry((Color.Indigo), LanguageManager.Instance.GetText("Demon Spikescale"));
        Main.tileSolid[Type] = true;
        DustType = DustID.CorruptionThorns;
        HitSound = SoundID.Tink;
    }
}
