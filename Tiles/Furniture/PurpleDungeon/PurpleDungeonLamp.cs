using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.PurpleDungeon;

public class PurpleDungeonLamp : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleWrapLimit = 36;
        TileObjectData.addTile(Type);
        Main.tileLighted[Type] = true;
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        AddMapEntry(new Color(253, 221, 3), Language.GetText("MapObject.FloorLamp"));
        DustType = -1;
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonLamp>());
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
        Tile tile = Main.tile[i, j];
        int topY = j - tile.TileFrameY / 18 % 3;
        short frameAdjustment = (short)(tile.TileFrameX > 0 ? -18 : 18);
        Main.tile[i, topY].TileFrameX += frameAdjustment;
        Main.tile[i, topY + 1].TileFrameX += frameAdjustment;
        Main.tile[i, topY + 2].TileFrameX += frameAdjustment;
        Wiring.SkipWire(i, topY);
        Wiring.SkipWire(i, topY + 1);
        Wiring.SkipWire(i, topY + 2);
        NetMessage.SendTileSquare(-1, i, topY + 1, 3, TileChangeType.None);
    }
    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(224, 104, 147, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        int width = 18;
        int offsetY = 0;
        int height = 18;
        int offsetX = 1;
        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        for (int k = 0; k < 7; k++)
        {
            float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
            float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
}
