using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.WorldGen;

namespace Avalon.Tiles.GemTrees;

public class PeridotSapling : ModTile
{

    public static GrowTreeSettings GemTree_Peridot = new GrowTreeSettings
    {
        GroundTest = GemTreeGroundTest,
        WallTest = GemTreeWallTest,
        TreeHeightMax = 12,
        TreeHeightMin = 7,
        TreeTileType = 5,
        TreeTopPaddingNeeded = 4,
        SaplingTileType = (ushort)ModContent.TileType<PeridotSapling>()
    };
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        //TileID.Sets.CommonSapling[Type] = true;
        //TileID.Sets.TreeSapling[Type] = true;
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 2;
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.AnchorValidTiles = new int[16] { 1, 25, 117, 203, 182, 180, 179, 381, 183, 181, 534, 536, 539, 625, 627, ModContent.TileType<Contagion.Chunkstone>() };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.DrawFlipHorizontal = true;
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.RandomStyleRange = 3;
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(200, 200, 200));
        DustType = ModContent.DustType<Dusts.PeridotDust>();
        AdjTiles = new int[1] { TileID.Saplings };
    }

    public override void RandomUpdate(int i, int j)
    {
        if (WorldGen.genRand.NextBool(20))
        {
            bool flag = WorldGen.PlayerLOS(i, j);
            if (WorldGen.GrowTree(i, j) && flag)
            {
                WorldGen.TreeGrowFXCheck(i, j);
            }
        }
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
    {
        if (i % 2 == 1)
        {
            effects = SpriteEffects.FlipHorizontally;
        }
    }
}
