using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.CrystalMines;

public class GiantCrystalShard : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = false;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.CoordinateHeights = new[] {16, 16};
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(85, 37, 134));
        HitSound = SoundID.Item27;
    }

    //public override void NumDust(int i, int j, bool fail, ref int num)
    //{
    //    if (Main.tile[i, j].frameY == 0) num = DustID.PurpleCrystalShard;
    //    else if (Main.tile[i, j].frameY == 36) num = DustID.BlueCrystalShard;
    //    else if (Main.tile[i, j].frameY == 72) num = DustID.PinkCrystalShard;
    //}
    public override bool CreateDust(int i, int j, ref int type)
    {
        switch (Main.tile[i, j].TileFrameY / 36)
        {
            case 0:
                type = DustID.PurpleCrystalShard;
                break;
            case 1:
                type = DustID.BlueCrystalShard;
                break;
            case 2:
                type = DustID.PinkCrystalShard;
                break;
        }

        return true;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 16,
            ModContent.ItemType<Items.Placeable.Tile.GiantCrystalShard>());
    }

    public override void PlaceInWorld(int i, int j, Item item)
    {
        short f = (short)(Main.rand.Next(3) * 36);
        Main.tile[i - 1, j - 1].TileFrameY = f;
        Main.tile[i, j - 1].TileFrameY = f;
        Main.tile[i - 1, j].TileFrameY = (short)(f + 18);
        Main.tile[i, j].TileFrameY = (short)(f + 18);
    }

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.tile[i, j].TileFrameY < 36)
        {
            Lighting.AddLight(new Vector2(i * 16, j * 16), 83 / 255, 38 / 255, 131 / 255);
        }
        else if (Main.tile[i, j].TileFrameY < 72)
        {
            Lighting.AddLight(new Vector2(i * 16, j * 16), 0, 74 / 255, 122 / 255);
        }
        else
        {
            Lighting.AddLight(new Vector2(i * 16, j * 16), 152 / 255, 12 / 255, 121 / 255);
        }
    }
}
