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
using Terraria.Localization;
using ThoriumMod.Tiles;
using Avalon.Tiles;
using System.Linq;

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
	//		.AddRecipeGroup("Avalon:SilverBar", 10)
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
					if (recipe.TryGetIngredient(item.type, out Item ing))
					{
						// if the recipe's required tile is a furnace, Hellforge, or Adamantite Forge(s), or result is is Life Quartz (Thorium)
						// set barType to the result's type and amtOfOre to the stack size of the ingredient
						// also set bars to item.stack
						if (recipe.requiredTile.Contains(TileID.Furnaces) ||
							recipe.requiredTile.Contains(TileID.Hellforge) ||
							recipe.requiredTile.Contains(TileID.AdamantiteForge) ||
							recipe.requiredTile.Contains(ModContent.TileType<CaesiumForge>()) ||
							item.type == ExxoAvalonOrigins.Thorium?.Find<ModItem>("LifeQuartz").Type)
						{
							// if the ingredient is Fallen Star or Glowing Mushroom, bypass it
							//if (ing.type is ItemID.FallenStar or ItemID.GlowingMushroom) continue;
							if (recipe.requiredItem.Count > 1 || item.createTile == -1 || !TileID.Sets.Ore[item.createTile]) continue;
							amtOfOre = ing.stack;
							barType = recipe.createItem.type;
							bars = item.stack;
							break;
						}
					}
				}
				else
				{
					foreach (int itemID in Data.Sets.Tile.VanillaOreTilesToBarItems.Values)
					{
						if (recipe.TryGetResult(itemID, out _))
						{
							if (Data.Sets.Item.VanillaBarItems[itemID])
							{
								if (recipe.TryGetIngredient(item.type, out Item ingr))
								{
									if (item.createTile == -1 || !TileID.Sets.Ore[item.createTile]) continue;
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
			Point tilepos = Main.LocalPlayer.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
			Color c = Lighting.GetColor(tilepos);

			if (TileID.Sets.Ore[Main.tile[tilepos.X, tilepos.Y].TileType]/* && Main.tile[tilepos.X, tilepos.Y].TileType != ModContent.TileType<SulphurOre>()*/ &&
				c.R > 5 && c.G > 5 && c.B > 5)
			{
				ushort type = Main.tile[tilepos.X, tilepos.Y].TileType;
				int bars = (int)CalculatorSpectacles.CountOres(tilepos, type, 700);
				int remainder = 0;
				int remainderDenominator = 3;

				// for some reason CountOres returns 0 if there's only 1 ore
				if (bars == 0) bars = 1;

				int barType = -1;

				// check manually for shroomite/bars
				//if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<ShroomiteOre>())
				//{
				//	remainder = bars % 5;
				//	bars /= 5;
				//	remainderDenominator = 5;
				//	barType = ItemID.ShroomiteBar;
				//}
				//// check manually for shroomite/bars
				//else if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<HallowedOre>())
				//{
				//	remainder = bars % 5;
				//	bars /= 5;
				//	remainderDenominator = 5;
				//	barType = ItemID.HallowedBar;
				//}
				//// check manually for heartstone/life crystals
				//else if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Heartstone>())
				//{
				//	remainder = bars % 45;
				//	bars /= 45;
				//	remainderDenominator = 45;
				//	barType = ItemID.LifeCrystal;
				//}
				//// check manually for starstone/mana crystals
				//else if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Starstone>())
				//{
				//	remainder = bars % 60;
				//	bars /= 60;
				//	remainderDenominator = 60;
				//	barType = ItemID.ManaCrystal;
				//}
				//// check manually for boltstone/stamina crystals
				//else if (Main.tile[tilepos.X, tilepos.Y].TileType == ModContent.TileType<Boltstone>())
				//{
				//	remainder = bars % 25;
				//	bars /= 25;
				//	remainderDenominator = 25;
				//	barType = ModContent.ItemType<StaminaCrystal>();
				//}
				//else
				//{
					// grab the modded tile at the cursor's position
					ModTile t = TileLoader.GetTile(Main.tile[tilepos.X, tilepos.Y].TileType);
					if (t != null)
					{
						// grab the drops of the tile
						var drops = t.GetItemDrops(tilepos.X, tilepos.Y);

						int amtOfOre = 0;
						// loop through the drops of the tile (will only loop once likely)
						foreach (Item item in drops)
						{
							// loop through all recipes
							foreach (Recipe recipe in Main.recipe)
							{
								// check if the recipe's ingredient matches the drop of the tile
								if (recipe.TryGetIngredient(item.type, out Item ing))
								{
									// if the recipe's result contains the word "Bar," set barType to the
									// result's type and amtOfOre to the stack size of the ingredient
									if (recipe.requiredTile.Contains(TileID.Furnaces) ||
										recipe.requiredTile.Contains(TileID.Hellforge) ||
										recipe.requiredTile.Contains(TileID.AdamantiteForge) ||
										recipe.requiredTile.Contains(ModContent.TileType<CaesiumForge>()) ||
										item.type == ExxoAvalonOrigins.Thorium?.Find<ModItem>("LifeQuartz").Type)
									{
										amtOfOre = ing.stack;
										barType = recipe.createItem.type;
										break;
									}
									
								}
							}
						}
						if (amtOfOre == 0) return;

						// assign all the things necessary to display the text/sprite later on
						remainder = bars % amtOfOre;
						bars /= amtOfOre;
						remainderDenominator = amtOfOre;
					}
					// if the tile is vanilla
					else if (Main.tile[tilepos.X, tilepos.Y].TileType < TileID.Count)
					{

						if (Data.Sets.Tile.ThreeOrePerBar.Contains(type))
						{
							remainder = bars % 3;
							bars /= 3;
							remainderDenominator = 3;
							barType = Data.Sets.Tile.VanillaOreTilesToBarItems[type];
						}
						else if (Data.Sets.Tile.FourOrePerBar.Contains(type))
						{
							remainder = bars % 4;
							bars /= 4;
							remainderDenominator = 4;
							barType = Data.Sets.Tile.VanillaOreTilesToBarItems[type];
						}
						else if (Data.Sets.Tile.FiveOrePerBar.Contains(type))
						{
							remainder = bars % 5;
							bars /= 5;
							remainderDenominator = 5;
							barType = Data.Sets.Tile.VanillaOreTilesToBarItems[type];
						}
						else return;
					}
				//}
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

				Vector2 pos = Main.MouseScreen + new Vector2(-5, ypos);
				if (remainder > 0)
				{
					text += "" + remainderDenominator + "/" + remainderDenominator;
				}
				Vector2 posModified = new Vector2(Main.MouseScreen.X - FontAssets.MouseText.Value.MeasureString(text).X, pos.Y);
				Vector2 pos3 = posModified;

				if (bars > 0)
				{
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, $"{bars}", pos3, Color.Yellow, Color.Black, 1.4f);
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
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, "/", pos3, Color.Yellow, Color.Black, 1.4f);
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, $"{remainder}", pos3 + new Vector2(xmod, 0), Color.Yellow, Color.Black, 1.4f, scale: 0.6f);
					DrawOutlinedString(spriteBatch, FontAssets.MouseText.Value, $"{remainderDenominator}", pos3 + new Vector2(5, 10), Color.Yellow, Color.Black, 1.4f, scale: 0.6f);
				}

				// draw the sprite
				DrawOutlinedTexture(spriteBatch, TextureAssets.Item[barType].Value, posModified + new Vector2(FontAssets.MouseText.Value.MeasureString(text).X + 10, 0), Color.White);
				// ⅓
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
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

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
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, ExxoAvalonOrigins.CalculatorSpectaclesEffect, Main.GameViewMatrix.ZoomMatrix);

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
		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

		// draw the actual texture with normal colors
		sb.Draw(tex, pos, color);
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
