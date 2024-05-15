using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Avalon.Common.Players;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

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
    public static float CountOres(Point p, int type, int maxTiles = 500)
    {
        int tiles = 0;

        Tile tile = Framing.GetTileSafely(p);
        if (!tile.HasTile || tile.TileType != type)
        {
            return 0;
        }

        List<List<Point>> points = new List<List<Point>>();
        points = AddValidNeighbors(points, p);

        HashSet<Point> fullAmount = new HashSet<Point>();

        int index = 0;
        while (points.Count > 0 && tiles < maxTiles && index < points.Count)
        {
            List<Point> tilePos = points[index];

            foreach (Point a in tilePos)
            {
                if (fullAmount.Contains(a)) continue;
                Tile t = Framing.GetTileSafely(a.X, a.Y);
                if (t.HasTile && t.TileType == type)
                {
                    tiles++;
                    AddValidNeighbors(points, a);
                    fullAmount.Add(a);
                }
            }
            index++;
        }

        float bars = fullAmount.Count;
        if (Data.Sets.Tile.ThreeOrePerBar.Contains(type))
        {
            bars /= 3f;
        }
        else if (Data.Sets.Tile.FourOrePerBar.Contains(type))
        {
            bars /= 4f;
        }
        else if (Data.Sets.Tile.FiveOrePerBar.Contains(type))
        {
            bars /= 5f;
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
                int bars = (int)CalculatorSpectacles.CountOres(tilepos, Main.tile[tilepos.X, tilepos.Y].TileType, 700);
                DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, bars + (bars == 1 ? " bar" : " bars"), Main.MouseScreen + new Vector2(-5, -32), Color.Yellow, Color.Black, 1.4f);
            }
        }
    }
}
