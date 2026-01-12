using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Blastplains;

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
        Common.TileMerge.MergeWith(Type, ModContent.TileType<Ores.CaesiumOre>());
        HitSound = SoundID.Item27;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Common.TileMerge.MergeWithFrame(i, j, Type, ModContent.TileType<Ores.CaesiumOre>(), false, false, false, false, resetFrame);
        return false;
    }
}
