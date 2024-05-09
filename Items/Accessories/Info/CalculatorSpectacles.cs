using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Avalon.Common.Players;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI;

namespace Avalon.Items.Accessories.Info;

public class CalculatorSpectacles : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = dims.Height;
    }
    public static List<List<Point>> AddValidNeighbors(List<List<Point>> p, Point start)
    {
        p.Add(new List<Point>()
        {
            start + new Point(0, -1), start + new Point(0, 1), start + new Point(-1, 0), start + new Point(1, 0)
        });
        return p;
    }
    public static int CountOres(Point p, int type, int maxTiles = 500)
    {
        int tiles = 0;
        int t2 = 0;

        Tile tile = Framing.GetTileSafely(p);
        if (!tile.HasTile || tile.TileType != type)
        {
            return 0;
        }

        List<List<Point>> points = new List<List<Point>>();
        points = AddValidNeighbors(points, p);

        int index = 0;
        while (points.Count > 0 && tiles < maxTiles && index < points.Count)
        {
            List<Point> tilePos = points[index];

            foreach (Point a in tilePos)
            {
                Tile t = Framing.GetTileSafely(a.X, a.Y);
                if (t.HasTile && t.TileType == type)
                {
                    tiles++;
                    AddValidNeighbors(points, a);
                    if (!t.YellowWire)
                    {

                        t.YellowWire = true;
                    }
                }
            }
            index++;
        }
        foreach (List<Point> z in points)
        {
            foreach (Point q in z)
            {
                Tile t = Framing.GetTileSafely(q.X, q.Y);
                if (t.HasTile && t.TileType == type)
                {
                    if (t.YellowWire)
                    {
                        t2++;
                        t.YellowWire = false;
                    }
                }
            }
        }

        int bars = t2;
        if (Data.Sets.Tile.ThreeOrePerBar.Contains(type))
        {
            bars /= 3;
        }
        else if (Data.Sets.Tile.FourOrePerBar.Contains(type))
        {
            bars /= 4;
        }
        else if (Data.Sets.Tile.FiveOrePerBar.Contains(type))
        {
            bars /= 5;
        }

        return bars;
    }
}
public class CalcSpecSystem : ModSystem
{
    private void DrawOutlinedString(SpriteBatch SB, DynamicSpriteFont SF, string txt, Vector2 P, Color C, Color shadeC, float strength = 1f, Vector2 V = default(Vector2), float scale = 1f, SpriteEffects SE = SpriteEffects.None, float LL = 0f)
    {
        if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt)) return;
        Vector2[] OS = new Vector2[4] { new Vector2(strength, strength), new Vector2(strength, -strength), new Vector2(-strength, strength), new Vector2(-strength, -strength) };
        foreach (Vector2 VO in OS)
            DynamicSpriteFontExtensionMethods.DrawString(SB, SF, txt, new Vector2(P.X + VO.X, P.Y + VO.Y), shadeC, 0f, V, scale, SE, LL);
        DynamicSpriteFontExtensionMethods.DrawString(SB, SF, txt, P, C, 0f, V, scale, SE, LL);
    }

    public override void PostDrawInterface(SpriteBatch spriteBatch)
    {
        if (Main.LocalPlayer.HasItem(ModContent.ItemType<CalculatorSpectacles>()))
        {
            Point tilepos = Main.LocalPlayer.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
            if (TileID.Sets.Ore[Main.tile[tilepos.X, tilepos.Y].TileType])
            {
                int bars = CalculatorSpectacles.CountOres(tilepos, Main.tile[tilepos.X, tilepos.Y].TileType, 500);
                DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, bars + " bars" /*" [i:" + Data.Sets.Tile.OresToBars[Main.tile[tilepos.X, tilepos.Y].TileType] + "]"*/, Main.LocalPlayer.GetModPlayer<AvalonPlayer>().MousePosition - Main.screenPosition - new Vector2(32, 32), Color.Yellow, Color.Black, 1.4f);
            }
        }
    }
}
