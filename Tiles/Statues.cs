using System;
using System.Collections.Generic;
using System.Linq;
using Avalon.Items.Placeable.Statue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.WorldBuilding;

namespace Avalon.Tiles;

public class Statues : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleWrapLimit = 55;
        TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
        TileObjectData.addAlternate(165);
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(144, 148, 144), Language.GetText("MapObject.Statue"));
        AddMapEntry(new Color(175, 216, 235));
        AddMapEntry(new Color(201, 188, 170), Language.GetText("MapObject.Vase"));
        AddMapEntry(new Color(13, 47, 84));
        DustType = DustID.Stone;
        TileID.Sets.DisableSmartCursor[Type] = true;
    }

    public override ushort GetMapOption(int i, int j)
    {
        switch (Main.tile[i, j].TileFrameX / 36)
        {
            case 2:
            case 9:
            case 10:
                return 1;
            case 3:
            case 13:
			case 14:
                return 2;
            case 6:
                return 3;
        }
        return 0;
    }
    public override void HitWire(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int left = i;
        int top = j;
        while (tile.TileFrameX % 36 != 0)
        {
            left--;
            if (Main.tile[left, j].TileFrameX % 36 == 0)
            {
                break;
            }
        }
        while (tile.TileFrameY != 0 || tile.TileFrameY != 162)
        {
            top--;
            if (Main.tile[left, top].TileFrameY == 0 || Main.tile[left, top].TileFrameY == 162)
            {
                break;
            }
        }
        ClassExtensions.SkipWireMulti(left, top, 2, 3);
        for (int q = left; q < left + 1; q++)
        {
            for (int z = top; z < top + 2; z++)
            {
                if (Main.tile[q, z].TileFrameX >= 432 && Main.tile[q, z].TileFrameX <= 466)
                {
                    Main.tile[q, z].TileFrameX -= 36;
                    WorldGen.SquareTileFrame(q, z);
                }
                else if (Main.tile[q, z].TileFrameX >= 396 && Main.tile[q, z].TileFrameX <= 430)
                {
                    Main.tile[q, z].TileFrameX += 36;
                    WorldGen.SquareTileFrame(q, z);
                }
                
            }
        }
    }
    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.tile[i, j].TileFrameX >= 432 && Main.tile[i, j].TileFrameX <= 466)
        {
            Lighting.AddLight(new Vector2(i * 16, j * 16), new Vector3(1f, 0.1f, 0.1f));
        }
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        int item = 0;
        int num9 = (Main.tile[i, j].TileFrameX / 36);
        int num10 = (Main.tile[i, j].TileFrameY / 54);
        num10 %= 3;
        num9 += num10 * 55;
        switch (num9)
        {
            case 0:
                item = ModContent.ItemType<ShellStatue>();
                break;
            case 1:
                item = ModContent.ItemType<DNAStatue>();
                break;
            case 2:
                item = ModContent.ItemType<IceSculpture>();
                break;
            case 3:
                item = ModContent.ItemType<OrangeDungeonVase>();
                break;
            case 4:
                item = ModContent.ItemType<TomeStatue>();
                break;
            case 5:
                item = ModContent.ItemType<HallowStatue>();
                break;
            case 6:
                item = ModContent.ItemType<BlueLihzahrdStatue>();
                break;
            case 7:
                item = ModContent.ItemType<ContagionStatue>();
                break;
            case 8:
                item = ModContent.ItemType<CrimsonStatue>();
                break;
            case 9:
                item = ModContent.ItemType<AngelSculpture>();
                break;
            case 10:
                item = ModContent.ItemType<DNASculpture>();
                break;
            case 11:
            case 12:
                item = ModContent.ItemType<TurretStatue>();
                break;
            case 13:
                item = ModContent.ItemType<PurpleDungeonVase>();
                break;
            case 14:
                item = ModContent.ItemType<YellowDungeonVase>();
                break;
        }

        yield return new Item(item);
    }

    public override bool CreateDust(int i, int j, ref int type)
    {
        int num9 = (Main.tile[i, j].TileFrameX / 36);
        int num10 = (Main.tile[i, j].TileFrameY / 54);
        num10 %= 3;
        num9 += num10 * 55;
        switch (num9)
        {
            case 2:
            case 9:
            case 10:
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Ice);
                return false;
            case 3:
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.OrangeDungeonDust>());
                return false;
            case 6:
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.t_Granite);
                return false;
            case 13:
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.PurpleDungeonDust>());
                return false;
        }
        return base.CreateDust(i, j, ref type);
    }
}

public class ExampleStatueModWorld : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int ResetIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));
        if (ResetIndex != -1)
        {
            tasks.Insert(ResetIndex + 1, new PassLegacy("Avalon Statue Setup", delegate (GenerationProgress progress, GameConfiguration config)
            {
                progress.Message = "Adding Avalon Statues";

                // Not necessary, just a precaution.
                if (GenVars.statueList.Any(point => point.X == ModContent.TileType<Statues>()))
                {
                    return;
                }
                // Make space in the statueList array, and then add a Point16 with (TileID, PlaceStyle)
                Array.Resize(ref GenVars.statueList, GenVars.statueList.Length + 10);
                GenVars.statueList[GenVars.statueList.Length - 10] = new Point16(ModContent.TileType<Statues>(), 0);
                GenVars.statueList[GenVars.statueList.Length - 9] = new Point16(ModContent.TileType<Statues>(), 1);
                GenVars.statueList[GenVars.statueList.Length - 8] = new Point16(ModContent.TileType<Statues>(), 2);
                GenVars.statueList[GenVars.statueList.Length - 7] = new Point16(ModContent.TileType<Statues>(), 4);
                GenVars.statueList[GenVars.statueList.Length - 6] = new Point16(ModContent.TileType<Statues>(), 5);
                GenVars.statueList[GenVars.statueList.Length - 5] = new Point16(ModContent.TileType<Statues>(), 7);
                GenVars.statueList[GenVars.statueList.Length - 4] = new Point16(ModContent.TileType<Statues>(), 8);
                GenVars.statueList[GenVars.statueList.Length - 3] = new Point16(ModContent.TileType<Statues>(), 9);
                GenVars.statueList[GenVars.statueList.Length - 2] = new Point16(ModContent.TileType<Statues>(), 10);
                GenVars.statueList[GenVars.statueList.Length - 1] = new Point16(ModContent.TileType<Statues>(), 11);
                //for (int i = WorldGen.statueList.Length - 9; i < WorldGen.statueList.Length; i++)
                //{
                //    if (i == )
                //    WorldGen.statueList[i] = new Point16(ModContent.TileType<Statues>(), 0);
                //}
            }));
        }
    }
}
