using Avalon.Common;
using Avalon.WorldGeneration.Enums;
using Avalon.WorldGeneration.Passes;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration;

public class GenSystem : ModSystem
{
    public static int HellfireItemCount;
    public static int TropicsItemCount;
    public override void PostWorldGen()
    {
        AvalonWorld.JungleLocationX = GenVars.JungleX;
    }
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        GenPass currentPass;

        int index = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));
        if (index != -1)
        {
            tasks.Insert(index + 1, new AvalonReset("Avalon Reset", 1000f));
        }

        //index = tasks.FindIndex(genpass => genpass.Name.Equals("Generate Ice Biome"));
        //if (index != -1)
        //{
        //    tasks.Insert(index + 1, new SkyClouds());
        //}

        int evil = WorldGen.WorldGenParam_Evil;
        if (evil == (int)WorldEvil.Contagion && !Main.zenithWorld)
        {
            index = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption"));
            if (index != -1)
            {
                // Replace corruption task with contagion task
                tasks[index] = new Contagion("Contagion", 80f);
            }
            index = tasks.FindIndex(genpass => genpass.Name.Equals("Altars"));
            if (index != -1)
            {
                tasks[index] = new IckyAltars();
            }
        }

        if (ModContent.GetInstance<AvalonWorld>().WorldJungle == WorldJungle.Tropics)
        {
            int jungleIndex = tasks.FindIndex(i => i.Name.Equals("Wet Jungle"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Wet Tropics", new WorldGenLegacyMethod(Tropics.JunglesWetTask));
            }
            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Mud Caves To Grass"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Loam Caves To Grass", new WorldGenLegacyMethod(Tropics.JunglesGrassTask));
                tasks.Insert(jungleIndex, new PassLegacy("Loam", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration configuration)
                {
                    int tile = ModContent.TileType<Tiles.Tropics.Loam>();
                    for (int i = 0; i < Main.maxTilesX; i++)
                    {
                        for (int j = 0; j < Main.maxTilesY; j++)
                        {
                            if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == TileID.Mud)
                            {
                                Main.tile[i, j].TileType = (ushort)tile;
                            }
                        }
                    }
                })));
            }
            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Jungle Temple"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Tuhrtl Outpost", new WorldGenLegacyMethod(Tropics.TuhrtlOutpostTask));
            }
            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Hives"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Wet Tropics", new WorldGenLegacyMethod(Tropics.WaspNests));
            }

            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Jungle Chests"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Tropics Sanctums", new WorldGenLegacyMethod(Tropics.TropicsSanctumTask));
            }
            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Muds Walls In Jungle"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Loam Walls in Tropics", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration passConfig)
                {
                    progress.Set(1.0);
                    int num171 = 0;
                    int num172 = 0;
                    bool flag4 = false;
                    for (int num173 = 5; num173 < Main.maxTilesX - 5; num173++)
                    {
                        for (int num174 = 0; num174 < Main.worldSurface + 20.0; num174++)
                        {
                            if (Main.tile[num173, num174].HasTile && Main.tile[num173, num174].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>())
                            {
                                num171 = num173;
                                flag4 = true;
                                break;
                            }
                        }

                        if (flag4)
                            break;
                    }

                    flag4 = false;
                    for (int num175 = Main.maxTilesX - 5; num175 > 5; num175--)
                    {
                        for (int num176 = 0; num176 < Main.worldSurface + 20.0; num176++)
                        {
                            if (Main.tile[num175, num176].HasTile && Main.tile[num175, num176].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>())
                            {
                                num172 = num175;
                                flag4 = true;
                                break;
                            }
                        }

                        if (flag4)
                            break;
                    }
                    GenVars.jungleMinX = num171;
                    GenVars.jungleMaxX = num172;
                    for (int num177 = num171; num177 <= num172; num177++)
                    {
                        for (int num178 = 0; (double)num178 < Main.maxTilesY - 200; num178++)
                        {
                            if (((num177 >= num171 + 2 && num177 <= num172 - 2) || !WorldGen.genRand.NextBool(2)) &&
                                ((num177 >= num171 + 3 && num177 <= num172 - 3) || !WorldGen.genRand.NextBool(3)) &&
                                (Main.tile[num177, num178].WallType == WallID.DirtUnsafe || Main.tile[num177, num178].WallType == WallID.Cave6Unsafe ||
                                Main.tile[num177, num178].WallType == WallID.MudUnsafe))
                            {
                                Main.tile[num177, num178].WallType = (ushort)ModContent.WallType<Walls.TropicalMudWall>();
                            }
                        }
                    }
                }));
            }

            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Temple"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Re-solidify Lihzahrd Brick", new WorldGenLegacyMethod(Tropics.LihzahrdBrickReSolidTask));
            }
            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Glowing Mushrooms and Jungle Plants"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Glowing Mushrooms and Tropics Plants", new WorldGenLegacyMethod(Tropics.GlowingMushroomsandJunglePlantsTask));
            }
            jungleIndex = tasks.FindIndex(i => i.Name.Equals("Jungle Plants"));
            if (jungleIndex != -1)
            {
                tasks[jungleIndex] = new PassLegacy("Tropics Plants", new WorldGenLegacyMethod(Tropics.JungleBushesTask));
            }
        }

        index = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

        if (index != -1)
        {
            tasks.Insert(index + 1, new OreGenPreHardmode("Adding Avalon Ores", 237.4298f));
        }

        index = tasks.FindIndex(genpass => genpass.Name.Equals("Weeds"));
        if (index != -1)
        {
            tasks.Insert(index + 1, new ShortGrass("Contagion Weeds", 50f));
            tasks.Insert(index + 2, new ReplaceChestItems());
        }

        int iceWalls = tasks.FindIndex(genPass => genPass.Name == "Cave Walls");
        if (iceWalls != -1)
        {
            currentPass = new Shrines();
            tasks.Insert(iceWalls + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int stalac = tasks.FindIndex(genPass => genPass.Name == "Remove Broken Traps");
        if (stalac != -1)
        {
            currentPass = new GemTreePass();
            tasks.Insert(stalac + 2, currentPass);
            totalWeight += currentPass.Weight;

            currentPass = new GemStashes();
            tasks.Insert(stalac + 2, currentPass);
            totalWeight += currentPass.Weight;

            currentPass = new AvalonStalac();
            tasks.Insert(stalac + 1, currentPass);
            totalWeight += currentPass.Weight;
        }
        int dungeon = tasks.FindIndex(genPass => genPass.Name == "Dungeon");
        if (dungeon != -1)
        {
            currentPass = new MoreDungeonChests();
            tasks.Insert(dungeon + 1, currentPass);
            totalWeight += currentPass.Weight;

            currentPass = new ManaCrystals("Mana Crystals in the Dungeon", 5f);
            tasks.Insert(dungeon + 2, currentPass);
            totalWeight += currentPass.Weight;
        }
        // uncomment when hm update releases
        int underworld = tasks.FindIndex(genPass => genPass.Name == "Micro Biomes");
        if (underworld != -1)
        {
            //currentPass = new Underworld();
            //tasks.Insert(underworld + 1, currentPass);
            //totalWeight += currentPass.Weight;

            //currentPass = new Ectovines();
            //tasks.Insert(underworld + 2, currentPass);
            //totalWeight += currentPass.Weight;

            // uncomment when sky fortress becomes a thing
            //tasks.Insert(underworld + 4, new SkyFortress());

            currentPass = new ReplacePass("Replacing any improper ores", 25f);
            tasks.Insert(underworld + 1, currentPass);
            totalWeight += currentPass.Weight;

            if (ModContent.GetInstance<AvalonWorld>().WorldJungle == WorldJungle.Tropics)
            {
                currentPass = new PassLegacy("Tropics Thing", new WorldGenLegacyMethod(Tropics.LoamWallTask));
                tasks.Insert(index + 2, currentPass);
                totalWeight += currentPass.Weight;
            }
        }

        index = tasks.FindIndex(genPass => genPass.Name == "Vines");
        if (index != -1)
        {
            currentPass = new Hooks.DungeonRemoveCrackedBricks();
            tasks.Insert(index + 1, currentPass);
            totalWeight += currentPass.Weight;
            if (evil == (int)WorldEvil.Contagion && !Main.zenithWorld)
            {
                tasks.Insert(index + 2, new ContagionVines("Contagion Vines", 25f));
            }
            if (ModContent.GetInstance<AvalonWorld>().WorldJungle == WorldJungle.Tropics)
            {
                currentPass = new TropicsVines();
                tasks.Insert(index + 3, currentPass);
                totalWeight += currentPass.Weight;

                currentPass = new PassLegacy("Tropics Traps", new WorldGenLegacyMethod(Tropics.PlatformLeafTrapTask));
                tasks.Insert(index + 4, currentPass);
                totalWeight += currentPass.Weight;
            }
            //currentPass = new CrystalMinesPass();
            //tasks.Insert(index + 4, currentPass);
            //totalWeight += currentPass.Weight;
        }
    }
    public static int GetNextHellfireChestItem()
    {
        int result = ModContent.ItemType<Items.Accessories.PreHardmode.OilBottle>();
        switch (HellfireItemCount % 2)
        {
            case 0:
                result = ModContent.ItemType<Items.Accessories.PreHardmode.OilBottle>();
                break;
            case 1:
                result = ModContent.ItemType<Items.Tools.PreHardmode.EruptionHook>();
                break;
        }

        HellfireItemCount++;
        return result;
    }

    public static int GetNextTropicsChestItem()
    {
        int result = ModContent.ItemType<Items.Accessories.PreHardmode.RubberGloves>();
        switch (TropicsItemCount % 4)
        {
            case 0:
                result = ModContent.ItemType<Items.Accessories.PreHardmode.RubberGloves>();
                break;
            case 1:
                result = ModContent.ItemType<Items.Weapons.Ranged.PreHardmode.Thompson>();
                break;
            case 2:
                result = ModContent.ItemType<Items.Accessories.PreHardmode.AnkletofAcceleration>();
                break;
            case 3:
                result = ItemID.StaffofRegrowth;
                break;
        }

        if (WorldGen.genRand.NextBool(50))
            result = ItemID.Seaweed;
        else if (WorldGen.genRand.NextBool(15))
            result = ItemID.FiberglassFishingPole;
        else if (WorldGen.genRand.NextBool(20))
            result = ItemID.FlowerBoots;

        TropicsItemCount++;
        return result;
    }
}
