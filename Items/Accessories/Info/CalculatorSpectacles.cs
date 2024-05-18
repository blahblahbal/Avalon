using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Avalon.Common.Players;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Avalon.Tiles.Ores;

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
		//else if (Data.Sets.Tile.SixOrePerBar.Contains(type))
		//{
		//	bars /= 6f;
		//}
		//else if (Data.Sets.Tile.SevenOrePerBar.Contains(type))
		//{
		//	bars /= 7f;
		//}
		else if (Data.Sets.Tile.EightOrePerBar.Contains(type))
		{
			bars /= 8f;
		}
		else if (type == ModContent.TileType<Heartstone>())
		{
			bars /= 45;
		}
		else if (type == ModContent.TileType<Starstone>())
		{
			bars /= 60;
		}
		else if (type == ModContent.TileType<Boltstone>())
		{
			bars /= 25;
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
			Color c = Lighting.GetColor(tilepos);

			if (TileID.Sets.Ore[Main.tile[tilepos.X, tilepos.Y].TileType] && c.R > 5 && c.G > 5 && c.B > 5 && Main.tile[tilepos.X, tilepos.Y].TileType != ModContent.TileType<Tiles.Ores.PrimordialOre>())
            {
                int bars = (int)CalculatorSpectacles.CountOres(tilepos, Main.tile[tilepos.X, tilepos.Y].TileType, 700);
				string text = bars.ToString();
				if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Heartstone>() ||
					Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Starstone>() ||
					Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Boltstone>())
				{
					text += (bars == 1 ? " crystal" : " crystals");
				}
				else text += (bars == 1 ? " bar" : " bars");
				DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, text, Main.MouseScreen + new Vector2(-5, -32), Color.Yellow, Color.Black, 1.4f);
            }
        }
    }
}
