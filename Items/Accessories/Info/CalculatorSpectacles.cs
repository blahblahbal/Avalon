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
using System;
using Terraria.UI;
using Avalon.Items.Consumables;

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
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().CalculatorSpectacles = true;
	}
	public override void UpdateInventory(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().CalculatorSpectacles = true;
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

        List<List<Point>> points = new();
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

        return fullAmount.Count;
    }
}
internal class CalcSpec : UIState
{
	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().CalculatorSpectacles)
		{
			Point tilepos = Main.LocalPlayer.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
			Color c = Lighting.GetColor(tilepos);
			Data.Sets.Tile.OresToBars.TryGetValue(Main.tile[tilepos.X, tilepos.Y].TileType, out int stupidWayOfCheckingIfThisIsZero);

			if (TileID.Sets.Ore[Main.tile[tilepos.X, tilepos.Y].TileType] &&
				//Main.tile[tilepos.X, tilepos.Y].TileType != ModContent.TileType<Boltstone>() &&
				//Main.tile[tilepos.X, tilepos.Y].TileType != ModContent.TileType<Heartstone>() &&
				//Main.tile[tilepos.X, tilepos.Y].TileType != ModContent.TileType<Starstone>() &&
				Main.tile[tilepos.X, tilepos.Y].TileType != ModContent.TileType<SulphurOre>() &&
				c.R > 5 && c.G > 5 && c.B > 5)
			{
				ushort type = Main.tile[tilepos.X, tilepos.Y].TileType;
				int bars = (int)CalculatorSpectacles.CountOres(tilepos, type, 700);
				int remainder = 0;
				int remainderDenominator = 3;

				// for some reason CountOres returns 0 if there's only 1 ore
				if (bars == 0) bars = 1;

				int barType = -1;

				if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<ShroomiteOre>())
				{
					remainder = bars % 5;
					bars /= 5;
					remainderDenominator = 5;
					barType = ItemID.ShroomiteBar;
				}
				else if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Heartstone>())
				{
					remainder = bars % 45;
					bars /= 45;
					remainderDenominator = 45;
					barType = ItemID.LifeCrystal;
				}
				else if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Starstone>())
				{
					remainder = bars % 60;
					bars /= 60;
					remainderDenominator = 60;
					barType = ItemID.ManaCrystal;
				}
				else if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Boltstone>())
				{
					remainder = bars % 25;
					bars /= 25;
					remainderDenominator = 25;
					barType = ModContent.ItemType<StaminaCrystal>();
				}
				else
				{
					ModTile t = TileLoader.GetTile(Main.tile[tilepos.X, tilepos.Y].TileType);
					if (t != null)
					{
						var drops = t.GetItemDrops(tilepos.X, tilepos.Y);

						int amtOfOre = 0;
						foreach (Item item in drops)
						{
							foreach (Recipe recipe in Main.recipe)
							{
								if (recipe.TryGetIngredient(item.type, out Item ing))
								{
									if (recipe.createItem.Name.Contains("Bar"))
									{
										amtOfOre = ing.stack;
										barType = recipe.createItem.type;
										break;
									}
								}
							}
						}
						remainder = bars % amtOfOre;
						bars /= amtOfOre;
						remainderDenominator = amtOfOre;
					}
					else if (Main.tile[tilepos.X, tilepos.Y].TileType < TileID.Count)
					{
						if (Data.Sets.Tile.ThreeOrePerBar.Contains(type))
						{
							remainder = bars % 3;
							bars /= 3;
							remainderDenominator = 3;
						}
						else if (Data.Sets.Tile.FourOrePerBar.Contains(type))
						{
							remainder = bars % 4;
							bars /= 4;
							remainderDenominator = 4;
						}
						else if (Data.Sets.Tile.FiveOrePerBar.Contains(type))
						{
							remainder = bars % 5;
							bars /= 5;
							remainderDenominator = 5;
						}
						barType = Data.Sets.Tile.OresToBars[type];
					}
				}


				string text = bars.ToString();
				int ypos = -40;

				Vector2 pos = Main.MouseScreen + new Vector2(-5, ypos);
				if (remainder > 0)
				{
					text += "" + remainder + "/" + remainderDenominator;
				}
				Vector2 posModified = new Vector2(Main.MouseScreen.X - FontAssets.MouseText.Value.MeasureString(text).X, pos.Y);
				Vector2 pos3 = posModified;

				if (bars > 0)
				{
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, $"{bars}", pos3, Color.Yellow, Color.Black, 1.4f);
				}
				if (remainder > 0)
				{
					int xmod = -5;
					if (remainder > 9) xmod = -11;
					pos3.X += FontAssets.MouseText.Value.MeasureString($"{bars}  ").X;
					if (bars == 0 && (barType == ItemID.LifeCrystal || barType == ItemID.ManaCrystal || barType == ModContent.ItemType<StaminaCrystal>()))
					{
						pos3.X += 18;
					}
					//Main.NewText(FontAssets.MouseText.Value.MeasureString($"{bars}  ").X);
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, "/", pos3, Color.Yellow, Color.Black, 1.4f);
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, $"{remainder}", pos3 + new Vector2(xmod, 0), Color.Yellow, Color.Black, 1.4f, scale: 0.6f);
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, $"{remainderDenominator}", pos3 + new Vector2(5, 10), Color.Yellow, Color.Black, 1.4f, scale: 0.6f);
				}
				DrawOutlinedTexture(spriteBatch, TextureAssets.Item[barType].Value, posModified + new Vector2(FontAssets.MouseText.Value.MeasureString(text).X + 10, 0), Color.White, Color.White, 1.4f, Vector2.Zero);
				//if (remainder > 0)
				//{
				//	text = remainder.ToString();
				//	posModified = new Vector2(Main.MouseScreen.X - FontAssets.MouseText.Value.MeasureString(text).X, pos2.Y);
				//	DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, text, posModified /*+ new Vector2(FontAssets.MouseText.Value.MeasureString(text2).X - 9, 0)*/, Color.Yellow, Color.Black, 1.4f);
				//	DrawOutlinedTexture(spriteBatch, TextureAssets.Item[Data.Sets.Tile.OreTilesToItems[type]].Value, posModified + new Vector2(FontAssets.MouseText.Value.MeasureString(text).X + 10, 0), Color.White, Color.White, 1.4f);
				//}
				// ⅓
			}
		}
	}
	private void DrawOutlinedString(SpriteBatch SB, DynamicSpriteFont SF, string txt, Vector2 P, Color C, Color shadeC, float strength = 1f, Vector2 V = default(Vector2), float scale = 1f, SpriteEffects SE = SpriteEffects.None, float LL = 0f)
	{
		SB.End();
		SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
		if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt)) return;
		Vector2[] OS = new Vector2[4] { new Vector2(strength, strength), new Vector2(strength, -strength), new Vector2(-strength, strength), new Vector2(-strength, -strength) };
		foreach (Vector2 VO in OS)
			DynamicSpriteFontExtensionMethods.DrawString(SB, SF, txt, new Vector2(P.X + VO.X, P.Y + VO.Y), shadeC, 0f, V, scale, SE, LL);
		DynamicSpriteFontExtensionMethods.DrawString(SB, SF, txt, P, C, 0f, V, scale, SE, LL);
		SB.End();
		SB.Begin();
	}

	private void DrawOutlinedTexture(SpriteBatch sb, Texture2D tex, Vector2 pos, Color color, Color shadowColor, float strength = 1f, Vector2 vec = default, float scale = 1f, SpriteEffects effects = SpriteEffects.None, float LL = 0f)
	{
		if (tex == null) return;

		sb.End();
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

		int num = 2;
		int num2 = num * 2;
		for (int i = -num2; i <= num2; i += num)
		{
			for (int j = -num2; j <= num2; j += num)
			{
				if (Math.Abs(i) + Math.Abs(j) == num2)
				{
					sb.Draw(tex, new Vector2(pos.X + i, pos.Y + j), Color.Black);
				}
			}
		}
		sb.End();
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, ExxoAvalonOrigins.CalculatorSpectaclesEffect, Main.GameViewMatrix.ZoomMatrix);
		num2 = num;
		for (int k = -num2; k <= num2; k += num)
		{
			for (int l = -num2; l <= num2; l += num)
			{
				if (Math.Abs(k) + Math.Abs(l) == num2)
				{
					sb.Draw(tex, new Vector2(pos.X + k, pos.Y + l), Color.White);
				}
			}
		}
		sb.End();
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

		sb.Draw(tex, pos, color);
	}
}
