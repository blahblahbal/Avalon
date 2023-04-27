using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.OrangeDungeon;

public class OrangeDungeonCandle : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleWrapLimit = 36;
        TileObjectData.newTile.CoordinateHeights = new int[] { 20 };
        TileObjectData.newTile.DrawYOffset = -4;
        TileObjectData.addTile(Type);
        DustType = 7;
        Main.tileLighted[Type] = true;
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        AddMapEntry(new Color(253, 221, 3));
        DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.player[Main.myPlayer];
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonCandle>();
    }

    public override bool RightClick(int i, int j)
    {
        WorldGen.KillTile(i, j);
        if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
        {
            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
        }
        return true;
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

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonCandle >());
    }

    public override void HitWire(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int topY = j - tile.TileFrameY / 18;
        short frameAdjustment = (short)(tile.TileFrameX > 0 ? -18 : 18);
        Main.tile[i, topY].TileFrameX += frameAdjustment;
        Wiring.SkipWire(i, topY);
        NetMessage.SendTileSquare(-1, i, topY + 1, 1, TileChangeType.None);
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        var color = new Color(224, 104, 147, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        int width = 18;
        int offsetY = -4;
        int height = 20;
        int offsetX = 1;
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        for (int k = 0; k < 7; k++)
        {
            float x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
            float y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f + x, j * 16 - (int)Main.screenPosition.Y + offsetY + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
}
