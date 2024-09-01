using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class TropicalGrassWallSafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(35, 76, 0));
        HitSound = SoundID.Grass;
        DustType = ModContent.DustType<SavannaGrassBladeDust>();
    }
}
