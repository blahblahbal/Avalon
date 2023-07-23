using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BloodiedSpike : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(195, 61, 40), LanguageManager.Instance.GetText("Bloodied Spike"));
        Main.tileSolid[Type] = true;
        DustType = DustID.Palladium;
        HitSound = SoundID.Tink;
    }
}
