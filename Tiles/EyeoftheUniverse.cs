using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class EyeoftheUniverse : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 12;
        TileObjectData.newTile.Height = 9;
        TileObjectData.newTile.CoordinateHeights = new int[]
        {
            16, 16, 16, 16, 16, 16, 16, 16, 16
        };
        TileObjectData.newTile.AnchorWall = true;
        TileObjectData.addTile(Type);
        DustType = 7;
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(120, 85, 60));
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }

        Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
        var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/EyeoftheUniverse_Glow").Value, pos, frame,
            Color.White);
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        int item = 0;
        switch (frameY / 160)
        {
            case 0: item = ModContent.ItemType<Items.Placeable.Painting.EyeoftheUniverse>(); break;
            case 1: item = ModContent.ItemType<Items.Placeable.Painting.BlueEyeoftheUniverse>(); break;
        }
        if (item > 0)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 48, item);
        }
    }
}
