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

namespace Avalon.Hooks;

internal class Tropics : ModHook
{
    protected override void Apply()
    {
        IL_DesertDescription.RowHasInvalidTiles += IL_DesertDescription_RowHasInvalidTiles;
        
    }

    private void IL_DesertDescription_RowHasInvalidTiles(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.Mud, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.Loam>())[id]);
        Utilities.AddAlternativeIdChecks(il, TileID.JungleGrass, id => TileID.Sets.Factory.CreateBoolSet((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())[id]);
    }


    public static void ILGenPassDirtWallBackgrounds(ILContext il)
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
    }
}
