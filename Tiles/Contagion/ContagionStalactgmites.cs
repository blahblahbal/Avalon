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
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        TileID.Sets.BreakableWhenPlacing[Type] = true;
        Main.tileMerge[ModContent.TileType<Chunkstone>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Chunkstone>()] = true;
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
    //LEAVE THIS HERE just in case we need it later lol
    //public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    //{
    //    //WorldGen.CheckTight(i, j);
    //    Tile tAbove = Framing.GetTileSafely(i, j - 1);
    //    Tile tBelow = Framing.GetTileSafely(i, j + 1);
    //    Tile tUpRight = Framing.GetTileSafely(i + 1, j - 1);
    //    Tile tUpLeft = Framing.GetTileSafely(i - 1, j - 1);
    //    Tile tDownRight = Framing.GetTileSafely(i + 1, j + 1);
    //    Tile tDownLeft = Framing.GetTileSafely(i - 1, j + 1);
    //    Tile tUp2 = Framing.GetTileSafely(i, j - 2);
    //    Tile tDown2 = Framing.GetTileSafely(i, j + 2);

    //    Tile tUp2Left = Framing.GetTileSafely(i - 1, j - 2);
    //    Tile tUp2Right = Framing.GetTileSafely(i + 1, j - 2);

    //    Tile tUpLeft2 = Framing.GetTileSafely(i - 2, j - 1);
    //    Tile tUpRight2 = Framing.GetTileSafely(i + 2, j - 1);
    //    ushort chunk = (ushort)ModContent.TileType<Chunkstone>();
    //    ushort dirt = TileID.Dirt;
    //    if (tAbove.TileType == chunk && tAbove.HasTile)
    //    {
    //        if (tUpRight.TileType == dirt && tUpRight.HasTile)
    //        {
    //            if (!tUp2.HasTile)
    //            {
    //                if (tUpLeft.HasTile)
    //                {
    //                    // up 2 inactive, up-right dirt, up-left dirt
    //                    if (tUpLeft.TileType == dirt)
    //                    {
    //                        tAbove.TileFrameX = (short)(18 * WorldGen.genRand.Next(7, 10));
    //                        tAbove.TileFrameY = 0;
    //                        if (tUpLeft2.HasTile)
    //                        {
    //                            tUpLeft.TileFrameX = 216;
    //                            tUpLeft.TileFrameY = (short)(18 * WorldGen.genRand.Next(3));
    //                        }
    //                        else
    //                        {
    //                            tUpLeft.TileFrameX = (short)(18 * WorldGen.genRand.Next(9, 12));
    //                            tUpLeft.TileFrameY = 54;
    //                        }
    //                    }
    //                    // up 2 inactive, up-right dirt, up-left stone tile
    //                    else if (TileID.Sets.Stone[tUpLeft.TileType])
    //                    {

    //                        tAbove.TileFrameX = (short)(18 * WorldGen.genRand.Next(3, 6));
    //                        tAbove.TileFrameY = 198;
    //                        if (tUpLeft2.HasTile)
    //                        {
    //                            tUpLeft.TileFrameX = 216;
    //                            tUpLeft.TileFrameY = (short)(18 * WorldGen.genRand.Next(3));
    //                        }
    //                        else
    //                        {
    //                            tUpLeft.TileFrameX = (short)(18 * WorldGen.genRand.Next(10, 13));
    //                            tUpLeft.TileFrameY = 54;
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (tUpLeft.HasTile)
    //                {
    //                    if (tUpLeft.TileType == dirt)
    //                    {
    //                        //tAbove.TileFrameX = ;
    //                    }
    //                    else if (tUpLeft.TileType == chunk)
    //                    {

    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return false;
    //}
}
