using Avalon.Common;
using Avalon.Items.Placeable.Painting;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

/// <summary>
/// 6x4 Painting tile
/// </summary>
public class Paintings : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileSpelunker[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 6;
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new int[]
        {
            16,
            16,
            16,
            16
        };
        TileObjectData.newTile.AnchorWall = true;
        TileObjectData.addTile(Type);
        DustType = 7;
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(120, 85, 60));
    }
}
public class PaintingsInDungeonHook : ModHook
{
    protected override void Apply()
    {
        On_WorldGen.RandPictureTile += On_WorldGen_RandPictureTile;
    }

    private PaintingEntry On_WorldGen_RandPictureTile(On_WorldGen.orig_RandPictureTile orig)
    {
        PaintingEntry pe = orig.Invoke();
        if (pe.tileType == TileID.Painting6X4 && WorldGen.genRand.NextBool(1))
        {
            switch (WorldGen.genRand.Next(3))
            {
                case 0:
                    pe.tileType = ModContent.TileType<Paintings>();
                    pe.style = 1;
                    break;
                case 1:
                    pe.tileType = ModContent.TileType<Paintings>();
                    pe.style = 2;
                    break;
                case 2:
                    pe.tileType = ModContent.TileType<Paintings>();
                    pe.style = 5;
                    break;
            }
        }
        return pe;
    }
}
