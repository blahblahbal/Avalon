using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;
using Avalon.Items.Other;

namespace Avalon.Tiles.Furniture.Tuhrtl;

public class LockedTuhrtlDoor : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.NotReallySolid[Type] = true;
        TileID.Sets.DrawsWalls[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.Origin = new Point16(0, 0);
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(0, 1);
        TileObjectData.addAlternate(0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(0, 2);
        TileObjectData.addAlternate(0);
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(119, 105, 79), this.GetLocalization("MapEntry"));
        //DisableSmartCursor = true;
        AdjTiles = new int[] { TileID.ClosedDoor };
        MinPick = 2000;
        DustType = DustID.Silt;
    }

    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        blockDamaged = false;
        return false;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }

    public override bool RightClick(int i, int j)
    {
        Player player = Main.LocalPlayer;
        Tile tile = Main.tile[i, j];
        Main.mouseRightRelease = false;
        for (int num146 = 0; num146 < player.inventory.Length; num146++)
        {
            if (player.inventory[num146].type == ModContent.ItemType<OutpostKey>() && player.inventory[num146].stack > 0)
            {
                player.inventory[num146].stack--;
                if (player.inventory[num146].stack <= 0)
                {
                    player.inventory[num146] = new Item();
                }
                UnlockDoor(i, j);
                return true;
            }
        }
        return false;
    }
    public static void UnlockDoor(int i, int j)
    {
        int num = j;
        //if (type == ModContent.TileType<LockedTuhrtlDoor>())
        //{
        while (Main.tile[i, num].TileFrameY != 0)
        {
            num--;
            if (Main.tile[i, num].TileFrameY < 0 || num <= 0)
            {
                return;
            }
        }
        //}
        SoundEngine.PlaySound(SoundID.Unlock, new Vector2(i * 16, num * 16 + 16));
        for (int k = num; k <= num + 2; k++)
        {
            Main.tile[i, k].TileType = (ushort)ModContent.TileType<TuhrtlDoorClosed>();
            for (int l = 0; l < 4; l++)
            {
                Dust.NewDust(new Vector2((float)(i * 16), (float)(k * 16)), 16, 16, DustID.Silver, 0f, 0f, 0, default(Color), 1f);
            }
        }
    }
    public override void MouseOver(int i, int j)
    {
        var player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<OutpostKey>();
    }
}
