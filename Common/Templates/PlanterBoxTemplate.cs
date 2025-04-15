using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Templates;

public abstract class PlanterBoxTemplate : ModTile
{
    public virtual int DropItem => 0;
    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileLavaDeath[Type] = false;
        TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[Type] = true;
        AddMapEntry(new Color(191, 142, 111));
        TileID.Sets.DisableSmartCursor[Type] = true;
        AdjTiles = new int[] { TileID.PlanterBox };
        DustType = DustID.Dirt;
        Data.Sets.TileSets.SuitableForPlantingHerbs[Type] = true;
        RegisterItemDrop(DropItem);
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Tile tile = Main.tile[i, j];
        int tType = tile.TileType;
        Rectangle rectangle = new Rectangle(-1, -1, 0, 0);
        Tile tile23 = Main.tile[i - 1, j];
        if (tile23 == null)
        {
            return false;
        }
        Tile tile30 = Main.tile[i + 1, j];
        if (!(tile30 == null) && !(Main.tile[i - 1, j + 1] == null) && !(Main.tile[i + 1, j + 1] == null) && !(Main.tile[i - 1, j - 1] == null) && Main.tile[i + 1, j - 1] != null)
        {
            int num12 = -1;
            int num23 = -1;
            if (tile23 != null && tile23.HasTile)
            {
                num23 = Main.tileStone[tile23.TileType] ? 1 : tile23.TileType;
            }
            if (tile30 != null && tile30.HasTile)
            {
                num12 = Main.tileStone[tile30.TileType] ? 1 : tile30.TileType;
            }
            if (num12 >= 0 && !Main.tileSolid[num12])
            {
                num12 = -1;
            }
            if (num23 >= 0 && !Main.tileSolid[num23])
            {
                num23 = -1;
            }
            if (num23 == tType && num12 == tType)
            {
                rectangle.X = 18;
            }
            else if (num23 == tType && num12 != tType)
            {
                rectangle.X = 36;
            }
            else if (num23 != tType && num12 == tType)
            {
                rectangle.X = 0;
            }
            else
            {
                rectangle.X = 54;
            }
            tile.TileFrameX = (short)rectangle.X;
        }
        return false;
    }
    public override void PostSetDefaults()
    {
        Main.tileNoSunLight[Type] = false;
    }
}
