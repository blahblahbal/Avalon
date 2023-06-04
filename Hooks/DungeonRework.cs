using Avalon.Common;
using Avalon.Tiles.Furniture.OrangeDungeon;
using Avalon.Tiles.Furniture.PurpleDungeon;
using Avalon.Tiles.Furniture.YellowDungeon;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
//using Avalon.Tiles.Furniture.PurpleDungeon;
//using Avalon.Tiles.Furniture.OrangeDungeon;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class DungeonRework : ModHook
{
    protected override void Apply()
    {
        On_WorldGen.DungeonPitTrap += OnDungeonPitTrap;
    }
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModContent.GetInstance<AvalonConfig>().RevertDungeonGen;
    }
    private static bool OnDungeonPitTrap(On_WorldGen.orig_DungeonPitTrap orig, int i, int j, ushort tileType, int wallType)
    {
        return true;
    }
}
public class DungeonRemoveCrackedBricks : GenPass
{
    public DungeonRemoveCrackedBricks()
        : base("DungeonRemoveCrackedBricks", 10)
    {
    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        #region uncomment later
        int RandDungeonColour = (WorldGen.genRand.Next(3));
        //RandDungeonColour = 1;
        bool otherColors = WorldGen.genRand.NextBool(2);
        if (otherColors)
        {
            for (int i = 100; i < Main.maxTilesX - 100; i++)
            {
                for (int j = 100; j < Main.maxTilesY - 200; j++)
                {
                    if (Main.tile[i, j].TileType is TileID.BlueDungeonBrick or TileID.GreenDungeonBrick or TileID.PinkDungeonBrick &&
                        Main.tile[i, j].HasTile)
                    {
                        switch (RandDungeonColour)
                        {
                            case 0:
                                Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.OrangeBrick>();
                                break;
                            case 1:
                                Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.PurpleBrick>();
                                break;
                            case 2:
                                Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.YellowBrick>();
                                break;
                        }
                    }
                    if (Main.tile[i, j].WallType is WallID.GreenDungeonUnsafe or WallID.BlueDungeonUnsafe or
                        WallID.PinkDungeonUnsafe)
                    {
                        switch (RandDungeonColour)
                        {
                            case 0:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.OrangeBrickUnsafe>();
                                break;
                            case 1:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.PurpleBrickUnsafe>();
                                break;
                            case 2:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.YellowBrickUnsafe>();
                                break;
                        }
                    }
                    if (Main.tile[i, j].WallType is WallID.GreenDungeonSlabUnsafe or WallID.BlueDungeonSlabUnsafe or
                        WallID.PinkDungeonSlabUnsafe)
                    {
                        switch (RandDungeonColour)
                        {
                            case 0:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.OrangeSlabUnsafe>();
                                break;
                            case 1:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.PurpleSlabWallUnsafe>();
                                break;
                            case 2:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.YellowSlabWallUnsafe>();
                                break;
                        }
                    }
                    if (Main.tile[i, j].WallType is WallID.GreenDungeonTileUnsafe or WallID.BlueDungeonTileUnsafe or
                        WallID.PinkDungeonTileUnsafe)
                    {
                        switch (RandDungeonColour)
                        {
                            case 0:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.OrangeTiledUnsafe>();
                                break;
                            case 1:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.PurpleTiledWallUnsafe>();
                                break;
                            case 2:
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.YellowTiledWallUnsafe>();
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Platforms && Main.tile[i, j].TileFrameY is 6 * 18 or 7 * 18 or 8 * 18 &&
                        Main.tile[i, j].HasTile)
                    {
                        switch (RandDungeonColour)
                        {
                            case 0:
                                Main.tile[i, j].TileType = (ushort)ModContent.TileType<OrangeBrickPlatform>();
                                break;
                            case 1:
                                Main.tile[i, j].TileType = (ushort)ModContent.TileType<PurpleBrickPlatform>();
                                break;
                            case 2:
                                Main.tile[i, j].TileType = (ushort)ModContent.TileType<YellowBrickPlatform>();
                                break;
                        }
                        Main.tile[i, j].TileFrameY = 0;
                    }
                    if (Main.tile[i, j].TileType is TileID.Bathtubs && Main.tile[i, j].TileFrameY is >= 756 and <= 846 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<OrangeDungeonBathtub>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<PurpleDungeonBathtub>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<YellowDungeonBathtub>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Beds && Main.tile[i, j].TileFrameY is >= 180 and <= 270 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<OrangeDungeonBed>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<PurpleDungeonBed>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<YellowDungeonBed>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Bookcases && Main.tile[i, j].TileFrameX is >= 54 and <= 198 &&
                        Main.tile[i, j].TileFrameY <= 54 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<OrangeDungeonBookcase>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<PurpleDungeonBookcase>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<YellowDungeonBookcase>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Candles && Main.tile[i, j].TileFrameY is >= 22 and <= 66 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<OrangeDungeonCandle>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<PurpleDungeonCandle>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<YellowDungeonCandle>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Candelabras && Main.tile[i, j].TileFrameY is >= 792 and <= 882 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<OrangeDungeonCandelabra>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<PurpleDungeonCandelabra>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<YellowDungeonCandelabra>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Chairs && Main.tile[i, j].TileFrameY is >= 520 and <= 618 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<OrangeDungeonChair>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<PurpleDungeonChair>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<YellowDungeonChair>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Chandeliers && Main.tile[i, j].TileFrameY is >= 1458 and <= 1602 &&
                        Main.tile[i, j].TileFrameX <= 90 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j, ModContent.TileType<OrangeDungeonChandelier>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j, ModContent.TileType<PurpleDungeonChandelier>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j, ModContent.TileType<YellowDungeonChandelier>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.ClosedDoor && Main.tile[i, j].TileFrameY is >= 864 and <= 1008 &&
                        Main.tile[i, j].TileFrameX <= 36 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j + 1, ModContent.TileType<OrangeDungeonDoorClosed>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j + 1, ModContent.TileType<PurpleDungeonDoorClosed>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j + 1, ModContent.TileType<YellowDungeonDoorClosed>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Dressers && Main.tile[i, j].TileFrameX is >= 270 and <= 414 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<OrangeDungeonDresser>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<PurpleDungeonDresser>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<YellowDungeonDresser>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Lamps && Main.tile[i, j].TileFrameY is >= 1300 and <= 1440 &&
                        Main.tile[i, j].TileFrameX <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j + 2, ModContent.TileType<OrangeDungeonLamp>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j + 2, ModContent.TileType<PurpleDungeonLamp>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j + 2, ModContent.TileType<YellowDungeonLamp>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Pianos && Main.tile[i, j].TileFrameX is >= 594 and <= 738 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<OrangeDungeonPiano>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<PurpleDungeonPiano>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<YellowDungeonPiano>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Sinks && Main.tile[i, j].TileFrameY is >= 380 and <= 474 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<OrangeDungeonSink>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<PurpleDungeonSink>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<YellowDungeonSink>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Benches && Main.tile[i, j].TileFrameX is >= 324 and <= 468 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<OrangeDungeonSofa>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<PurpleDungeonSofa>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<YellowDungeonSofa>(), true);
                                break;
                        }

                    }
                    if (Main.tile[i, j].TileType is TileID.Tables && Main.tile[i, j].TileFrameX is >= 540 and <= 684 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<OrangeDungeonTable>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<PurpleDungeonTable>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<YellowDungeonTable>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.WorkBenches && Main.tile[i, j].TileFrameX is >= 396 and <= 486 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<OrangeDungeonWorkBench>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<PurpleDungeonWorkBench>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j, ModContent.TileType<YellowDungeonWorkBench>(), true);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.Statues && Main.tile[i, j].TileFrameX is >= 1656 and <= 1746 &&
                        Main.tile[i, j].TileFrameY <= 36 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j + 2, ModContent.TileType<Tiles.Statues>(), true, style: 3);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j + 2, ModContent.TileType<Tiles.Statues>(), true, style: 13);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j + 2, ModContent.TileType<Tiles.Statues>(), true, style: 14);
                                break;
                        }
                    }
                    if (Main.tile[i, j].TileType is TileID.GrandfatherClocks && Main.tile[i, j].TileFrameX is >= 1080 and <= 1152 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        switch (RandDungeonColour)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j + 4, ModContent.TileType<OrangeDungeonClock>(), true);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j + 4, ModContent.TileType<PurpleDungeonClock>(), true);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j + 4, ModContent.TileType<YellowDungeonClock>(), true);
                                break;
                        }
                    }
                }
            }
        }
        #endregion
        for (int i = 100; i < Main.maxTilesX - 100; i++)
        {
            for (int j = 100; j < Main.maxTilesY - 200; j++)
            {
                if ((Main.tile[i, j].TileType is TileID.CrackedBlueDungeonBrick or TileID.CrackedGreenDungeonBrick or TileID.CrackedPinkDungeonBrick) &&
                    Main.tile[i, j].HasTile && ModContent.GetInstance<AvalonConfig>().RevertDungeonGen)
                {
                    WorldGen.KillTile(i, j);
                }
                if ((Main.tile[i, j].TileType is TileID.CrackedBlueDungeonBrick or TileID.CrackedGreenDungeonBrick or TileID.CrackedPinkDungeonBrick) &&
                    Main.tile[i, j].HasTile && !ModContent.GetInstance<AvalonConfig>().RevertDungeonGen && otherColors)
                {
                    Tile t = Main.tile[i, j];
                    t.HasTile = false;
                    switch (RandDungeonColour)
                    {
                        case 0:
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrackedOrangeBrick>(), true);
                            break;
                        case 1:
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrackedPurpleBrick>(), true);
                            break;
                        case 2:
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrackedYellowBrick>(), true);
                            break;
                    }
                }
            }
        }
    }
}
