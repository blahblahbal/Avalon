using Avalon.Tiles;
using Avalon.Tiles.Ores;
using Avalon.WorldGeneration.Structures;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class Underworld : GenPass
{
    public Underworld() : base("Avalon Underworld", 120f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        /*progress.Message = "Generating Caesium Blastplains";
        #region caesium blastplains
        //for (var i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 0.0008); i++)
        //{
        //    WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY - 150, Main.maxTilesY), WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 5), (ushort)ModContent.TileType<Tiles.CaesiumOre>());
        //}
        int caesiumXPosLeft = Main.maxTilesX - (Main.maxTilesX / 5) - 15;
        int caesiumXPosRight = Main.maxTilesX - (Main.maxTilesX / 5);
        int caesiumMaxRight = Main.maxTilesX - 20;
        if (Main.drunkWorld)
        {
            caesiumXPosLeft = Main.maxTilesX - (Main.maxTilesX / 3) - 15;
            caesiumXPosRight = Main.maxTilesX - (Main.maxTilesX / 3);
            caesiumMaxRight = Main.maxTilesX - (Main.maxTilesX / 5) + 50;
        }
        for (int q = caesiumXPosLeft; q < caesiumXPosRight; q++)
        {
            for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
            {
                if (q > caesiumXPosLeft + 5)
                {
                    if (WorldGen.genRand.Next(10) == 0 && Main.tile[q, z].HasTile &&
                        Main.tile[q, z].TileType == TileID.Ash)
                    {
                        WorldGen.TileRunner(q, z, WorldGen.genRand.Next(6, 8), WorldGen.genRand.Next(6, 8),
                            ModContent.TileType<BlastedStone>());
                    }
                }
            }
        }

        for (int q = caesiumXPosRight; q < caesiumMaxRight; q++)
        {
            for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
            {
                //if (WorldGen.genRand.Next(20) == 0 && z >= Main.maxTilesY - 250 && z <= Main.maxTilesY - 241 && Main.tile[q, z].HasTile) Main.tile[q, z].type = (ushort)ModContent.TileType<Tiles.BlackBlaststone>();
                //if (WorldGen.genRand.Next(10) == 0 && z >= Main.maxTilesY - 240 && z <= Main.maxTilesY - 231 && Main.tile[q, z].HasTile) Main.tile[q, z].type = (ushort)ModContent.TileType<Tiles.BlackBlaststone>();
                //if (WorldGen.genRand.Next(5) == 0 && z >= Main.maxTilesY - 230 && z <= Main.maxTilesY - 221 && Main.tile[q, z].HasTile) Main.tile[q, z].type = (ushort)ModContent.TileType<Tiles.BlackBlaststone>();
                if ((Main.tile[q, z].TileType == TileID.Ash || Main.tile[q, z].TileType == TileID.Hellstone) &&
                    Main.tile[q, z].HasTile)
                {
                    Main.tile[q, z].TileType = (ushort)ModContent.TileType<BlastedStone>();
                }

                if (z < Main.maxTilesY - 110 && z > Main.maxTilesY - 120)
                {
                    if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                    {
                        if (Main.tile[q, z].TileType != ModContent.TileType<CaesiumOre>())
                        {
                            if (q % 20 == 0)
                            {
                                CaesiumSpike.CreateSpikeUp(q, z,
                                    (ushort)ModContent
                                        .TileType<CaesiumOre>()); // Structures.CaesiumSpike.CreateSpike(q, z);
                                CaesiumSpike.CreateSpikeDown(q, z,
                                    (ushort)ModContent.TileType<CaesiumOre>());
                            }
                        }
                    }
                }

                if (z < Main.maxTilesY - 170 && z > Main.maxTilesY - 180)
                {
                    if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                    {
                        if (Main.tile[q, z].TileType != ModContent.TileType<CaesiumOre>())
                        {
                            if (q % 20 == 0)
                            {
                                CaesiumSpike.CreateSpikeUp(q, z,
                                    (ushort)ModContent.TileType<CaesiumOre>());
                                CaesiumSpike.CreateSpikeDown(q, z,
                                    (ushort)ModContent
                                        .TileType<CaesiumOre>()); // Structures.CaesiumSpike.CreateSpike(q, z);
                            }
                        }
                    }
                }
                //if (z < Main.maxTilesY - 160 && z > Main.maxTilesY - 190)
                //{
                //    if (Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile ||
                //        Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile ||
                //        Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile ||
                //        Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile)
                //    {
                //        if (WorldGen.genRand.Next(40) == 0)
                //        {
                //            Structures.CaesiumSpike.CreateSpike(q, z);
                //        }
                //    }
                //}
            }
        }

        for (int q = Main.maxTilesX - (Main.maxTilesX / 5); q < Main.maxTilesX - 20; q++)
        {
            for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
            {
                if (q % 100 < 33 && z > Main.maxTilesY - 175)
                {
                    if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                        (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                    {
                        if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
                        {
                            Main.tile[q, z].TileType = (ushort)ModContent.TileType<LaziteGrass>();
                        }
                    }
                }
            }
        }
        #endregion
        */
        progress.Message = "Generating Hellcastle and Phantom Overgrowth";
        int hellcastleOriginX = (Main.maxTilesX / 2) - 200;
        int ashenLeft = hellcastleOriginX - 100;
        int ashenRight = hellcastleOriginX + 500;
        // TODO: make impervious brick pillars


        //if (Main.drunkWorld)
        //{
        //    hellcastleOriginX = (Main.maxTilesX / 3) - 210;
        //    ashenLeft = (Main.maxTilesX / 3) - 450;
        //    ashenRight = (Main.maxTilesX / 3) + 500;
        //}

        Hellcastle.GenerateHellcastle(hellcastleOriginX, Main.maxTilesY - 330);
        for (int hbx = ashenLeft; hbx < ashenRight; hbx++)
        {
            for (int hby = Main.maxTilesY - 200; hby < Main.maxTilesY - 50; hby++)
            {
                //if (Main.tile[hbx, hby].HasTile &&
                //    (Main.tile[hbx, hby].TileType == TileID.ObsidianBrick ||
                //     Main.tile[hbx, hby].TileType == TileID.HellstoneBrick))
                //{
                //    Main.tile[hbx, hby].TileType = (ushort)ModContent.TileType<ImperviousBrick>();
                //    Tile t = Main.tile[hbx, hby];
                //    t.HasTile = true;
                //}
                //if (Main.tile[hbx, hby].WallType == WallID.ObsidianBrickUnsafe ||
                //     Main.tile[hbx, hby].WallType == WallID.HellstoneBrickUnsafe)
                //{
                //    Main.tile[hbx, hby].TileType = (ushort)ModContent.TileType<ImperviousBrick>();
                //    Tile t = Main.tile[hbx, hby];
                //    t.HasTile = true;
                //    WorldGen.KillWall(hbx, hby);
                //}
                if ((Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby - 1].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby + 1].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby].HasTile))
                {
                    if (Main.tile[hbx, hby].TileType == TileID.Ash)
                    {
                        SlopeType s = Main.tile[hbx, hby].Slope;
                        Tile t = Main.tile[hbx, hby];
                        t.TileType = (ushort)ModContent.TileType<Ectograss>();
                        t.Slope = s;
                        if (WorldGen.genRand.Next(1) == 0)
                        {
                            WorldGen.GrowTree(hbx, hby - 1);
                        }
                    }
                }
                if (WorldGen.genRand.NextBool(70))
                {
                    WorldGen.OreRunner(hbx, hby, 4, 4, (ushort)ModContent.TileType<BrimstoneBlock>());
                }
            }
        }
    }
}
