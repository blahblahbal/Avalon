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
        AddMapEntry(new Color(195, 61, 40), this.GetLocalization("MapEntry"));
        Main.tileSolid[Type] = true;
        DustType = DustID.Palladium;
        HitSound = SoundID.Tink;
    }
}
