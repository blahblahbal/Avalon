using Avalon.Common;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Biomes.Desert;
using Mono.Cecil.Cil;
using Terraria;
using System.Reflection;
using Avalon.Tiles.Savanna;
using Avalon.Tiles.Contagion;

namespace Avalon.Hooks;

internal class Tropics : ModHook
{
    protected override void Apply()
    {
        IL_DesertDescription.RowHasInvalidTiles += IL_DesertDescription_RowHasInvalidTiles;
        IL_WorldGen.PlaceJunglePlant += IL_WorldGen_PlaceJunglePlant;
        IL_WorldGen.GenerateWorld += IL_WorldGen_GenerateWorld;
        IL_Liquid.DelWater += IL_Liquid_DelWater;
        On_Liquid.DelWater += On_Liquid_DelWater;
        IL_WorldGen.OreRunner += IL_WorldGen_OreRunner;
        On_WorldGen.Place3x2 += On_WorldGen_Place3x2;
    }

    private void On_WorldGen_Place3x2(On_WorldGen.orig_Place3x2 orig, int x, int y, ushort type, int style)
    {
        if (type is TileID.LargePiles2 or TileID.LargePiles && Main.tile[x, y + 1].TileType == ModContent.TileType<TuhrtlBrick>())
        {
            type = (ushort)ModContent.TileType<TuhrtlBackgroundBlobs>();
            style = WorldGen.genRand.Next(3);
        }
        orig.Invoke(x, y, type, style);
    }

    private void IL_WorldGen_OreRunner(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<SavannaGrass>())[id]);
    }

    private void On_Liquid_DelWater(On_Liquid.orig_DelWater orig, int l)
    {
        orig.Invoke(l);

        int num = Main.liquid[l].x;
        int num2 = Main.liquid[l].y;
        Tile tile4 = Main.tile[num, num2];

        if (tile4.LiquidType == LiquidID.Lava)
        {
            Liquid.LavaCheck(num, num2);
            for (int i = num - 1; i <= num + 1; i++)
            {
                for (int j = num2 - 1; j <= num2 + 1; j++)
                {
                    Tile tile5 = Main.tile[i, j];
                    if (!tile5.HasTile)
                        continue;

                    if (tile5.TileType == ModContent.TileType<SavannaGrass>())
                    {
                        tile5.TileType = (ushort)ModContent.TileType<Loam>();
                        WorldGen.SquareTileFrame(i, j);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendTileSquare(-1, num, num2, 3);
                    }
                }
            }
        }
    }

    private void IL_Liquid_DelWater(ILContext il)
    {
        //Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.CorruptJungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<ContagionJungleGrass>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.Grass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Ickgrass>())[id]);
    }

    private void IL_WorldGen_GenerateWorld(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Savanna.SavannaGrass>())[id]);
        Utilities.AddAlternativeIdChecks(il, WallID.LihzahrdBrickUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>())[id]);
    }

    private void IL_WorldGen_PlaceJunglePlant(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Savanna.SavannaGrass>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.JunglePlants, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Savanna.SavannaShortGrass>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.JunglePlants2, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Savanna.SavannaLongGrass>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.PlantDetritus, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Savanna.SavannaBushes>())[id]);
    }

    private void IL_DesertDescription_RowHasInvalidTiles(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.Mud, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Savanna.Loam>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Savanna.SavannaGrass>())[id]);
    }


    /*public static void ILGenPassDirtWallBackgrounds(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, WallID.JungleUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalGrassWall>())[id]);
    }
    public static void ILGenPassJungle(ILContext il)
    {
        //Utilities.AddAlternativeIdChecks(il, TileID.Mud, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.TropicalMud>());
        ReplaceIDIfTropics(il, TileID.Mud, (ushort)ModContent.TileType<Tiles.Tropics.Loam>());

        Utilities.AddAlternativeIdChecks(il, WallID.JungleUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalGrassWall>())[id]);
        Utilities.AddAlternativeIdChecks(il, WallID.MudUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalMudWall>())[id]);
    }
    public static void ILGrowUndergroundTree(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())[id]);
    }
    public static void ILTileRunner(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.Mud, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.Loam>())[id]);

        //Utilities.AddAlternativeIdChecks(il, WallID.JungleUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalGrassWall>());
        ReplaceIDIfTropics(il, WallID.JungleUnsafe, (ushort)ModContent.WallType<Walls.TropicalGrassWall>());

        //Utilities.AddAlternativeIdChecks(il, WallID.MudUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalMudWall>());
        ReplaceIDIfTropics(il, WallID.MudUnsafe, (ushort)ModContent.WallType<Walls.TropicalMudWall>());
    }
    public static void ILCleanUpDirt(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, WallID.JungleUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalGrassWall>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.Crimsand, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Contagion.Snotsand>())[id]);
    }
    public static void ILWetJungle(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())[id]);
    }
    public static void ILJunglePlants(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())[id]);
    }
    public static void ILMudCavesToGrass(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())[id]);
    }
    public static void ILMudWallsInJungle(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.Mud, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.Loam>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())[id]);
        ReplaceIDIfTropics(il, WallID.MudUnsafe, (ushort)ModContent.WallType<Walls.TropicalMudWall>());
    }
    public static void ILWallVariety(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.Mud, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.Loam>())[id]);
        ReplaceIDIfTropics(il, TileID.JungleGrass, (ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>());
    }
    public static void ILIceWalls(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, WallID.JungleUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalGrassWall>())[id]);
        ReplaceIDIfTropics(il, WallID.MudUnsafe, (ushort)ModContent.WallType<Walls.TropicalMudWall>());
    }
    public static void ILGrassWall(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, WallID.MudUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TropicalMudWall>())[id]);
    }
    public static void ILPots(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.LihzahrdBrick, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TuhrtlBrick>())[id]);
    }
    public static void ILPiles(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, WallID.LihzahrdBrickUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.LihzahrdBrick, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TuhrtlBrick>())[id]);
    }
    public static void ILSpreadWall(ILContext il)
    {
        Instruction loadWall = Instruction.Create(OpCodes.Ldloc_0);
        Instruction loadWallType = Instruction.Create(OpCodes.Ldarg_2);

        Utilities.SoftReplaceAllMatchingInstructions(il, loadWall, loadWallType);
    }
    public static void ILSpreadWall2(ILContext il)
    {
        Instruction loadB = Instruction.Create(OpCodes.Ldloc_0);
        Instruction loadWallType = Instruction.Create(OpCodes.Ldarg_2);

        Utilities.SoftReplaceAllMatchingInstructions(il, loadB, loadWallType);
    }
    public static void ILHitWireSingle(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, WallID.LihzahrdBrickUnsafe, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>())[id]);
    }
    //public static void OnSpreadGrass(On_WorldGen.orig_SpreadGrass orig, int i, int j, int dirt, int grass, bool repeat, TileColorCache color)
    //{
    //    if (AvalonWorld.jungleMenuSelection == AvalonWorld.JungleVariant.tropics)
    //    {
    //        if (dirt == TileID.Mud && grass != TileID.MushroomGrass)
    //        {
    //            dirt = ModContent.TileType<Tiles.Tropics.Loam>();
    //        }
    //        if (grass == TileID.JungleGrass)
    //        {
    //            grass = ModContent.TileType<Tiles.Tropics.TropicalGrass>();
    //        }
    //    }
    //    orig.Invoke(i, j, dirt, grass, repeat, color);
    //}


    private static void ReplaceIDIfTropics(ILContext il, ushort val1, ushort val2)
    {
        //Utilities.ReplaceIDIfMatch(il, val1, val2, typeof(AvalonWorld).GetField("jungleMenuSelection", BindingFlags.Public | BindingFlags.Static), (int)AvalonWorld.JungleVariant.tropics);
    }*/
}
