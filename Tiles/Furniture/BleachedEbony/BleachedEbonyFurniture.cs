using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.BleachedEbony;

public class BleachedEbonyBathtub : BathtubTemplate { }

public class BleachedEbonyBed : BedTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyBed>();
}

public class BleachedEbonyBookcase : BookcaseTemplate { }

public class BleachedEbonyCandelabra : CandelabraTemplate
{
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(224, 104, 147, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        int width = 18;
        int offsetY = 2;
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

public class BleachedEbonyCandle : CandleTemplate 
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyCandle>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(224, 104, 147, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        int width = 18;
        int offsetY = -4;
        int height = 20;
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

public class BleachedEbonyChair : ChairTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyChair>();
}

public class BleachedEbonyChandelier : ChandelierTemplate
{
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
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

public class BleachedEbonyChest : ChestTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyChest>());
        }
    }
}

public class BleachedEbonyClock : ClockTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyClock>();
}

public class BleachedEbonyDoorClosed : ClosedDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyDoor>();
}

public class BleachedEbonyDoorOpen : OpenDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyDoor>();
}

public class BleachedEbonyDresser : DresserTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyDresser>();
}

public class BleachedEbonyLamp : LampTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyLamp>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
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

public class BleachedEbonyLantern : LanternTemplate 
{
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(224, 104, 147, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        int width = 18;
        int offsetY = 0;
        int height = 16;
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

public class BleachedEbonyPiano : PianoTemplate { }

public class BleachedEbonySink : SinkTemplate { }

public class BleachedEbonySofa : SofaTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonySofa>();
}

public class BleachedEbonyTable : TableTemplate { }

public class BleachedEbonyToilet : ToiletTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyToilet>();
}

public class BleachedEbonyWorkBench : WorkbenchTemplate { }

public class BleachedEbonyPlatform : PlatformTemplate 
{
    public override int Dust => DustID.SnowBlock;
}

