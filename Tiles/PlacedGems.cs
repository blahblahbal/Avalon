using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Avalon.Tiles;

public class PlacedGems : ModTile
{
    public override void SetStaticDefaults()
    {
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 1;
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
        TileObjectData.newTile.CoordinatePadding = 2;
        //TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 500;
        Main.tileSpelunker[Type] = true;
        AddMapEntry(new Color(86, 239, 255), this.GetLocalization("Opal"));
        AddMapEntry(new Color(61, 56, 65), this.GetLocalization("Onyx"));
        AddMapEntry(new Color(251, 66, 146), this.GetLocalization("Kunzite"));
        AddMapEntry(new Color(22, 212, 198), this.GetLocalization("Tourmaline"));
        AddMapEntry(new Color(0, 237, 14), this.GetLocalization("Peridot"));
        AddMapEntry(new Color(198, 168, 130), this.GetLocalization("Zircon"));
    }

    // selects the map entry depending on the frameX
    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)(Main.tile[i, j].TileFrameX / 18);
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        int toDrop = 0;
        switch (Main.tile[i, j].TileFrameX / 18)
        {
            //case 0:
            //    toDrop = ModContent.ItemType<Items.Material.Opal>();
            //    break;
            //case 1:
            //    toDrop = ModContent.ItemType<Items.Material.Onyx>();
            //    break;
            //case 2:
            //    toDrop = ModContent.ItemType<Items.Material.Kunzite>();
            //    break;
            case 3:
                toDrop = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
                break;
            case 4:
                toDrop = ModContent.ItemType<Items.Material.Ores.Peridot>();
                break;
            case 5:
                toDrop = ModContent.ItemType<Items.Material.Ores.Zircon>();
                break;
        }

        yield return new Item(toDrop);
    }
    //public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    //{
    //    int toDrop = 0;
    //    //Main.NewText("" + Main.tile[i, j].frameX);
    //    switch (Main.tile[i, j].TileFrameX / 18)
    //    {
    //        //case 0:
    //        //    toDrop = ModContent.ItemType<Items.Material.Opal>();
    //        //    break;
    //        //case 1:
    //        //    toDrop = ModContent.ItemType<Items.Material.Onyx>();
    //        //    break;
    //        //case 2:
    //        //    toDrop = ModContent.ItemType<Items.Material.Kunzite>();
    //        //    break;
    //        case 3:
    //            toDrop = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
    //            break;
    //        case 4:
    //            toDrop = ModContent.ItemType<Items.Material.Ores.Peridot>();
    //            break;
    //        case 5:
    //            toDrop = ModContent.ItemType<Items.Material.Ores.Zircon>();
    //            break;
    //    }
    //    if (toDrop > 0) Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, toDrop);
    //}

    // copy from the vanilla tileframe for placed gems
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        var tile = Framing.GetTileSafely(i, j);
        var topTile = Framing.GetTileSafely(i, j - 1);
        var bottomTile = Framing.GetTileSafely(i, j + 1);
        var leftTile = Framing.GetTileSafely(i - 1, j);
        var rightTile = Framing.GetTileSafely(i + 1, j);
        var topType = -1;
        var bottomType = -1;
        var leftType = -1;
        var rightType = -1;
        if (topTile.HasTile && !topTile.BottomSlope)
            bottomType = topTile.TileType;
        if (bottomTile.HasTile && !bottomTile.IsHalfBlock && !bottomTile.TopSlope)
            topType = bottomTile.TileType;
        if (leftTile.HasTile)
            leftType = leftTile.TileType;
        if (rightTile.HasTile)
            rightType = rightTile.TileType;
        var variation = WorldGen.genRand.Next(3) * 18;
        if (topType >= 0 && Main.tileSolid[topType] && !Main.tileSolidTop[topType])
        {
            if (tile.TileFrameY > 0 || tile.TileFrameY < 36)
                tile.TileFrameY = (short)variation;
        }
        else if (leftType >= 0 && Main.tileSolid[leftType] && !Main.tileSolidTop[leftType])
        {
            if (tile.TileFrameY < 108 || tile.TileFrameY > 54)
                tile.TileFrameY = (short)(108 + variation);
        }
        else if (rightType >= 0 && Main.tileSolid[rightType] && !Main.tileSolidTop[rightType])
        {
            if (tile.TileFrameY < 162 || tile.TileFrameY > 198)
                tile.TileFrameY = (short)(162 + variation);
        }
        else if (bottomType >= 0 && Main.tileSolid[bottomType] && !Main.tileSolidTop[bottomType])
        {
            if (tile.TileFrameY < 54 || tile.TileFrameY > 90)
                tile.TileFrameY = (short)(54 + variation);
        }
        else
            WorldGen.KillTile(i, j);
        return true;
    }

    // needed so gems are only allowed to be placed on solid tiles
    public override bool CanPlace(int i, int j)
    {
        return WorldGen.SolidTile(i - 1, j) || WorldGen.SolidTile(i + 1, j) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1);
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        if (Main.tile[i, j].TileFrameY / 18 < 3)
            offsetY = 2;
    }
}
