using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.PurpleDungeon;

public class PurpleDungeonCandelabra : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.StyleHorizontal = true;
        //TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.StyleWrapLimit = 36;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
        TileObjectData.addTile(Type);
        DustType = 7;
        Main.tileLighted[Type] = true;
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        AddMapEntry(new Color(253, 221, 3));
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.9f;
            g = 0.45f;
            b = 0.6f;
        }
    }

    public override void HitWire(int i, int j)
    {
        int x = i - Main.tile[i, j].TileFrameX / 18 % 2;
        int y = j - Main.tile[i, j].TileFrameY / 18 % 2;
        for (int l = x; l < x + 2; l++)
        {
            for (int m = y; m < y + 2; m++)
            {
                if (Main.tile[l, m].HasTile && Main.tile[l, m].TileType == Type)
                {
                    if (Main.tile[l, m].TileFrameX < 36)
                    {
                        Main.tile[l, m].TileFrameX += 36;
                    }
                    else
                    {
                        Main.tile[l, m].TileFrameX -= 36;
                    }
                }
            }
        }
        if (Wiring.running)
        {
            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
        }
        NetMessage.SendTileSquare(-1, x, y + 1, 2);
    }
}
