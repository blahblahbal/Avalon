using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;
using static Terraria.GameContent.Drawing.TileDrawing;

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
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
		if (tile.TileFrameX <= 36)
		{
            r = 0.9f;
            g = 0.5f;
            b = 0.7f;
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(85, 85, 85, 0);
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
            Main.spriteBatch.Draw(flameTexture.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
}

public class HeartstoneCandle : CandleTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneCandle>();
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
	{
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
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
        Color color = new Color(85, 85, 85, 0);
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
            Main.spriteBatch.Draw(flameTexture.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
}

public class HeartstoneChair : ChairTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChair>();
}

public class HeartstoneChandelier : ChandelierTemplate
{
    public override Color FlameColor => new Color(85, 85, 85, 0);
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneChandelier>();
    public List<Point> Coordinates = new List<Point>();
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Coordinates = new();
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Point p = new(i, j);
        if (Coordinates.Contains(p)) Coordinates.Remove(p);
    }
    public void DrawMultiTileVines(int i, int j, SpriteBatch spriteBatch)
    {
        Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
        Vector2 zero = Vector2.Zero;
        int sizeX = 1;
        int sizeY = 1;
        Tile tile = Main.tile[i, j];
        if (tile != null && tile.HasTile)
        {
            sizeX = 3;
            sizeY = 3;
            DrawMultiTileVinesInWind(unscaledPosition, zero, i, j, sizeX, sizeY, spriteBatch);
        }
    }

    private void DrawMultiTileVinesInWind(Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY, SpriteBatch spriteBatch)
    {
        double _sunflowerWindCounter = (double)typeof(TileDrawing).GetField("_sunflowerWindCounter", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        UnifiedRandom _rand = (UnifiedRandom)typeof(TileDrawing).GetField("_rand", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        bool _isActiveAndNotPaused = (bool)typeof(TileDrawing).GetField("_isActiveAndNotPaused", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);

        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)topLeftY << 32 | (long)((ulong)topLeftX));

        float windCycle = (float)typeof(TileDrawing).GetMethod("GetWindCycle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { topLeftX, topLeftY, _sunflowerWindCounter });
        float num = windCycle;
        int totalPushTime = 60;
        float pushForcePerFrame = 1.26f;
        float highestWindGridPushComplex = (float)typeof(TileDrawing).GetMethod("GetHighestWindGridPushComplex", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { topLeftX, topLeftY, sizeX, sizeY, totalPushTime, pushForcePerFrame, 3, true });
        windCycle += highestWindGridPushComplex;
        new Vector2(sizeX * 16 * 0.5f, 0f);
        Vector2 vector = new Vector2(topLeftX * 16 - (int)screenPosition.X + sizeX * 16f * 0.5f, topLeftY * 16 - (int)screenPosition.Y) + offSet;
        float num2 = 0.07f;
        Tile tile = Main.tile[topLeftX, topLeftY];
        int type = tile.TileType;
        Vector2 vector2 = new(0f, -2f);
        vector += vector2;
        Texture2D texture2D = null;
        Color color = Color.Transparent;
        float? num3 = null;
        float num4 = 1f;
        float num5 = -4f;
        bool flag2 = false;
        num2 = 0.15f;
        int PositioningFix = 192;
        num3 = 1f;
        num5 = 0f;
        if (flag2)
        {
            vector += new Vector2(0f, 16f);
        }
        num2 *= -1f;
        if (!WorldGen.InAPlaceWithWind(topLeftX, topLeftY, sizeX, sizeY))
        {
            windCycle -= num;
        }
        Vector2 vector4 = default(Vector2);
        Rectangle rectangle = default(Rectangle);
        for (int i = topLeftX; i < topLeftX + sizeX; i++)
        {
            for (int j = topLeftY; j < topLeftY + sizeY; j++)
            {
                Tile tile2 = Main.tile[i, j];
                ushort type2 = tile2.TileType;
                if (type2 != type || !IsVisible(tile2))
                {
                    return;
                }
                Math.Abs((i - topLeftX + 0.5f) / sizeX - 0.5f);
                short tileFrameX = tile2.TileFrameX;
                short tileFrameY = tile2.TileFrameY;
                float num7 = (j - topLeftY + 1) / (float)sizeY;
                if (num7 == 0f)
                {
                    num7 = 0.1f;
                }
                if (num3.HasValue)
                {
                    num7 = num3.Value;
                }
                if (flag2 && j == topLeftY)
                {
                    num7 = 0f;
                }
                Main.instance.TilesRenderer.GetTileDrawData(i, j, tile2, type2, ref tileFrameX, ref tileFrameY, out var tileWidth, out var tileHeight, out var tileTop, out var halfBrickHeight, out var addFrX, out var addFrY, out var tileSpriteEffect, out var _, out var _, out var _);
                bool flag3 = _rand.Next(4) == 0;
                Color tileLight = Lighting.GetColor(i, j);
                typeof(TileDrawing).GetMethod("DrawAnimatedTile_AdjustForVisionChangers", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { i, j, tile2, type2, tileFrameX, tileFrameY, tileLight, flag3 });
                tileLight = (Color)typeof(TileDrawing).GetMethod("DrawTiles_GetLightOverride", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { j, i, tile2, type2, tileFrameX, tileFrameY, tileLight });
                if (_isActiveAndNotPaused && flag3)
                {
                    typeof(TileDrawing).GetMethod("DrawTiles_EmitParticles", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { j, i, tile2, type2, tileFrameX, tileFrameY, tileLight });
                }
                Vector2 vector3 = new Vector2(i * 16 - (int)screenPosition.X, j * 16 - (int)screenPosition.Y + tileTop) + offSet;
                vector3 += vector2;
                vector4 = new(windCycle * num4, Math.Abs(windCycle) * num5 * num7);
                Vector2 vector5 = vector - vector3;
                Texture2D tileDrawTexture = Main.instance.TilesRenderer.GetTileDrawTexture(tile2, i, j);
                if (tileDrawTexture != null)
                {

                    Vector2 vector6 = vector + new Vector2(0f, vector4.Y);
                    rectangle = new(tileFrameX + addFrX, tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight);
                    float rotation = windCycle * num2 * num7;
                    Main.spriteBatch.Draw(tileDrawTexture, vector6, (Rectangle?)rectangle, tileLight, rotation, vector5, 1f, tileSpriteEffect, 0f);

                    for (int q = 0; q < 7; q++)
                    {
                        float x = Utils.RandomInt(ref randSeed, -10, 11) * FlameJitterMultX;
                        float y = Utils.RandomInt(ref randSeed, -10, 1) * FlameJitterMultY;
                        spriteBatch.Draw(flameTexture.Value, vector6 + new Vector2(x, y), (Rectangle?)rectangle, FlameColor, rotation, vector5, 1f, tileSpriteEffect, 0f);
                    }
                }
            }
        }
    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if (Main.tile[i, j].TileFrameX % 54 == 0 && Main.tile[i, j].TileFrameY % 54 == 0)
        {
            Point p = new(i, j);
            if (!Coordinates.Contains(p)) Coordinates.Add(p);
        }
        return false;
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
		if (tile.TileFrameX <= 52)
		{
            r = 0.9f;
            g = 0.5f;
            b = 0.7f;
        }
    }
}
public class HeartstoneLantern : LanternTemplate
{
    public override Color FlameColor => new Color(85, 85, 85, 0);
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneLantern>();
    public List<Point> Coordinates = new List<Point>();
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
        Coordinates = new();
		flameTexture = HasFlame ? ModContent.Request<Texture2D>(Texture + "_Flame") : null;
	}
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Point p = new(i, j);
        if (Coordinates.Contains(p)) Coordinates.Remove(p);
    }
    public void DrawMultiTileVines(int i, int j, SpriteBatch spriteBatch)
    {
        Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
        Vector2 zero = Vector2.Zero;
        int sizeX = 1;
        int sizeY = 1;
        Tile tile = Main.tile[i, j];
        if (tile != null && tile.HasTile)
        {
            sizeX = 1;
            sizeY = 2;
            DrawMultiTileVinesInWind(unscaledPosition, zero, i, j, sizeX, sizeY, spriteBatch);
        }
    }

    private void DrawMultiTileVinesInWind(Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY, SpriteBatch spriteBatch)
    {
        double _sunflowerWindCounter = (double)typeof(TileDrawing).GetField("_sunflowerWindCounter", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        UnifiedRandom _rand = (UnifiedRandom)typeof(TileDrawing).GetField("_rand", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        bool _isActiveAndNotPaused = (bool)typeof(TileDrawing).GetField("_isActiveAndNotPaused", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);

        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)topLeftY << 32 | (long)((ulong)topLeftX));

        float windCycle = (float)typeof(TileDrawing).GetMethod("GetWindCycle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { topLeftX, topLeftY, _sunflowerWindCounter });
        float num = windCycle;
        int totalPushTime = 60;
        float pushForcePerFrame = 1.26f;
        float highestWindGridPushComplex = (float)typeof(TileDrawing).GetMethod("GetHighestWindGridPushComplex", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { topLeftX, topLeftY, sizeX, sizeY, totalPushTime, pushForcePerFrame, 3, true });
        windCycle += highestWindGridPushComplex;
        new Vector2(sizeX * 16 * 0.5f, 0f);
        Vector2 vector = new Vector2(topLeftX * 16 - (int)screenPosition.X + sizeX * 16f * 0.5f, topLeftY * 16 - (int)screenPosition.Y) + offSet;
        float num2 = 0.07f;
        Tile tile = Main.tile[topLeftX, topLeftY];
        int type = tile.TileType;
        Vector2 vector2 = new(0f, -2f);
        vector += vector2;
        Texture2D texture2D = null;
        Color color = Color.Transparent;
        float? num3 = null;
        float num4 = 1f;
        float num5 = -4f;
        bool flag2 = false;
        num2 = 0.15f;
        int PositioningFix = 192;
        num3 = 1f;
        num5 = 0f;
        if (flag2)
        {
            vector += new Vector2(0f, 16f);
        }
        num2 *= -1f;
        if (!WorldGen.InAPlaceWithWind(topLeftX, topLeftY, sizeX, sizeY))
        {
            windCycle -= num;
        }
        Vector2 vector4 = default(Vector2);
        Rectangle rectangle = default(Rectangle);
        for (int i = topLeftX; i < topLeftX + sizeX; i++)
        {
            for (int j = topLeftY; j < topLeftY + sizeY; j++)
            {
                Tile tile2 = Main.tile[i, j];
                ushort type2 = tile2.TileType;
                if (type2 != type || !IsVisible(tile2))
                {
                    return;
                }
                Math.Abs((i - topLeftX + 0.5f) / sizeX - 0.5f);
                short tileFrameX = tile2.TileFrameX;
                short tileFrameY = tile2.TileFrameY;
                float num7 = (j - topLeftY + 1) / (float)sizeY;
                if (num7 == 0f)
                {
                    num7 = 0.1f;
                }
                if (num3.HasValue)
                {
                    num7 = num3.Value;
                }
                if (flag2 && j == topLeftY)
                {
                    num7 = 0f;
                }
                Main.instance.TilesRenderer.GetTileDrawData(i, j, tile2, type2, ref tileFrameX, ref tileFrameY, out var tileWidth, out var tileHeight, out var tileTop, out var halfBrickHeight, out var addFrX, out var addFrY, out var tileSpriteEffect, out var _, out var _, out var _);
                bool flag3 = _rand.Next(4) == 0;
                Color tileLight = Lighting.GetColor(i, j);
                typeof(TileDrawing).GetMethod("DrawAnimatedTile_AdjustForVisionChangers", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { i, j, tile2, type2, tileFrameX, tileFrameY, tileLight, flag3 });
                tileLight = (Color)typeof(TileDrawing).GetMethod("DrawTiles_GetLightOverride", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { j, i, tile2, type2, tileFrameX, tileFrameY, tileLight });
                if (_isActiveAndNotPaused && flag3)
                {
                    typeof(TileDrawing).GetMethod("DrawTiles_EmitParticles", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(Main.instance.TilesRenderer, new object[] { j, i, tile2, type2, tileFrameX, tileFrameY, tileLight });
                }
                Vector2 vector3 = new Vector2(i * 16 - (int)screenPosition.X, j * 16 - (int)screenPosition.Y + tileTop) + offSet;
                vector3 += vector2;
                vector4 = new(windCycle * num4, Math.Abs(windCycle) * num5 * num7);
                Vector2 vector5 = vector - vector3;
                Texture2D tileDrawTexture = Main.instance.TilesRenderer.GetTileDrawTexture(tile2, i, j);
                if (tileDrawTexture != null)
                {

                    Vector2 vector6 = vector + new Vector2(0f, vector4.Y);
                    rectangle = new(tileFrameX + addFrX, tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight);
                    float rotation = windCycle * num2 * num7;
                    Main.spriteBatch.Draw(tileDrawTexture, vector6, (Rectangle?)rectangle, tileLight, rotation, vector5, 1f, tileSpriteEffect, 0f);

                    if (HasFlame)
                    {
                        for (int q = 0; q < 7; q++)
                        {
                            float x = Utils.RandomInt(ref randSeed, -10, 11) * FlameJitterMultX;
                            float y = Utils.RandomInt(ref randSeed, -10, 1) * FlameJitterMultY;
                            spriteBatch.Draw(flameTexture.Value, vector6 + new Vector2(x, y), (Rectangle?)rectangle, FlameColor, rotation, vector5, 1f, tileSpriteEffect, 0f);
                        }
                    }
                }
            }
        }
    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if (Main.tile[i, j].TileFrameX % 18 == 0 && Main.tile[i, j].TileFrameY % 36 == 0)
        {
            Point p = new(i, j);
            if (!Coordinates.Contains(p)) Coordinates.Add(p);
        }
        return false;
    }
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
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
	{
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
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
        Color color = new Color(85, 85, 85, 0);
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
            Main.spriteBatch.Draw(flameTexture.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
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
    public override float SittingHeight => 1;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneSofa>();
}

public class HeartstoneTable : TableTemplate { }

public class HeartstoneToilet : ToiletTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Heartstone.HeartstoneToilet>();
}

public class HeartstoneWorkbench : WorkbenchTemplate { }
