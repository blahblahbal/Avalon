using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class LivingLightning : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(196, 142, 238));
        Main.tileLighted[Type] = true;
        DustType = DustID.Bone;
        AnimationFrameHeight = 90;
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 1;
        TileObjectData.newTile.Origin = new Point16(0, 0);
        TileObjectData.newTile.CoordinateHeights = new int[1] { 16 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(CanPlaceAlter, -1, 0, processedCoordinates: true);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(AfterPlacement, -1, 0, processedCoordinates: false);
        TileObjectData.addTile(Type);
        RegisterItemDrop(ModContent.ItemType<LivingLightningBlock>());
    }
    public int CanPlaceAlter(int i, int j, int type, int style, int direction, int alternate)
    {
        return 1;
    }

    public static int AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
    {
        if (Main.netMode == 1)
        {
            NetMessage.SendTileSquare(Main.myPlayer, i, j, 1, 1);
        }
        return 1;
    }
    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        if (++frameCounter > 5)
        {
            frameCounter = 0;
            if (++frame > 2) frame = 0;
        }
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.768f;
        g = 0.556f;
        b = 0.933f;
    }
    public override bool CanPlace(int i, int j)
    {
        List<List<int>> around = new List<List<int>>
        {
            new List<int>
            {
                i,
                j - 1
            },
            new List<int>
            {
                i - 1,
                j
            },
            new List<int>
            {
                i + 1,
                j
            },
            new List<int>
            {
                i,
                j + 1
            }
        };
        if (Main.tile[i, j].WallType != 0)
        {
            return true;
        }
        for (int k = 0; k < around.Count; k++)
        {
            Tile tile = Main.tile[around[k][0], around[k][1]];
            if (tile.HasTile && (Main.tileSolid[tile.TileType] || Data.Sets.Tile.LivingBlocks.Contains(tile.TileType)))
            {
                return true;
            }
        }
        return false;
    }
}
