using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CaesiumCrystal : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(10, 102, 32));
        Main.tileSolid[Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Ores.CaesiumOre>()] = true;
        Main.tileMerge[ModContent.TileType<Ores.CaesiumOre>()][Type] = true;
        Main.tileBrick[Type] = true;
        DustType = ModContent.DustType<Dusts.CaesiumDust>();
        HitSound = SoundID.Item27;
    }
}
