using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.WildMushroom;

public class WildMushroomBathtub : BathtubTemplate
{
}

public class WildMushroomBed : BedTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomBed>();
}

public class WildMushroomBookcase : BookcaseTemplate
{
}

//public class WildMushroomCandelabra : CandelabraTemplate
//{
//    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
//    {
//        Tile tile = Main.tile[i, j];
//        if (tile.TileFrameX == 0)
//        {
//            r = 1f / 1.5f;
//            g = 0.95f / 1.75f;
//            b = 0.65f / 1.75f;
//        }
//    }

//    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
//    {
//        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
//        Color color = new Color(198, 171, 108, 0);
//        int frameX = Main.tile[i, j].TileFrameX;
//        int frameY = Main.tile[i, j].TileFrameY;
//        int width = 18;
//        int offsetY = 2;
//        int height = 18;
//        int offsetX = 1;
//        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
//        if (Main.drawToScreen)
//        {
//            zero = Vector2.Zero;
//        }
//        for (int k = 0; k < 7; k++)
//        {
//            float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
//            float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
//            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
//        }
//    }
//}

//public class WildMushroomCandle : CandleTemplate
//{
//    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomCandle>();
//    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
//    {
//        Tile tile = Main.tile[i, j];
//        if (tile.TileFrameX == 0)
//        {
//            r = 1f / 1.5f;
//            g = 0.95f / 1.75f;
//            b = 0.65f / 1.75f;
//        }
//    }

//    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
//    {
//        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
//        Color color = new Color(198, 171, 108, 0);
//        int frameX = Main.tile[i, j].TileFrameX;
//        int frameY = Main.tile[i, j].TileFrameY;
//        int width = 18;
//        int offsetY = -4;
//        int height = 20;
//        int offsetX = 1;
//        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
//        if (Main.drawToScreen)
//        {
//            zero = Vector2.Zero;
//        }
//        for (int k = 0; k < 7; k++)
//        {
//            float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
//            float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
//            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
//        }
//    }
//}

public class WildMushroomChair : ChairTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomChair>();
}
public class WildMushroomClock : ClockTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomClock>();
}

public class WildMushroomChandelier : ChandelierTemplate
{
    public override Color FlameColor => base.FlameColor;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomChandelier>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f / 1.5f;
            g = 0.95f / 1.75f;
            b = 0.65f / 1.75f;
        }
    }
}

/*


public class WildMushroomChest : ChestTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomChest>());
        }
    }
}



public class WildMushroomDoorClosed : ClosedDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomDoor>();
}

public class WildMushroomDoorOpen : OpenDoorTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomDoor>();
}

public class WildMushroomDresser : DresserTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomDresser>();
}
*/
public class WildMushroomLamp : LampTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomLamp>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f / 1.5f;
            g = 0.95f / 1.75f;
            b = 0.65f / 1.75f;
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
/*
public class WildMushroomLantern : LanternTemplate
{
    public override Color FlameColor => base.FlameColor;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomLantern>();
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f / 1.5f;
            g = 0.95f / 1.75f;
            b = 0.65f / 1.75f;
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



public class WildMushroomPlatform : PlatformTemplate
{
    public override int Dust => ModContent.DustType<WildMushroomDust>();
}


*/

public class WildMushroomPiano : PianoTemplate
{
}

public class WildMushroomSink : SinkTemplate
{
}
public class WildMushroomSofa : SofaTemplate
{
    public override float SittingHeight => 2;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomSofa>();
}

public class WildMushroomTable : TableTemplate
{
}

public class WildMushroomToilet : ToiletTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomToilet>();
}

public class WildMushroomWorkBench : WorkbenchTemplate
{
}
