using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class ChunkstoneColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(49, 56, 41));
        //ItemDrop = ModContent.ItemType<Items.Placeable.Beam.ChunkstoneColumn>();
        
        //TileObjectData.newTile.Width = 1;
        //TileObjectData.newTile.Height = 1;
        //TileObjectData.newTile.Origin = new Point16(0, 0);
        //TileObjectData.newTile.CoordinateHeights = new int[1] { 16 };
        //TileObjectData.newTile.CoordinateWidth = 16;
        //TileObjectData.newTile.CoordinatePadding = 2;
        //TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(CanPlaceAlter, -1, 0, processedCoordinates: true);
        //TileObjectData.newTile.UsesCustomCanPlace = true;
        //TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(AfterPlacement, -1, 0, processedCoordinates: false);
        //TileObjectData.addTile(Type);
        TileID.Sets.IsBeam[Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        height = 18;
    }

    //public override IEnumerable<Item> GetItemDrops(int i, int j)
    //{
    //    yield return new Item(ModContent.ItemType<Items.Placeable.Beam.ChunkstoneColumn>());
    //}

    //private int AfterPlacement(int i, int j, int type, int style, int direction, int arg6)
    //{
    //    if (Main.netMode == NetmodeID.MultiplayerClient)
    //    {
    //        NetMessage.SendTileSquare(Main.myPlayer, i, j, 1, 1);
    //    }
    //    return 1;
    //}

    //public override bool CanPlace(int i, int j)
    //{
    //    return (Main.tile[i, j - 1].HasTile || Main.tile[i, j + 1].HasTile || Main.tile[i, j].WallType != 0 && !Main.tile[i, j].HasTile);
    //}
    //public int CanPlaceAlter(int i, int j, int type, int style, int direction, int throwaway)
    //{
    //    return 1;
    //}
}
