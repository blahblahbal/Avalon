using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Contagion;

public class ContagionStalactgmites : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true;
        //TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
        //TileObjectData.newTile.Height = 2;
        //TileObjectData.newTile.Width = 1;
        //TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16 };
        //TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<Chunkstone>() };
        //TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        //TileObjectData.newTile.StyleHorizontal = true;
        //TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.ContagionDust>();
        AddMapEntry(new Color(133, 150, 39));
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

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        WorldGen.CheckTight(i, j);
        return false;
    }
}
