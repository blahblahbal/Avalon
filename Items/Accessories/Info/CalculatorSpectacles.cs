using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Avalon.Items.Accessories.Info;

public class CalculatorSpectacles : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2);
	}
	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
	}
	//public override void UpdateAccessory(Player player, bool hideVisual)
	//{
	//	player.GetModPlayer<AvalonPlayer>().CalculatorSpectacles = true;
	//}
	//public override void UpdateInventory(Player player)
	//{
	//	player.GetModPlayer<AvalonPlayer>().CalculatorSpectacles = true;
	//}
	//public override void AddRecipes()
	//{
	//	CreateRecipe()
	//		.AddRecipeGroup("SilverBar", 10)
	//		.AddIngredient(ItemID.Lens, 2)
	//		.AddIngredient()
	//}
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

		Tile tile = Framing.GetTileSafely(p);
		if (!tile.HasTile || tile.TileType != type)
		{
			return 0;
		}

		List<List<Point>> points = new();
		points = AddValidNeighbors(points, p);

		HashSet<Point> fullAmount = [p];

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

public class CalcSpecInfoDisplay : InfoDisplay
{
	public override string HoverTexture => Texture + "_Hover";
	public static int BarValue = -1;
	public static int OreRemainder = -1;
	public static int BarType = -1;
	public static int Denominator = -1;
	public override bool Active()
	{
		return Main.LocalPlayer.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay;
	}

	public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
	{
		if (BarType > -1 && BarValue > -1)
		{
			string barName = Lang.GetItemNameValue(BarType);
			string barAmount = BarValue.ToString();

			string all = barName + ": " + barAmount;

			if (OreRemainder > 0 && Denominator > 0)
			{
				all += " " + OreRemainder + "/" + Denominator;
			}
			return all;
		}
		else
		{
			displayColor = InactiveInfoTextColor;
			return Language.GetTextValue("Mods.Avalon.InfoDisplays.NoInfo");
		}
	}
}

public class CalcSpecGlobalItem : GlobalItem
{
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		if (!Main.LocalPlayer.hideInfo[ModContent.GetInstance<CalcSpecInfoDisplay>().Type] && Main.LocalPlayer.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay)
		{
			int amtOfOre = 0;
			int barType = -1;
			int bars = 0;
			string text = "";
			foreach (Recipe recipe in Main.recipe)
			{
				// check if the recipe's ingredient matches the drop of the tile
				if (item.type > ItemID.Count) // this check is pretty much only necessary for hellstone, but I literally could not be bothered fixing it properly rn
				{
					var amountAndType = CalcSpec.GetModdedBars(item, amtOfOre, barType);
					amtOfOre = amountAndType.amountOfOre;
					barType = amountAndType.barType;
					bars = item.stack;
					break;
				}
				else
				{
					foreach (int itemID in Data.Sets.TileSets.VanillaOreTilesToBarItems.Values)
					{
						if (recipe.TryGetResult(itemID, out _))
						{
							if (Data.Sets.ItemSets.VanillaBarItems[itemID])
							{
								if (recipe.TryGetIngredient(item.type, out Item ingr))
								{
									if (item.createTile == -1 || (!TileID.Sets.Ore[item.createTile] && item.createTile != TileID.LunarOre)) continue;
									if (recipe.createItem.type == itemID)
									{
										amtOfOre = ingr.stack;
										barType = itemID;
										bars = item.stack;
										break;
									}
								}
							}
						}
					}
				}
			}
			//if (item.type == ModContent.ItemType<Material.Ores.ShroomiteOre>())
			//{
			//	amtOfOre = 5;
			//	barType = ItemID.ShroomiteBar;
			//	bars = item.stack;
			//}
			if (amtOfOre != 0 && barType != -1)
			{
				int remainder = bars % amtOfOre;
				bars /= amtOfOre;
				text = bars + " [i:" + barType + "] " + remainder + " [i:" + item.type + "]";
				tooltips.Add(new TooltipLine(Mod, "CalcSpecTooltip", NetworkText.FromLiteral(text).ToString()));
			}
		}
	}
}
internal class CalcSpec : UIState
{
	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		if (!Main.LocalPlayer.hideInfo[ModContent.GetInstance<CalcSpecInfoDisplay>().Type] && Main.LocalPlayer.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay)
		{
			bool itemAlreadyAssigned = false;
			int bars = -1;
			int remainder = -1;
			int remainderDenominator = -1;
			int barType = -1;
			int amtOfOre = 0;
			// get the item dropped in the world, which unfortunately isn't a value stored anywhere in vanillaRectangle mouseRectangle = default(Rectangle);
			Rectangle mouseRectangle = new Rectangle((int)(Main.mouseX + Main.screenPosition.X), (int)(Main.mouseY + Main.screenPosition.Y), 1, 1);
			if (Main.LocalPlayer.gravDir == -1f)
			{
				mouseRectangle.Y = (int)Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
			}
			foreach (var item in Main.ActiveItems)
			{
				Rectangle drawHitbox = Item.GetDrawHitbox(item.type, null);
				Vector2 bottom = item.Bottom;
				Rectangle value = new Rectangle((int)(bottom.X - drawHitbox.Width * 0.5f), (int)(bottom.Y - drawHitbox.Height), drawHitbox.Width, drawHitbox.Height);
				if (mouseRectangle.Intersects(value))
				{
					var amountAndType = GetModdedBars(item, amtOfOre, barType);
					amtOfOre = amountAndType.amountOfOre;
					barType = amountAndType.barType;
					if (barType != -1)
					{
						itemAlreadyAssigned = true;
						bars = item.stack;
						remainder = bars % amtOfOre;
						bars /= amtOfOre;
						remainderDenominator = amtOfOre;
						Draw(spriteBatch, bars, remainder, remainderDenominator, barType);
					}
					break;
				}
			}

			// get the tile
			if (!itemAlreadyAssigned)
			{
				Point tilepos = Main.MouseWorld.ToTileCoordinates();
				if (!WorldGen.InWorld(tilepos.X, tilepos.Y)) return;
				Color c = Lighting.GetColor(tilepos);
				if ((TileID.Sets.Ore[Main.tile[tilepos.X, tilepos.Y].TileType] || Main.tile[tilepos.X, tilepos.Y].TileType == TileID.LunarOre)/* && Main.tile[tilepos.X, tilepos.Y].TileType != ModContent.TileType<SulphurOre>()*/ &&
					c.R > 5 && c.G > 5 && c.B > 5)
				{
					ushort type = Main.tile[tilepos.X, tilepos.Y].TileType;
					bars = CalculatorSpectacles.CountOres(tilepos, type, 700);
					remainder = 0;
					remainderDenominator = 3;

					// grab the modded tile at the cursor's position
					ModTile t = TileLoader.GetTile(Main.tile[tilepos.X, tilepos.Y].TileType);
					if (t != null)
					{
						// grab the drops of the tile
						var drops = t.GetItemDrops(tilepos.X, tilepos.Y);

						var amountAndType = GetModdedBarsFromTileDrop(drops, amtOfOre, barType);
						amtOfOre = amountAndType.amountOfOre;
						barType = amountAndType.barType;
						if (amtOfOre == 0) return;

						// assign all the things necessary to display the text/sprite later on
						remainder = bars % amtOfOre;
						bars /= amtOfOre;
						remainderDenominator = amtOfOre;
					}
					// if the tile is vanilla
					else if (Main.tile[tilepos.X, tilepos.Y].TileType < TileID.Count)
					{
						if (Data.Sets.TileSets.ThreeOrePerBar.Contains(type))
						{
							remainder = bars % 3;
							bars /= 3;
							remainderDenominator = 3;
							barType = Data.Sets.TileSets.VanillaOreTilesToBarItems[type];
						}
						else if (Data.Sets.TileSets.FourOrePerBar.Contains(type))
						{
							remainder = bars % 4;
							bars /= 4;
							remainderDenominator = 4;
							barType = Data.Sets.TileSets.VanillaOreTilesToBarItems[type];
						}
						else if (Data.Sets.TileSets.FiveOrePerBar.Contains(type))
						{
							remainder = bars % 5;
							bars /= 5;
							remainderDenominator = 5;
							barType = Data.Sets.TileSets.VanillaOreTilesToBarItems[type];
						}
						else return;
					}
					Draw(spriteBatch, bars, remainder, remainderDenominator, barType);
				}
				else
				{
					CalcSpecInfoDisplay.BarType = -1;
					CalcSpecInfoDisplay.BarValue = -1;
					CalcSpecInfoDisplay.OreRemainder = -1;
					CalcSpecInfoDisplay.Denominator = -1;
				}
			}
		}
	}
	private (int amountOfOre, int barType) GetModdedBarsFromTileDrop(IEnumerable<Item>? itemsToCheck, int amountOfOre, int barType)
	{
		// loop through the drops of the tile (will only loop once likely)
		if (itemsToCheck != null)
		{
			foreach (Item item in itemsToCheck)
			{
				var amountAndType = GetModdedBars(item, amountOfOre, barType);
				amountOfOre = amountAndType.amountOfOre;
				barType = amountAndType.barType;
			}
		}
		return (amountOfOre, barType);
	}
	public static (int amountOfOre, int barType) GetModdedBars(Item itemToCheck, int amountOfOre, int barType)
	{
		// loop through all recipes
		foreach (Recipe recipe in Main.recipe)
		{
			// check if the recipe's ingredient matches the drop of the tile
			if (ExxoAvalonOrigins.ThoriumContentEnabled && itemToCheck.type == ModContent.ItemType<Material.Ores.Heartstone>() && recipe.TryGetIngredient(ExxoAvalonOrigins.Thorium.Find<ModItem>("LifeQuartz").Type, out Item lq))
			{
				if (recipe.createItem.type == ItemID.LifeCrystal)
				{
					amountOfOre = lq.stack;
					barType = recipe.createItem.type;
				}
			}
			else if (recipe.TryGetIngredient(itemToCheck.type, out Item ing))
			{
				// if the recipe's result contains the word "Bar," set barType to the
				// result's type and amountOfOre to the stack size of the ingredient
				if (recipe.requiredTile.Contains(TileID.Furnaces) ||
					recipe.requiredTile.Contains(TileID.Hellforge) ||
					recipe.requiredTile.Contains(TileID.AdamantiteForge) ||
					recipe.requiredTile.Contains(ModContent.TileType<CaesiumForge>()))
				{
					if (!Data.Sets.TileSets.OresToChunks.ContainsValue(ing.type))
					{
						if (recipe.requiredItem.Count > 1 || ing.createTile == -1 || !TileID.Sets.Ore[ing.createTile] || ing.stack < 2) continue;
					}
					amountOfOre = ing.stack;
					barType = recipe.createItem.type;
					break;
				}
			}
		}
		return (amountOfOre, barType);
	}

	/// <summary>
	/// Sets up the draw positions using the provided parameters, then calls the DrawOutlinedString and DrawOutlinedTexture methods to draw the text and item
	/// </summary>
	/// <param name="spriteBatch"></param>
	/// <param name="bars"></param>
	/// <param name="remainder"></param>
	/// <param name="remainderDenominator"></param>
	/// <param name="barType"></param>
	private void Draw(SpriteBatch spriteBatch, int bars, int remainder, int remainderDenominator, int barType)
	{
		if (barType != -1 && barType < ItemID.Count)
		{
			Main.instance.LoadItem(barType);
		}
		CalcSpecInfoDisplay.BarType = barType;
		CalcSpecInfoDisplay.BarValue = bars;
		CalcSpecInfoDisplay.OreRemainder = remainder;
		CalcSpecInfoDisplay.Denominator = remainderDenominator;

		//string text = bars.ToString();
		string text = $"{(bars > 0 ? bars : "")}";
		int ypos = -40;
		//Main.NewText(FontAssets.MouseText.Value.MeasureString(text).X);

		// Fixes the position based on zoom level and UI scale
		//Vector2 mouseZoomFix = Main.MouseScreen;

		//mouseZoomFix.X += (mouseZoomFix.X - Main.screenWidth / 2f) * Main.GameZoomTarget - (mouseZoomFix.X - Main.screenWidth / 2f);
		//mouseZoomFix.Y += (mouseZoomFix.Y - Main.screenHeight / 2f) * Main.GameZoomTarget - (mouseZoomFix.Y - Main.screenHeight / 2f);
		Vector2 mouseZoomFix = new Vector2(PlayerInput.MouseX, PlayerInput.MouseY);
		mouseZoomFix /= Main.UIScale;
		mouseZoomFix = mouseZoomFix.ToPoint().ToVector2();
		if (mouseZoomFix.Y > (Main.screenHeight - 24) / Main.UIScale)
		{
			mouseZoomFix.Y = (Main.screenHeight - 24) / Main.UIScale;
		}

		Vector2 pos = mouseZoomFix + new Vector2(-5, ypos);
		if (remainder > 0)
		{
			text += "" + remainderDenominator + "/" + remainderDenominator;
		}
		Vector2 posModified = new Vector2(mouseZoomFix.X - FontAssets.MouseText.Value.MeasureString(text).X, pos.Y);
		Vector2 pos3 = posModified;
		//float tempModScale = 0.4f;
		//var tempFont = FontAssets.DeathText.Value;
		//float tempStrength = 1.25f;
		//StringOutlineSmoothness tempSmooth = StringOutlineSmoothness.Vanilla;
		//float tempModScale = 1f;
		var tempFont = FontAssets.MouseText.Value;
		//float tempStrength = 1.4f;

		float mouseTextColorRemapped = Utils.Remap(Main.mouseTextColor, 0, 255, 60, 255) / 255f;
		float mouseTextColorRemappedBlue = Utils.Remap(Main.mouseTextColor, 190, 255, 0, 70) / 255f;
		Color color = new Color(mouseTextColorRemapped, mouseTextColorRemapped, mouseTextColorRemappedBlue, Main.mouseTextColor / 255f);

		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		if (bars > 0)
		{
			//DrawOutlinedString(spriteBatch, tempFont, $"{bars}", pos3, Color.Yellow, Color.Black, tempStrength, scale: 1f * tempModScale, outlineSmoothness: tempSmooth);

			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, tempFont, $"{bars}", pos3, color, 0f, default(Vector2), new Vector2(1f));
		}
		if (remainder > 0)
		{
			// position modifier for if the remainder (numerator) is 10
			// or higher, to shift it to the left a bit
			int xmod = -5;
			if (remainder > 9) xmod = -11;
			//pos3.X += FontAssets.MouseText.Value.MeasureString($"{bars}  ").X;
			string baseDenom = "/";
			foreach (var ch in remainderDenominator.ToString())
			{
				baseDenom += "11";
			}
			pos3.X += FontAssets.MouseText.Value.MeasureString(text).X - FontAssets.MouseText.Value.MeasureString(baseDenom).X;

			// draw the text
			//DrawOutlinedString(spriteBatch, tempFont, "/", pos3, Color.Yellow, Color.Black, tempStrength, scale: 1f * tempModScale, outlineSmoothness: tempSmooth);
			//DrawOutlinedString(spriteBatch, tempFont, $"{remainder}", pos3 + new Vector2(xmod, 0), Color.Yellow, Color.Black, tempStrength, scale: 0.6f * tempModScale, outlineSmoothness: tempSmooth);
			//DrawOutlinedString(spriteBatch, tempFont, $"{remainderDenominator}", pos3 + new Vector2(5, 10), Color.Yellow, Color.Black, tempStrength, scale: 0.6f * tempModScale, outlineSmoothness: tempSmooth);

			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, tempFont, "/", pos3, color, 0f, default, new Vector2(1f), spread: 1.4f);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, tempFont, $"{remainder}", pos3 + new Vector2(xmod, 0), color, 0f, default, new Vector2(0.6f), spread: 1.4f);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, tempFont, $"{remainderDenominator}", pos3 + new Vector2(5, 10), color, 0f, default, new Vector2(0.6f), spread: 1.4f);
		}
		spriteBatch.End();
		spriteBatch.Begin();

		// draw the sprite
		DrawOutlinedTexture(spriteBatch, TextureAssets.Item[barType].Value, posModified + new Vector2(FontAssets.MouseText.Value.MeasureString(text).X + 10, 0), Color.White);
		// ⅓
	}

	/// <summary>
	/// A helper method made by PoroCYon a very long time ago. Draws an outlined string (obviously).
	/// </summary>
	/// <param name="SB">The <see cref="SpriteBatch"/> to draw from.</param>
	/// <param name="SF">The <see cref="SpriteFont"/> to use.</param>
	/// <param name="txt">The text to draw.</param>
	/// <param name="P">The position to draw at.</param>
	/// <param name="C">The color to use for the interior of the text.</param>
	/// <param name="shadeC">The outline color.</param>
	/// <param name="strength">The thickness of the outline.</param>
	/// <param name="V">Unknown.</param>
	/// <param name="scale">The size of the text.</param>
	/// <param name="SE">The SpriteEffects to use.</param>
	/// <param name="LL">Unknown.</param>
	/// <param name="outlineSmoothness">
	/// Controls the smoothness of the outline.<br/>
	/// Vanilla will draw the outline offset by scale in each orthogonal direction.<br/>
	/// Diagonal will draw the outline offset by scale in each diagonal direction.<br/>
	/// Double will draw characters diagonally and orthogonally, with altered position of the diagonal characters to smooth the edges.<br/>
	/// Quad draws a character interpolated between each of the above.
	/// </param>
	private void DrawOutlinedString(SpriteBatch SB, DynamicSpriteFont SF, string txt, Vector2 P, Color C, Color shadeC, float strength = 1f, Vector2 V = default(Vector2), float scale = 1f, SpriteEffects SE = SpriteEffects.None, float LL = 0f, StringOutlineSmoothness outlineSmoothness = StringOutlineSmoothness.Vanilla)
	{
		SB.End();
		//SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
		SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt)) return;
		Vector2 up = new(0, -strength);
		Vector2 down = new(0, strength);
		Vector2 left = new(-strength, 0);
		Vector2 right = new(strength, 0);

		if (outlineSmoothness is not StringOutlineSmoothness.Diagonal or StringOutlineSmoothness.Vanilla) strength *= 0.85f;

		Vector2 upLeft = new(-strength, -strength);
		Vector2 upRight = new(strength, -strength);
		Vector2 downLeft = new(-strength, strength);
		Vector2 downRight = new(strength, strength);

		//Vector2[] OS = [upLeft, upRight, downLeft, downRight];
		Vector2[] OS = outlineSmoothness == StringOutlineSmoothness.Diagonal ? [upLeft, upRight, downLeft, downRight] : [up, down, left, right];
		if (outlineSmoothness >= StringOutlineSmoothness.Double)
		{
			OS = [up, upLeft * 0.85f, left, downLeft * 0.85f, down, downRight * 0.85f, right, upRight * 0.85f];

			if (outlineSmoothness == StringOutlineSmoothness.Quad)
			{
				Array.Resize(ref OS, OS.Length * 2);
				for (int i = OS.Length / 2; i < OS.Length; i++)
				{
					if (i == OS.Length - 1)
					{
						OS[i] = Vector2.Lerp(OS[i - 8], OS[0], 0.5f);
					}
					else
					{
						OS[i] = Vector2.Lerp(OS[i - 8], OS[i - 7], 0.5f);
					}
				}
			}
		}
		foreach (Vector2 VO in OS)
			DynamicSpriteFontExtensionMethods.DrawString(SB, SF, txt, new Vector2(P.X + VO.X, P.Y + VO.Y), shadeC, 0f, V, scale, SE, LL);
		DynamicSpriteFontExtensionMethods.DrawString(SB, SF, txt, P, C, 0f, V, scale, SE, LL);
		SB.End();
		SB.Begin();
	}
	private enum StringOutlineSmoothness : byte
	{
		Vanilla = 0,
		Diagonal = 4,
		Double = 8,
		Quad = 16
	}

	/// <summary>
	/// A helper method to draw an outlined texture.
	/// </summary>
	/// <param name="sb">The <see cref="SpriteBatch"/> to draw from.</param>
	/// <param name="tex">The texture to use.</param>
	/// <param name="pos">The position to draw the texture at.</param>
	/// <param name="color">The color to tint the texture.</param>
	private void DrawOutlinedTexture(SpriteBatch sb, Texture2D tex, Vector2 pos, Color color)
	{
		if (tex == null) return;

		// end the sprite batch and begin again to make it draw in the right position
		sb.End();
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

		// code yoinked from vanilla; this draws the outer black outline
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

		// end the spritebatch and begin again, using the shader this time (so it draws the sprite full white)
		sb.End();
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, ExxoAvalonOrigins.CalculatorSpectaclesEffect, Main.UIScaleMatrix);

		// code yoinked from vanilla; this draws the inner white outline 
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

		// end the sprite batch and begin again, so it draws normally without the shader now
		sb.End();
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

		// draw the actual texture with normal colors
		sb.Draw(tex, pos, color);
		sb.End();
		sb.Begin();
	}
}

public class CalcSpecPlayer : ModPlayer
{
	public bool CalcSpecDisplay;
	public bool CalculatorSpectacles;
	public bool ActuallyDisplay;
	public override void ResetEffects()
	{
		CalculatorSpectacles = false;
	}
	public override void ResetInfoAccessories()
	{
		CalcSpecDisplay = false;
	}
	public override void PostUpdate()
	{
		if (CalcSpecDisplay)
		{
			CalculatorSpectacles = true;
		}
		else CalculatorSpectacles = false;
	}
	public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
	{
		if (otherPlayer.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay)
		{
			CalcSpecDisplay = true;
		}
	}
}
