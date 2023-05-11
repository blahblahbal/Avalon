using Terraria;

namespace Avalon.Common
{
    internal class TileMerge
    {
        public enum Similarity
        {
            None,
            Same,
            Merge
        }

        public static Similarity GetSimilarity(Tile check, int myType, int mergeType)
        {
            if (!check.HasTile)
            {
                return Similarity.None;
            }
            if (check.TileType == myType || Main.tileMerge[myType][check.TileType])
            {
                return Similarity.Same;
            }
            if (check.TileType == mergeType)
            {
                return Similarity.Merge;
            }
            return Similarity.None;
        }

        public static void MergeWith(int type1, int type2, bool merge = true)
        {
            if (type1 != type2)
            {
                Main.tileMerge[type1][type2] = merge;
                Main.tileMerge[type2][type1] = merge;
            }
        }

        private static void SetFrame(int x, int y, int frameX, int frameY)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null)
            {
                tile.TileFrameX = (short)frameX;
                tile.TileFrameY = (short)frameY;
            }
        }

        internal static void MergeWithFrameExplicit(int x, int y, int myType, int mergeType, out bool mergedUp, out bool mergedLeft, out bool mergedRight, out bool mergedDown, bool forceSameDown = false, bool forceSameUp = false, bool forceSameLeft = false, bool forceSameRight = false, bool resetFrame = true)
        {
            if (Main.tile[x, y] == null || x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
            {
                mergedUp = (mergedLeft = (mergedRight = (mergedDown = false)));
                return;
            }
            Main.tileMerge[myType][mergeType] = false;
            Tile tileLeft = Main.tile[x - 1, y];
            Tile tileRight = Main.tile[x + 1, y];
            Tile tileUp = Main.tile[x, y - 1];
            Tile tileDown = Main.tile[x, y + 1];
            Tile tileTopLeft = Main.tile[x - 1, y - 1];
            Tile tileTopRight = Main.tile[x + 1, y - 1];
            Tile tileBottomLeft = Main.tile[x - 1, y + 1];
            Tile check = Main.tile[x + 1, y + 1];
            Similarity leftSim = ((!forceSameLeft) ? GetSimilarity(tileLeft, myType, mergeType) : Similarity.Same);
            Similarity rightSim = ((!forceSameRight) ? GetSimilarity(tileRight, myType, mergeType) : Similarity.Same);
            Similarity upSim = ((!forceSameUp) ? GetSimilarity(tileUp, myType, mergeType) : Similarity.Same);
            Similarity downSim = ((!forceSameDown) ? GetSimilarity(tileDown, myType, mergeType) : Similarity.Same);
            Similarity topLeftSim = GetSimilarity(tileTopLeft, myType, mergeType);
            Similarity topRightSim = GetSimilarity(tileTopRight, myType, mergeType);
            Similarity bottomLeftSim = GetSimilarity(tileBottomLeft, myType, mergeType);
            Similarity bottomRightSim = GetSimilarity(check, myType, mergeType);
            int randomFrame;
            if (resetFrame)
            {
                randomFrame = WorldGen.genRand.Next(3);
                Tile t = Main.tile[x, y];
                t.TileFrameNumber = randomFrame;
            }
            else
            {
                randomFrame = Main.tile[x, y].TileFrameNumber;
            }
            mergedDown = (mergedLeft = (mergedRight = (mergedUp = false)));
            switch (leftSim)
            {
                case Similarity.None:
                    switch (upSim)
                    {
                        case Similarity.Same:
                            switch (downSim)
                            {
                                case Similarity.Same:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            SetFrame(x, y, 0, 18 * randomFrame);
                                            break;

                                        case Similarity.Merge:
                                            mergedRight = true;
                                            SetFrame(x, y, 234 + (18 * randomFrame), 36);
                                            break;

                                        default:
                                            SetFrame(x, y, 90, 18 * randomFrame);
                                            break;
                                    }
                                    break;

                                case Similarity.Merge:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            mergedDown = true;
                                            SetFrame(x, y, 72, 90 + (18 * randomFrame));
                                            break;

                                        case Similarity.Merge:
                                            SetFrame(x, y, 108 + (18 * randomFrame), 54);
                                            break;

                                        default:
                                            mergedDown = true;
                                            SetFrame(x, y, 126, 90 + (18 * randomFrame));
                                            break;
                                    }
                                    break;

                                default:
                                    if (rightSim == Similarity.Same)
                                    {
                                        SetFrame(x, y, 36 * randomFrame, 72);
                                    }
                                    else
                                    {
                                        SetFrame(x, y, 108 + (18 * randomFrame), 54);
                                    }
                                    break;
                            }
                            break;

                        case Similarity.Merge:
                            switch (downSim)
                            {
                                case Similarity.Same:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            mergedUp = true;
                                            SetFrame(x, y, 72, 144 + (18 * randomFrame));
                                            break;

                                        case Similarity.Merge:
                                            SetFrame(x, y, 108 + (18 * randomFrame), 0);
                                            break;

                                        default:
                                            mergedUp = true;
                                            SetFrame(x, y, 126, 144 + (18 * randomFrame));
                                            break;
                                    }
                                    break;

                                case Similarity.Merge:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            SetFrame(x, y, 162, 18 * randomFrame);
                                            break;

                                        case Similarity.Merge:
                                            SetFrame(x, y, 162 + (18 * randomFrame), 54);
                                            break;

                                        default:
                                            mergedUp = true;
                                            mergedDown = true;
                                            SetFrame(x, y, 108, 216 + (18 * randomFrame));
                                            break;
                                    }
                                    break;

                                default:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            SetFrame(x, y, 162, 18 * randomFrame);
                                            break;

                                        case Similarity.Merge:
                                            SetFrame(x, y, 162 + (18 * randomFrame), 54);
                                            break;

                                        default:
                                            mergedUp = true;
                                            SetFrame(x, y, 108, 144 + (18 * randomFrame));
                                            break;
                                    }
                                    break;
                            }
                            break;

                        default:
                            switch (downSim)
                            {
                                case Similarity.Same:
                                    if (rightSim == Similarity.Same)
                                    {
                                        SetFrame(x, y, 36 * randomFrame, 54);
                                        break;
                                    }
                                    _ = 1;
                                    SetFrame(x, y, 108 + (18 * randomFrame), 0);
                                    break;

                                case Similarity.Merge:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            SetFrame(x, y, 162, 18 * randomFrame);
                                            break;

                                        case Similarity.Merge:
                                            SetFrame(x, y, 162 + (18 * randomFrame), 54);
                                            break;

                                        default:
                                            mergedDown = true;
                                            SetFrame(x, y, 108, 90 + (18 * randomFrame));
                                            break;
                                    }
                                    break;

                                default:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            SetFrame(x, y, 162, 18 * randomFrame);
                                            break;

                                        case Similarity.Merge:
                                            mergedRight = true;
                                            SetFrame(x, y, 54 + (18 * randomFrame), 234);
                                            break;

                                        default:
                                            SetFrame(x, y, 162 + (18 * randomFrame), 54);
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    return;

                case Similarity.Merge:
                    switch (upSim)
                    {
                        case Similarity.Same:
                            switch (downSim)
                            {
                                case Similarity.Same:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            mergedLeft = true;
                                            SetFrame(x, y, 162, 126 + (18 * randomFrame));
                                            break;

                                        case Similarity.Merge:
                                            mergedLeft = true;
                                            mergedRight = true;
                                            SetFrame(x, y, 180, 126 + (18 * randomFrame));
                                            break;

                                        default:
                                            mergedLeft = true;
                                            SetFrame(x, y, 234 + (18 * randomFrame), 54);
                                            break;
                                    }
                                    break;

                                case Similarity.Merge:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            mergedLeft = (mergedDown = true);
                                            SetFrame(x, y, 36, 108 + (36 * randomFrame));
                                            break;

                                        case Similarity.Merge:
                                            mergedLeft = (mergedRight = (mergedDown = true));
                                            SetFrame(x, y, 198, 144 + (18 * randomFrame));
                                            break;

                                        default:
                                            SetFrame(x, y, 108 + (18 * randomFrame), 54);
                                            break;
                                    }
                                    break;

                                default:
                                    if (rightSim == Similarity.Same)
                                    {
                                        mergedLeft = true;
                                        SetFrame(x, y, 18 * randomFrame, 216);
                                    }
                                    else
                                    {
                                        SetFrame(x, y, 108 + (18 * randomFrame), 54);
                                    }
                                    break;
                            }
                            break;

                        case Similarity.Merge:
                            switch (downSim)
                            {
                                case Similarity.Same:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            mergedUp = (mergedLeft = true);
                                            SetFrame(x, y, 36, 90 + (36 * randomFrame));
                                            break;

                                        case Similarity.Merge:
                                            mergedLeft = (mergedRight = (mergedUp = true));
                                            SetFrame(x, y, 198, 90 + (18 * randomFrame));
                                            break;

                                        default:
                                            SetFrame(x, y, 108 + (18 * randomFrame), 0);
                                            break;
                                    }
                                    break;

                                case Similarity.Merge:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            mergedUp = (mergedLeft = (mergedDown = true));
                                            SetFrame(x, y, 216, 90 + (18 * randomFrame));
                                            break;

                                        case Similarity.Merge:
                                            mergedDown = (mergedLeft = (mergedRight = (mergedUp = true)));
                                            SetFrame(x, y, 108 + (18 * randomFrame), 198);
                                            break;

                                        default:
                                            SetFrame(x, y, 162 + (18 * randomFrame), 54);
                                            break;
                                    }
                                    break;

                                default:
                                    if (rightSim == Similarity.Same)
                                    {
                                        SetFrame(x, y, 162, 18 * randomFrame);
                                    }
                                    else
                                    {
                                        SetFrame(x, y, 162 + (18 * randomFrame), 54);
                                    }
                                    break;
                            }
                            break;

                        default:
                            switch (downSim)
                            {
                                case Similarity.Same:
                                    if (rightSim == Similarity.Same)
                                    {
                                        mergedLeft = true;
                                        SetFrame(x, y, 18 * randomFrame, 198);
                                    }
                                    else
                                    {
                                        _ = 1;
                                        SetFrame(x, y, 108 + (18 * randomFrame), 0);
                                    }
                                    break;

                                case Similarity.Merge:
                                    if (rightSim == Similarity.Same)
                                    {
                                        SetFrame(x, y, 162, 18 * randomFrame);
                                        break;
                                    }
                                    _ = 1;
                                    SetFrame(x, y, 162 + (18 * randomFrame), 54);
                                    break;

                                default:
                                    switch (rightSim)
                                    {
                                        case Similarity.Same:
                                            mergedLeft = true;
                                            SetFrame(x, y, 18 * randomFrame, 252);
                                            break;

                                        case Similarity.Merge:
                                            mergedRight = (mergedLeft = true);
                                            SetFrame(x, y, 162 + (18 * randomFrame), 198);
                                            break;

                                        default:
                                            mergedLeft = true;
                                            SetFrame(x, y, 18 * randomFrame, 234);
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    return;
            }
            switch (upSim)
            {
                case Similarity.Same:
                    switch (downSim)
                    {
                        case Similarity.Same:
                            switch (rightSim)
                            {
                                case Similarity.Same:
                                    if (topLeftSim == Similarity.Merge || topRightSim == Similarity.Merge || bottomLeftSim == Similarity.Merge || bottomRightSim == Similarity.Merge)
                                    {
                                        if (bottomRightSim == Similarity.Merge)
                                        {
                                            SetFrame(x, y, 0, 90 + (36 * randomFrame));
                                        }
                                        else if (bottomLeftSim == Similarity.Merge)
                                        {
                                            SetFrame(x, y, 18, 90 + (36 * randomFrame));
                                        }
                                        else if (topRightSim == Similarity.Merge)
                                        {
                                            SetFrame(x, y, 0, 108 + (36 * randomFrame));
                                        }
                                        else
                                        {
                                            SetFrame(x, y, 18, 108 + (36 * randomFrame));
                                        }
                                        break;
                                    }
                                    switch (topLeftSim)
                                    {
                                        case Similarity.Same:
                                            if (topRightSim == Similarity.Same)
                                            {
                                                if (bottomLeftSim == Similarity.Same)
                                                {
                                                    SetFrame(x, y, 18 + (18 * randomFrame), 18);
                                                }
                                                else if (bottomRightSim == Similarity.Same)
                                                {
                                                    SetFrame(x, y, 18 + (18 * randomFrame), 18);
                                                }
                                                else
                                                {
                                                    SetFrame(x, y, 108 + (18 * randomFrame), 36);
                                                }
                                                return;
                                            }
                                            if (bottomLeftSim != 0)
                                            {
                                                break;
                                            }
                                            if (bottomRightSim == Similarity.Same)
                                            {
                                                if (topRightSim == Similarity.Merge)
                                                {
                                                    SetFrame(x, y, 0, 108 + (36 * randomFrame));
                                                }
                                                else
                                                {
                                                    SetFrame(x, y, 18 + (18 * randomFrame), 18);
                                                }
                                            }
                                            else
                                            {
                                                SetFrame(x, y, 198, 18 * randomFrame);
                                            }
                                            return;

                                        case Similarity.None:
                                            if (topRightSim == Similarity.Same)
                                            {
                                                if (bottomRightSim == Similarity.Same)
                                                {
                                                    SetFrame(x, y, 18 + (18 * randomFrame), 18);
                                                }
                                                else
                                                {
                                                    SetFrame(x, y, 18 + (18 * randomFrame), 18);
                                                }
                                            }
                                            else
                                            {
                                                SetFrame(x, y, 18 + (18 * randomFrame), 18);
                                            }
                                            return;
                                    }
                                    SetFrame(x, y, 18 + (18 * randomFrame), 18);
                                    break;

                                case Similarity.Merge:
                                    mergedRight = true;
                                    SetFrame(x, y, 144, 126 + (18 * randomFrame));
                                    break;

                                default:
                                    SetFrame(x, y, 72, 18 * randomFrame);
                                    break;
                            }
                            break;

                        case Similarity.Merge:
                            switch (rightSim)
                            {
                                case Similarity.Same:
                                    mergedDown = true;
                                    SetFrame(x, y, 144 + (18 * randomFrame), 90);
                                    break;

                                case Similarity.Merge:
                                    mergedDown = (mergedRight = true);
                                    SetFrame(x, y, 54, 108 + (36 * randomFrame));
                                    break;

                                default:
                                    mergedDown = true;
                                    SetFrame(x, y, 90, 90 + (18 * randomFrame));
                                    break;
                            }
                            break;

                        default:
                            switch (rightSim)
                            {
                                case Similarity.Same:
                                    SetFrame(x, y, 18 + (18 * randomFrame), 36);
                                    break;

                                case Similarity.Merge:
                                    mergedRight = true;
                                    SetFrame(x, y, 54 + (18 * randomFrame), 216);
                                    break;

                                default:
                                    SetFrame(x, y, 18 + (36 * randomFrame), 72);
                                    break;
                            }
                            break;
                    }
                    return;

                case Similarity.Merge:
                    switch (downSim)
                    {
                        case Similarity.Same:
                            switch (rightSim)
                            {
                                case Similarity.Same:
                                    mergedUp = true;
                                    SetFrame(x, y, 144 + (18 * randomFrame), 108);
                                    break;

                                case Similarity.Merge:
                                    mergedRight = (mergedUp = true);
                                    SetFrame(x, y, 54, 90 + (36 * randomFrame));
                                    break;

                                default:
                                    mergedUp = true;
                                    SetFrame(x, y, 90, 144 + (18 * randomFrame));
                                    break;
                            }
                            break;

                        case Similarity.Merge:
                            switch (rightSim)
                            {
                                case Similarity.Same:
                                    mergedUp = (mergedDown = true);
                                    SetFrame(x, y, 144 + (18 * randomFrame), 180);
                                    break;

                                case Similarity.Merge:
                                    mergedUp = (mergedRight = (mergedDown = true));
                                    SetFrame(x, y, 216, 144 + (18 * randomFrame));
                                    break;

                                default:
                                    SetFrame(x, y, 216, 18 * randomFrame);
                                    break;
                            }
                            break;

                        default:
                            if (rightSim == Similarity.Same)
                            {
                                mergedUp = true;
                                SetFrame(x, y, 234 + (18 * randomFrame), 18);
                            }
                            else
                            {
                                SetFrame(x, y, 216, 18 * randomFrame);
                            }
                            break;
                    }
                    return;
            }
            switch (downSim)
            {
                case Similarity.Same:
                    switch (rightSim)
                    {
                        case Similarity.Same:
                            SetFrame(x, y, 18 + (18 * randomFrame), 0);
                            break;

                        case Similarity.Merge:
                            mergedRight = true;
                            SetFrame(x, y, 54 + (18 * randomFrame), 198);
                            break;

                        default:
                            SetFrame(x, y, 18 + (36 * randomFrame), 54);
                            break;
                    }
                    break;

                case Similarity.Merge:
                    if (rightSim == Similarity.Same)
                    {
                        mergedDown = true;
                        SetFrame(x, y, 234 + (18 * randomFrame), 0);
                    }
                    else
                    {
                        SetFrame(x, y, 216, 18 * randomFrame);
                    }
                    break;

                default:
                    switch (rightSim)
                    {
                        case Similarity.Same:
                            SetFrame(x, y, 108 + (18 * randomFrame), 72);
                            break;

                        case Similarity.Merge:
                            mergedRight = true;
                            SetFrame(x, y, 54 + (18 * randomFrame), 252);
                            break;

                        default:
                            SetFrame(x, y, 216, 18 * randomFrame);
                            break;
                    }
                    break;
            }
        }

        public static void MergeWithFrame(int x, int y, int myType, int mergeType, bool forceSameDown = false, bool forceSameUp = false, bool forceSameLeft = false, bool forceSameRight = false, bool resetFrame = true)
        {
            MergeWithFrameExplicit(x, y, myType, mergeType, out _, out _, out _, out _, forceSameDown, forceSameUp, forceSameLeft, forceSameRight, resetFrame);
        }
    }
}
