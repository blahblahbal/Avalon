using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BlastedStalac : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        TileID.Sets.BreakableWhenPlacing[Type] = true;
        Main.tileMerge[ModContent.TileType<BlastedStone>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<BlastedStone>()] = true;
        DustType = DustID.Wraith;
        AddMapEntry(new Color(20, 20, 20));
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameY <= 18 || tile.TileFrameY == 72)
        {
            offsetY = -2;
        }
        else if ((tile.TileFrameY >= 36 && tile.TileFrameY <= 54) || tile.TileFrameY == 90)
        {
            offsetY = 2;
        }
    }
}
