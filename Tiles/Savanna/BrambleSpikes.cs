using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Savanna;

public class BrambleSpikes : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(117, 130, 69), this.GetLocalization("MapEntry"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        DustType = DustID.CorruptGibs;
    }
    public override bool Slope(int i, int j)
    {
        return false;
    }
}
