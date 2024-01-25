using Microsoft.Xna.Framework;
using Terraria;
using Avalon.Dusts;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Drawing;
using System;
using Terraria.Utilities;
using Terraria.DataStructures;
using System.Reflection;
using System.Collections.Generic;
using static Terraria.WorldGen;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Material.Ores;

namespace Avalon.Tiles.GemTrees;

public class PeridotTree : ModTile
{
    private readonly string TexturePath = "Avalon/Tiles/GemTrees/PeridotGemTree";

    public override void SetStaticDefaults()
    {
        Main.tileAxe[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.CountsAsGemTree[Type] = true;
        TileID.Sets.IsATreeTrunk[Type] = true;
        TileID.Sets.IsShakeable[Type] = true;
        TileID.Sets.GetsDestroyedForMeteors[Type] = true;
        TileID.Sets.GetsCheckedForLeaves[Type] = true;
        TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
        TileID.Sets.PreventsTileReplaceIfOnTopOfIt[Type] = true;
        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(128, 128, 128), name);
        DustType = DustID.Stone;
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        width = 20;
        height = 20;
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Tile tile = Main.tile[i, j];
        if (i > 5 && j > 5 && i < Main.maxTilesX - 5 && j < Main.maxTilesY - 5 && Main.tile[i, j] != null)
        {
            if (tile.HasTile)
            {
                if (Main.tileFrameImportant[Type])
                {
                    CheckTreeWithSettings(i, j, new CheckTreeSettings
                    {
                        IsGroundValid = GemTreeGroundTest
                    });
                }
            }
        }
        return false;
    }

    public override bool CreateDust(int i, int j, ref int type)
    {
        int dustType = ((WorldGen.genRand.Next(10) != 0) ? 1 : ModContent.DustType<PeridotDust>());
        Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, dustType);
        return false;
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tileCache = Main.tile[i, j];
        int dropItem = ItemID.None;
        int secondaryItem = ItemID.None;
        int dropItemStack = 1;
        SetGemTreeDrops(ModContent.ItemType<Peridot>(), ModContent.ItemType<PeridotGemcorn>(), tileCache, ref dropItem, ref secondaryItem);
        if (dropItem == 3)
        {
            dropItemStack = Main.rand.Next(1, 3);
        }
        yield return new Item(dropItem, dropItemStack);
        yield return new Item(secondaryItem);
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        spriteBatch.End();
        spriteBatch.Begin(0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.EffectMatrix);
        DrawTrees(i, j);
        spriteBatch.End();
        spriteBatch.Begin(); //No params as PostDraw doesn't use spritebatch with params
    }

    private static void SetGemTreeDrops(int gemType, int seedType, Tile tileCache, ref int dropItem, ref int secondaryItem)
    {
        if (Main.rand.Next(10) == 0)
        {
            dropItem = gemType;
        }
        else
        {
            dropItem = ItemID.StoneBlock;
        }
        if (tileCache.TileFrameX >= 22 && tileCache.TileFrameY >= 198 && Main.rand.Next(2) == 0)
        {
            secondaryItem = seedType;
        }
    }

    private static void ShakeTree(int i, int j)
    {
        FieldInfo numTreeShakesReflect = typeof(WorldGen).GetField("numTreeShakes", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
        int numTreeShakes = (int)numTreeShakesReflect.GetValue(null);
        int maxTreeShakes = (int)typeof(WorldGen).GetField("maxTreeShakes", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
        int[] treeShakeX = (int[])typeof(WorldGen).GetField("treeShakeX", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
        int[] treeShakeY = (int[])typeof(WorldGen).GetField("treeShakeY", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);

        if (numTreeShakes == maxTreeShakes)
        {
            return;
        }
        GetTreeBottom(i, j, out var x, out var y);
        for (int k = 0; k < numTreeShakes; k++)
        {
            if (treeShakeX[k] == x && treeShakeY[k] == y)
            {
                return;
            }
        }
        treeShakeX[numTreeShakes] = x;
        treeShakeY[numTreeShakes] = y;
        numTreeShakesReflect.SetValue(null, numTreeShakes++);
        y--;
        while (y > 10 && Main.tile[x, y].HasTile && TileID.Sets.IsShakeable[Main.tile[x, y].TileType])
        {
            y--;
        }
        y++;
        if (!IsTileALeafyTreeTop(x, y) || Collision.SolidTiles(x - 2, x + 2, y - 2, y + 2))
        {
            return;
        }
        if (Main.netMode == 2)
        {
            NetMessage.SendData(112, -1, -1, null, 1, x, y, 1f, ModContent.GoreType<PeridotGemLeaves>());
        }
        if (Main.netMode == 0)
        {
            TreeGrowFX(x, y, 1, ModContent.GoreType<PeridotGemLeaves>(), hitTree: true);
        }
    }

    private static void EmitPeridotLeaves(int tilePosX, int tilePosY, int grassPosX, int grassPosY)
    {
        bool _isActiveAndNotPaused = (bool)typeof(TileDrawing).GetField("_isActiveAndNotPaused", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        int _leafFrequency = (int)typeof(TileDrawing).GetField("_leafFrequency", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        UnifiedRandom _rand = (UnifiedRandom)typeof(TileDrawing).GetField("_rand", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        if (!_isActiveAndNotPaused)
        {
            return;
        }
        Tile tile = Main.tile[tilePosX, tilePosY];
        if (tile.LiquidAmount > 0)
        {
            return;
        }
        int num = 0;
        bool flag = (byte)num != 0;
        int num2 = _leafFrequency;
        bool flag2 = tilePosX - grassPosX != 0;
        if (flag)
        {
            num2 /= 2;
        }
        if (!WorldGen.DoesWindBlowAtThisHeight(tilePosY))
        {
            num2 = 10000;
        }
        if (flag2)
        {
            num2 *= 3;
        }
        if (_rand.Next(num2) != 0)
        {
            return;
        }
        int num3 = 2;
        Vector2 vector = new((float)(tilePosX * 16 + 8), (float)(tilePosY * 16 + 8));
        if (flag2)
        {
            int num4 = tilePosX - grassPosX;
            vector.X += num4 * 12;
            int num5 = 0;
            if (tile.TileFrameY == 220)
            {
                num5 = 1;
            }
            else if (tile.TileFrameY == 242)
            {
                num5 = 2;
            }
            if (tile.TileFrameX == 66)
            {
                switch (num5)
                {
                    case 0:
                        vector += new Vector2(0f, -6f);
                        break;
                    case 1:
                        vector += new Vector2(0f, -6f);
                        break;
                    case 2:
                        vector += new Vector2(0f, 8f);
                        break;
                }
            }
            else
            {
                switch (num5)
                {
                    case 0:
                        vector += new Vector2(0f, 4f);
                        break;
                    case 1:
                        vector += new Vector2(2f, -6f);
                        break;
                    case 2:
                        vector += new Vector2(6f, -6f);
                        break;
                }
            }
        }
        else
        {
            vector += new Vector2(-16f, -16f);
            if (flag)
            {
                vector.Y -= Main.rand.Next(0, 28) * 4;
            }
        }
        if (!WorldGen.SolidTile(vector.ToTileCoordinates()))
        {
            Gore.NewGoreDirect(new EntitySource_Misc(""), vector, Utils.RandomVector2(Main.rand, -num3, num3), ModContent.GoreType<PeridotGemLeaves>(), 0.7f + Main.rand.NextFloat() * 0.6f).Frame.CurrentColumn = Main.tile[tilePosX, tilePosY].TileColor;
        }
    }

    //I would recomend putting this into its own file so when making multiple gemtrees you can point to that file instead of each individual gemtrees
    public static bool GetGemTreeFoliageData(int i, int j, int xoffset, ref int treeFrame, out int floorY, out int topTextureFrameWidth, out int topTextureFrameHeight)
    {
        int num = i + xoffset;
        topTextureFrameWidth = 116;
        topTextureFrameHeight = 96;
        floorY = j;
        for (int k = 0; k < 100; k++)
        {
            floorY = j + k;
            Tile tile2 = Main.tile[num, floorY];
            if (tile2 == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DrawTrees(int k, int l)
    {
        double _treeWindCounter = (double)typeof(TileDrawing).GetField("_treeWindCounter", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
        Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
        Vector2 zero = Vector2.Zero;
        float num15 = 0.08f;
        float num16 = 0.06f;
        int PositioningFix = 192; //Fix to the positioning to the Branches and Tops being 192 pixels to the top and left
        int x = k;
        int y = l;
        Tile tile = Main.tile[x, y];
        if (tile == null || !tile.HasTile || tile.TileType != ModContent.TileType<PeridotTree>())
        {
            return;
        }
        short frameX = tile.TileFrameX;
        short frameY = tile.TileFrameY;
        bool flag = tile.WallType > 0;
        if (frameY >= 198 && frameX >= 22)
        {
            int treeFrame = WorldGen.GetTreeFrame(tile);
            switch (frameX)
            {
                case 22:
                    {
                        int num5 = 0;
                        int grassPosX = x + num5;
                        if (!GetGemTreeFoliageData(x, y, num5, ref treeFrame, out int floorY3, out int topTextureFrameWidth3, out int topTextureFrameHeight3))
                        {
                            return;
                        }
                        EmitPeridotLeaves(x, y, grassPosX, floorY3);
                        Texture2D treeTopTexture = null;
                        if (treeTopTexture == null)
                        {
                            treeTopTexture = (Texture2D)ModContent.Request<Texture2D>(TexturePath + "_Tops");
                        }
                        Vector2 vector = new Vector2((float)(x * 16 - (int)unscaledPosition.X + 8 + PositioningFix), (float)(y * 16 - (int)unscaledPosition.Y + 16 + PositioningFix)) + zero;
                        float num7 = 0f;
                        if (!flag)
                        {
                            num7 = Main.instance.TilesRenderer.GetWindCycle(x, y, _treeWindCounter);
                        }
                        vector.X += num7 * 2f;
                        vector.Y += Math.Abs(num7) * 2f;
                        Color color6 = Lighting.GetColor(x, y);
                        if (tile.IsTileFullbright)
                        {
                            color6 = Color.White;
                        }
                        Main.spriteBatch.Draw(treeTopTexture, vector, (Rectangle?)new Rectangle(treeFrame * (topTextureFrameWidth3 + 2), 0, topTextureFrameWidth3, topTextureFrameHeight3), color6, num7 * num15, new Vector2((float)(topTextureFrameWidth3 / 2), (float)topTextureFrameHeight3), 1f, (SpriteEffects)0, 0f);
                        break;
                    }
                case 44:
                    {
                        int num21 = x;
                        int num2 = 1;
                        if (!GetGemTreeFoliageData(x, y, num2, ref treeFrame, out int floorY2, out _, out _))
                        {
                            return;
                        }
                        EmitPeridotLeaves(x, y, num21 + num2, floorY2);
                        Texture2D treeBranchTexture2 = null;
                        if (treeBranchTexture2 == null)
                        {
                            treeBranchTexture2 = (Texture2D)ModContent.Request<Texture2D>(TexturePath + "_Branches");
                        }
                        Vector2 position2 = new Vector2((float)(x * 16) + PositioningFix, (float)(y * 16) + PositioningFix) - unscaledPosition.Floor() + zero + new Vector2(16f, 12f);
                        float num4 = 0f;
                        if (!flag)
                        {
                            num4 = Main.instance.TilesRenderer.GetWindCycle(x, y, _treeWindCounter);
                        }
                        if (num4 > 0f)
                        {
                            position2.X += num4;
                        }
                        position2.X += Math.Abs(num4) * 2f;
                        Color color4 = Lighting.GetColor(x, y);
                        if (tile.IsTileFullbright)
                        {
                            color4 = Color.White;
                        }
                        Main.spriteBatch.Draw(treeBranchTexture2, position2, (Rectangle?)new Rectangle(0, treeFrame * 42, 40, 40), color4, num4 * num16, new Vector2(40f, 24f), 1f, (SpriteEffects)0, 0f);
                        break;
                    }
                case 66:
                    {
                        int num17 = x;
                        int num18 = -1;
                        if (!GetGemTreeFoliageData(x, y, num18, ref treeFrame, out int floorY, out _, out _))
                        {
                            return;
                        }
                        EmitPeridotLeaves(x, y, num17 + num18, floorY);
                        Texture2D treeBranchTexture = null;
                        if (treeBranchTexture == null)
                        {
                            treeBranchTexture = (Texture2D)ModContent.Request<Texture2D>(TexturePath + "_Branches");
                        }
                        Vector2 position = new Vector2((float)(x * 16) + PositioningFix, (float)(y * 16) + PositioningFix) - unscaledPosition.Floor() + zero + new Vector2(0f, 18f);
                        float num20 = 0f;
                        if (!flag)
                        {
                            num20 = Main.instance.TilesRenderer.GetWindCycle(x, y, _treeWindCounter);
                        }
                        if (num20 < 0f)
                        {
                            position.X += num20;
                        }
                        position.X -= Math.Abs(num20) * 2f;
                        Color color2 = Lighting.GetColor(x, y);
                        if (tile.IsTileFullbright)
                        {
                            color2 = Color.White;
                        }
                        Main.spriteBatch.Draw(treeBranchTexture, position, (Rectangle?)new Rectangle(42, treeFrame * 42, 40, 40), color2, num20 * num16, new Vector2(0f, 30f), 1f, (SpriteEffects)0, 0f);
                        break;
                    }
            }
        }
    }
}
