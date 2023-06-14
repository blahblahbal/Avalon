using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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
        Main.tileLighted[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1200;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.CoordinateHeights = new[] {16, 16};
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(14, 100, 91), CreateMapEntryName());
        HitSound = SoundID.Item27;
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Tile.GiantCrystalShard>());
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
                type = DustID.IceTorch;
                break;
            case 1:
                type = DustID.GreenTorch;
                break;
            case 2:
                type = DustID.BlueTorch;
                break;
        }
        return true;
    }

    public override void PlaceInWorld(int i, int j, Item item)
    {
        short f = (short)(Main.rand.Next(3) * 36);
        Main.tile[i, j - 1].TileFrameY = f;
        Main.tile[i + 1, j - 1].TileFrameY = f;
        Main.tile[i, j].TileFrameY = (short)(f + 18);
        Main.tile[i + 1, j].TileFrameY = (short)(f + 18);
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        if (Main.tile[i, j].TileFrameY < 36)
        {
            r = 0;
            g = 74 / 255f;
            b = 122 / 255f;
        }
        else if (Main.tile[i, j].TileFrameY < 72)
        {
            r = 0;
            g = 140 / 255f;
            b = 56 / 255f;
        }
        else
        {
            r = 30 / 255f;
            g = 12 / 255f;
            b = 140 / 255f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }

        Vector2 pos = new Vector2(i * 16, j * 16 + 2) + zero - Main.screenPosition;
        var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/CrystalMines/GiantCrystalShard").Value, pos, frame, new Color(175, 175, 175, 175));
    }
}
