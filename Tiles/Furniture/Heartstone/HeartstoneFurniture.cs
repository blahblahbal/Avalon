using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Heartstone;

public class HeartstoneBathtub : BathtubTemplate { }

public class HeartstoneBed : BedTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneBed>();
}

public class HeartstoneBookcase : BookcaseTemplate { }

public class HeartstoneCandelabra : CandelabraTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneCandelabra>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.9f;
            g = 0.5f;
            b = 0.7f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(198, 171, 108, 0);
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

public class HeartstoneCandle : CandleTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneCandle>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.9f;
            g = 0.5f;
            b = 0.7f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(198, 171, 108, 0);
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

public class HeartstoneChair : ChairTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChair>();
}

public class HeartstoneChandelier : ChandelierTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChandelier>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.9f;
            g = 0.5f;
            b = 0.7f;
        }
    }
}
public class HeartstoneLantern : LanternTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneLantern>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.9f;
            g = 0.5f;
            b = 0.7f;
        }
    }
}
public class HeartstoneChest : ChestTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChest>());
        }
    }
}

public class HeartstoneClock : ClockTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneClock>();
}

public class HeartstoneDoorClosed : ClosedDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneDoor>();
}

public class HeartstoneDoorOpen : OpenDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneDoor>();
}

public class HeartstoneDresser : DresserTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneDresser>();
}

public class HeartstoneLamp : LampTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneLamp>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.9f;
            g = 0.5f;
            b = 0.7f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(198, 171, 108, 0);
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

public class HeartstonePiano : PianoTemplate { }

public class HeartstonePlatform : PlatformTemplate
{
    public override int Dust => ModContent.DustType<Dusts.HeartstoneDust>();
}

public class HeartstoneSink : SinkTemplate { }

public class HeartstoneSofa : SofaTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneSofa>();
}

public class HeartstoneTable : TableTemplate { }

//public class HeartstoneToilet : ToiletTemplate
//{
//    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneToilet>();
//}

public class HeartstoneWorkbench : WorkbenchTemplate { }
