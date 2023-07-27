using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Contagion;

public class ContagionStalagmites : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
        TileObjectData.newTile.Height = 2;
        TileObjectData.newTile.CoordinateHeights = new[] { 18, 16 };
        //TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<Chunkstone>() };
        //TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.ContagionDust>();
        AddMapEntry(new Color(133, 150, 39));
    }
    //public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    //{
    //    offsetY = 2;
    //}
}
