using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.YellowDungeon;

public class YellowDungeonBathtub : BathtubTemplate { }

public class YellowDungeonBed : BedTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonBed>();
}

public class YellowDungeonBookcase : BookcaseTemplate { }

public class YellowDungeonCandelabra : CandelabraTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonCandelabra>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.84f;
            g = 0.46f;
            b = 0.46f;
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

public class YellowDungeonCandle : CandleTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonCandle>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.84f;
            g = 0.46f;
            b = 0.46f;
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

public class YellowDungeonChair : ChairTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonChair>();
}

public class YellowDungeonChandelier : ChandelierTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonChandelier>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.84f;
            g = 0.46f;
            b = 0.46f;
        }
    }
}

public class YellowDungeonChest : ChestTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonChest>());
        }
    }
}

public class YellowDungeonClock : ClockTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonClock>();
}

public class YellowDungeonDoorClosed : ClosedDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonDoor>();
}

public class YellowDungeonDoorOpen : OpenDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonDoor>();
}

public class YellowDungeonDresser : DresserTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonDresser>();
}

public class YellowDungeonLamp : LampTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonLamp>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.84f;
            g = 0.46f;
            b = 0.46f;
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

public class YellowDungeonPiano : PianoTemplate { }

public class YellowBrickPlatform : PlatformTemplate
{
    public override int Dust => ModContent.DustType<Dusts.YellowDungeonDust>();
}

public class YellowDungeonSink : SinkTemplate { }

public class YellowDungeonSofa : SofaTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonSofa>();
}

public class YellowDungeonTable : TableTemplate { }

public class YellowDungeonToilet : ToiletTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.YellowDungeon.YellowDungeonToilet>();
}

public class YellowDungeonWorkbench : WorkbenchTemplate { }
