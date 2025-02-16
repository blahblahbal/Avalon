using Avalon.Common;
using Avalon.Reflection;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Light;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	public class RetroBackgrounds : ModHook
	{
		protected override void Apply()
		{
			On_Main.DrawBackground += renderOldNewBackground;
			On_Main.DrawUnderworldBackground += stopRenderinRetro;
			On_TileLightScanner.ApplyHellLight += FixRetroHellLight;
			On_AmbientSky.HellBatsGoupSkyEntity.Helper_GetOpacityWithAccountingForBackgroundsOff += stopRenderingRetroBats;
		}

		private float stopRenderingRetroBats(On_AmbientSky.HellBatsGoupSkyEntity.orig_Helper_GetOpacityWithAccountingForBackgroundsOff orig, object self)
		{
			if (!AvalonWorld.retroWorld)
			{
				return orig.Invoke(self);
			}
			return 0f;
		}

		private void FixRetroHellLight(On_TileLightScanner.orig_ApplyHellLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
		{
			orig.Invoke(self, tile, x, y, ref lightColor);
			if (AvalonWorld.retroWorld)
				lightColor = Vector3.Zero;
		}

		private void stopRenderinRetro(On_Main.orig_DrawUnderworldBackground orig, Main self, bool flat)
		{
			if (!AvalonWorld.retroWorld)
			{
				orig.Invoke(self, flat);
			}
		}

		private void renderOldNewBackground(On_Main.orig_DrawBackground orig, Main self)
		{
			float shimmer = Main.shimmerAlpha;
			if (shimmer == 1f)
			{
				orig.Invoke(self);
				return;
			}
			if (AvalonWorld.retroWorld)
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				double bgParrallax = self.GetBackgroundParrallax();
				float caveParrallax = Main.caveParallax;
				int bgStart = self.GetBackgroundStartX();
				int bgLoops = self.GetBackgroundLoops();
				int bgTop =	self.GetBackgroundTopY();
				int bgLoopsY = self.GetBackgroundLoopsY();
				int bgStartY = self.GetBackgroundStartY();
				Asset<Texture2D>[] backgroundTexture = TextureAssets.Background;
				Color[] slices = (Color[])(object)new Color[9];
				self.LoadBackground(1);
				self.LoadBackground(2);
				self.LoadBackground(3);
				self.LoadBackground(4);
				self.LoadBackground(5);
				self.LoadBackground(6);

				int num = (int)(255f * (1f - Main.gfxQuality) + 140f * Main.gfxQuality);
				int num2 = (int)(200f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
				int num3 = 96;
				Vector2 vector = new Vector2(Main.offScreenRange, Main.offScreenRange);
				if (Main.drawToScreen)
				{
					vector = default(Vector2);
				}
				float num4;
				float num5;
				float num6 = (num5 = (num4 = 0.9f));
				float num7 = 0f;
				if (ModContent.GetInstance<BiomeTileCounts>().ContagionTiles > Main.SceneMetrics.EvilTileCount && ModContent.GetInstance<BiomeTileCounts>().ContagionTiles > Main.SceneMetrics.HolyTileCount && ModContent.GetInstance<BiomeTileCounts>().ContagionTiles > Main.SceneMetrics.BloodTileCount)
				{
					num7 = (float)ModContent.GetInstance<BiomeTileCounts>().ContagionTiles / 800;
				}
				else if (Main.SceneMetrics.BloodTileCount > Main.SceneMetrics.EvilTileCount && Main.SceneMetrics.BloodTileCount > Main.SceneMetrics.HolyTileCount)
				{
					num7 = (float)Main.SceneMetrics.BloodTileCount / 800;
				}
				else if (Main.SceneMetrics.HolyTileCount > Main.SceneMetrics.EvilTileCount)
				{
					num7 = (float)Main.SceneMetrics.HolyTileCount / 800f;
				}
				else if (Main.SceneMetrics.EvilTileCount > Main.SceneMetrics.HolyTileCount)
				{
					num7 = (float)Main.SceneMetrics.EvilTileCount / 800f;
				}
				if (num7 > 1f)
				{
					num7 = 1f;
				}
				if (num7 < 0f)
				{
					num7 = 0f;
				}
				float num8 = (float)((double)Main.screenPosition.Y - Main.worldSurface * 16.0) / 300f;
				if (num8 < 0f)
				{
					num8 = 0f;
				}
				else if (num8 > 1f)
				{
					num8 = 1f;
				}
				float num9 = 1f * (1f - num8) + num4 * num8;
				float globalBrightness = Lighting.GlobalBrightness;
				Lighting.GlobalBrightness = globalBrightness * (1f - num8) + 1f * num8;
				//Lighting.brightness = Lighting.defBrightness * (1f - num8) + 1f * num8;
				float num10 = (float)((double)(Main.screenPosition.Y - (float)(Main.screenHeight / 2) + 200f) - Main.rockLayer * 16.0) / 300f;
				if (num10 < 0f)
				{
					num10 = 0f;
				}
				else if (num10 > 1f)
				{
					num10 = 1f;
				}
				if (ModContent.GetInstance<BiomeTileCounts>().ContagionTiles > Main.SceneMetrics.EvilTileCount && ModContent.GetInstance<BiomeTileCounts>().ContagionTiles > Main.SceneMetrics.HolyTileCount && ModContent.GetInstance<BiomeTileCounts>().ContagionTiles > Main.SceneMetrics.BloodTileCount)
				{
					num4 = 0.54f * num7 + num4 * (1f - num7);
					num5 = 0.78f * num7 + num5 * (1f - num7);
					num6 = 0.62f * num7 + num6 * (1f - num7);
				}
				else if (Main.SceneMetrics.BloodTileCount > Main.SceneMetrics.EvilTileCount && Main.SceneMetrics.BloodTileCount > Main.SceneMetrics.HolyTileCount)
				{
					num4 = 1f * num7 + num4 * (1f - num7);
					num5 = 0.55f * num7 + num5 * (1f - num7);
					num6 = 0.5f * num7 + num6 * (1f - num7);
				}
				else if (Main.SceneMetrics.EvilTileCount > 0)
				{
					num4 = 0.8f * num7 + num4 * (1f - num7);
					num5 = 0.75f * num7 + num5 * (1f - num7);
					num6 = 1.1f * num7 + num6 * (1f - num7);
				}
				else if (Main.SceneMetrics.HolyTileCount > 0)
				{
					num4 = 1f * num7 + num4 * (1f - num7);
					num5 = 0.7f * num7 + num5 * (1f - num7);
					num6 = 0.9f * num7 + num6 * (1f - num7);
				}
				num4 = 1f * (num9 - num10) + num4 * num10;
				num5 = 1f * (num9 - num10) + num5 * num10;
				num6 = 1f * (num9 - num10) + num6 * num10;
				globalBrightness = 1.2f * (1f - num10) + 1f * num10;
				//Lighting.defBrightness = 1.2f * (1f - num10) + 1f * num10;
				bgParrallax = caveParrallax;
				bgStart = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParrallax, num3) - (double)(num3 / 2));
				bgLoops = Main.screenWidth / num3 + 2;
				bgTop = (int)((float)((int)Main.worldSurface * 16 - Main.backgroundHeight[1]) - Main.screenPosition.Y + 16f);
				for (int i = 0; i < bgLoops; i++)
				{
					for (int j = 0; j < 6; j++)
					{
						int num11 = (int)(float)Math.Round(0f - (float)Math.IEEERemainder((float)bgStart + Main.screenPosition.X, 16.0));
						if (num11 == -8)
						{
							num11 = 8;
						}
						float num12 = bgStart + num3 * i + j * 16 + 8;
						float num13 = bgTop;
						Color color = Lighting.GetColor((int)((num12 + Main.screenPosition.X) / 16f), (int)((Main.screenPosition.Y + num13) / 16f));
						color.R = (byte)((float)(int)color.R * num4);
						color.G = (byte)((float)(int)color.G * num5);
						color.B = (byte)((float)(int)color.B * num6);
						Main.spriteBatch.Draw(backgroundTexture[1].Value, new Vector2(bgStart + num3 * i + 16 * j + num11, bgTop) + vector, new Rectangle(16 * j + num11 + 16, 0, 16, 16), color);
					}
				}
				double num14 = Main.maxTilesY - 230;
				double num15 = (int)((num14 - Main.worldSurface) / 6.0) * 6;
				num14 = Main.worldSurface + num15 - 5.0;
				bool flag = false;
				bool flag2 = false;
				bgTop = (int)((float)((int)Main.worldSurface * 16) - Main.screenPosition.Y + 16f);
				if (Main.worldSurface * 16.0 <= (double)(Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.offScreenRange))
				{
					bgParrallax = caveParrallax;
					bgStart = (int)(0.0 - Math.IEEERemainder(96.0 + (double)Main.screenPosition.X * bgParrallax, num3) - (double)(num3 / 2)) - (int)vector.X;
					bgLoops = (Main.screenWidth + (int)vector.X * 2) / num3 + 2;
					if (Main.worldSurface * 16.0 < (double)(Main.screenPosition.Y - 16f))
					{
						bgStartY = (int)(Math.IEEERemainder(bgTop, Main.backgroundHeight[2]) - (double)Main.backgroundHeight[2]);
						bgLoopsY = (Main.screenHeight - bgStartY + (int)vector.Y * 2) / Main.backgroundHeight[2] + 1;
					}
					else
					{
						bgStartY = bgTop;
						bgLoopsY = (Main.screenHeight - bgTop + (int)vector.Y * 2) / Main.backgroundHeight[2] + 1;
					}
					if (Main.rockLayer * 16.0 < (double)(Main.screenPosition.Y + 600f))
					{
						bgLoopsY = (int)(Main.rockLayer * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)bgStartY) / Main.backgroundHeight[2];
						flag2 = true;
					}
					int num16 = (int)(float)Math.Round(0f - (float)Math.IEEERemainder((float)bgStart + Main.screenPosition.X, 16.0));
					if (num16 == -8)
					{
						num16 = 8;
					}
					for (int k = 0; k < bgLoops; k++)
					{
						for (int l = 0; l < bgLoopsY; l++)
						{
							for (int m = 0; m < 6; m++)
							{
								for (int n = 0; n < 6; n++)
								{
									float num17 = bgStartY + l * 96 + n * 16 + 8;
									int num18 = (int)(((float)(bgStart + num3 * k + m * 16 + 8) + Main.screenPosition.X) / 16f);
									int num19 = (int)((num17 + Main.screenPosition.Y) / 16f);
									Color color2 = Lighting.GetColor(num18, num19);
									if (color2.R > 0 || color2.G > 0 || color2.B > 0)
									{
										if ((color2.R > num || (double)(int)color2.G > (double)num * 1.1 || (double)(int)color2.B > (double)num * 1.2) && !Main.tile[num18, num19].HasTile && (Main.tile[num18, num19].WallType == 0 || Main.tile[num18, num19].WallType == 21))
										{
											try
											{
												for (int num20 = 0; num20 < 9; num20++)
												{
													int num21 = 0;
													int num22 = 0;
													int width = 4;
													int height = 4;
													Color color3 = color2;
													Color color4 = color2;
													if (num20 == 0 && !Main.tile[num18 - 1, num19 - 1].HasTile)
													{
														color4 = Lighting.GetColor(num18 - 1, num19 - 1);
													}
													if (num20 == 1)
													{
														width = 8;
														num21 = 4;
														if (!Main.tile[num18, num19 - 1].HasTile)
														{
															color4 = Lighting.GetColor(num18, num19 - 1);
														}
													}
													if (num20 == 2)
													{
														if (!Main.tile[num18 + 1, num19 - 1].HasTile)
														{
															color4 = Lighting.GetColor(num18 + 1, num19 - 1);
														}
														num21 = 12;
													}
													if (num20 == 3)
													{
														if (!Main.tile[num18 - 1, num19].HasTile)
														{
															color4 = Lighting.GetColor(num18 - 1, num19);
														}
														height = 8;
														num22 = 4;
													}
													if (num20 == 4)
													{
														width = 8;
														height = 8;
														num21 = 4;
														num22 = 4;
													}
													if (num20 == 5)
													{
														num21 = 12;
														num22 = 4;
														height = 8;
														if (!Main.tile[num18 + 1, num19].HasTile)
														{
															color4 = Lighting.GetColor(num18 + 1, num19);
														}
													}
													if (num20 == 6)
													{
														if (!Main.tile[num18 - 1, num19 + 1].HasTile)
														{
															color4 = Lighting.GetColor(num18 - 1, num19 + 1);
														}
														num22 = 12;
													}
													if (num20 == 7)
													{
														width = 8;
														height = 4;
														num21 = 4;
														num22 = 12;
														if (!Main.tile[num18, num19 + 1].HasTile)
														{
															color4 = Lighting.GetColor(num18, num19 + 1);
														}
													}
													if (num20 == 8)
													{
														if (!Main.tile[num18 + 1, num19 + 1].HasTile)
														{
															color4 = Lighting.GetColor(num18 + 1, num19 + 1);
														}
														num21 = 12;
														num22 = 12;
													}
													color3.R = (byte)((color2.R + color4.R) / 2);
													color3.G = (byte)((color2.G + color4.G) / 2);
													color3.B = (byte)((color2.B + color4.B) / 2);
													color3.R = (byte)((float)(int)color3.R * num4);
													color3.G = (byte)((float)(int)color3.G * num5);
													color3.B = (byte)((float)(int)color3.B * num6);
													Main.spriteBatch.Draw(backgroundTexture[2].Value, new Vector2(bgStart + num3 * k + 16 * m + num21 + num16, bgStartY + Main.backgroundHeight[2] * l + 16 * n + num22) + vector, new Rectangle(16 * m + num21 + num16 + 16, 16 * n + num22, width, height), color3);
												}
											}
											catch
											{
												color2.R = (byte)((float)(int)color2.R * num4);
												color2.G = (byte)((float)(int)color2.G * num5);
												color2.B = (byte)((float)(int)color2.B * num6);
												Main.spriteBatch.Draw(backgroundTexture[2].Value, new Vector2(bgStart + num3 * k + 16 * m + num16, bgStartY + Main.backgroundHeight[2] * l + 16 * n) + vector, new Rectangle(16 * m + num16 + 16, 16 * n, 16, 16), color2);
											}
										}
										else if (color2.R > num2 || (double)(int)color2.G > (double)num2 * 1.1 || (double)(int)color2.B > (double)num2 * 1.2)
										{
											Lighting.GetColor4Slice(num18, num19, ref slices);
											for (int num23 = 0; num23 < 4; num23++)
											{
												int num24 = 0;
												int num25 = 0;
												Color color5 = color2;
												Color color6 = slices[num23];
												if (num23 == 0)
												{
													//color6 = ((!Lighting.Brighter(num18, num19 - 1, num18 - 1, num19)) ? Lighting.GetColor(num18, num19 - 1) : Lighting.GetColor(num18 - 1, num19)); //replaced with the GetColor4Slice
												}
												if (num23 == 1)
												{
													//color6 = ((!Lighting.Brighter(num18, num19 - 1, num18 + 1, num19)) ? Lighting.GetColor(num18, num19 - 1) : Lighting.GetColor(num18 + 1, num19));
													num24 = 8;
												}
												if (num23 == 2)
												{
													//color6 = ((!Lighting.Brighter(num18, num19 + 1, num18 - 1, num19)) ? Lighting.GetColor(num18, num19 + 1) : Lighting.GetColor(num18 - 1, num19));
													num25 = 8;
												}
												if (num23 == 3)
												{
													//color6 = ((!Lighting.Brighter(num18, num19 + 1, num18 + 1, num19)) ? Lighting.GetColor(num18, num19 + 1) : Lighting.GetColor(num18 + 1, num19));
													num24 = 8;
													num25 = 8;
												}
												color5.R = (byte)((color2.R + color6.R) / 2);
												color5.G = (byte)((color2.G + color6.G) / 2);
												color5.B = (byte)((color2.B + color6.B) / 2);
												color5.R = (byte)((float)(int)color5.R * num4);
												color5.G = (byte)((float)(int)color5.G * num5);
												color5.B = (byte)((float)(int)color5.B * num6);
												Main.spriteBatch.Draw(backgroundTexture[2].Value, new Vector2(bgStart + num3 * k + 16 * m + num24 + num16, bgStartY + Main.backgroundHeight[2] * l + 16 * n + num25) + vector, new Rectangle(16 * m + num24 + num16 + 16, 16 * n + num25, 8, 8), color5);
											}
										}
										else
										{
											color2.R = (byte)((float)(int)color2.R * num4);
											color2.G = (byte)((float)(int)color2.G * num5);
											color2.B = (byte)((float)(int)color2.B * num6);
											Main.spriteBatch.Draw(backgroundTexture[2].Value, new Vector2(bgStart + num3 * k + 16 * m + num16, bgStartY + Main.backgroundHeight[2] * l + 16 * n) + vector, new Rectangle(16 * m + num16 + 16, 16 * n, 16, 16), color2);
										}
									}
									else
									{
										color2.R = (byte)((float)(int)color2.R * num4);
										color2.G = (byte)((float)(int)color2.G * num5);
										color2.B = (byte)((float)(int)color2.B * num6);
										Main.spriteBatch.Draw(backgroundTexture[2].Value, new Vector2(bgStart + num3 * k + 16 * m + num16, bgStartY + Main.backgroundHeight[2] * l + 16 * n) + vector, new Rectangle(16 * m + num16 + 16, 16 * n, 16, 16), color2);
									}
								}
							}
						}
					}
					if (flag2)
					{
						bgParrallax = caveParrallax;
						bgStart = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParrallax, num3) - (double)(num3 / 2));
						bgLoops = (Main.screenWidth + (int)vector.X * 2) / num3 + 2;
						bgTop = bgStartY + bgLoopsY * Main.backgroundHeight[2];
						if (bgTop > -32)
						{
							for (int num26 = 0; num26 < bgLoops; num26++)
							{
								for (int num27 = 0; num27 < 6; num27++)
								{
									float num28 = bgStart + num3 * num26 + num27 * 16 + 8;
									float num29 = bgTop;
									Color color7 = Lighting.GetColor((int)((num28 + Main.screenPosition.X) / 16f), (int)((Main.screenPosition.Y + num29) / 16f));
									color7.R = (byte)((float)(int)color7.R * num4);
									color7.G = (byte)((float)(int)color7.G * num5);
									color7.B = (byte)((float)(int)color7.B * num6);
									Main.spriteBatch.Draw(backgroundTexture[4].Value, new Vector2(bgStart + num3 * num26 + 16 * num27 + num16, bgTop) + vector, new Rectangle(16 * num27 + num16 + 16, 0, 16, 16), color7);
								}
							}
						}
					}
				}
				bgTop = (int)((float)((int)Main.rockLayer * 16) - Main.screenPosition.Y + 16f + 600f - 8f);
				if (Main.rockLayer * 16.0 <= (double)(Main.screenPosition.Y + 600f))
				{
					bgParrallax = caveParrallax;
					bgStart = (int)(0.0 - Math.IEEERemainder(96.0 + (double)Main.screenPosition.X * bgParrallax, num3) - (double)(num3 / 2)) - (int)vector.X;
					bgLoops = (Main.screenWidth + (int)vector.X * 2) / num3 + 2;
					if (Main.rockLayer * 16.0 + (double)Main.screenHeight < (double)(Main.screenPosition.Y - 16f))
					{
						bgStartY = (int)(Math.IEEERemainder(bgTop, Main.backgroundHeight[3]) - (double)Main.backgroundHeight[3]);
						bgLoopsY = (Main.screenHeight - bgStartY + (int)vector.Y * 2) / Main.backgroundHeight[2] + 1;
					}
					else
					{
						bgStartY = bgTop;
						bgLoopsY = (Main.screenHeight - bgTop + (int)vector.Y * 2) / Main.backgroundHeight[2] + 1;
					}
					if (num14 * 16.0 < (double)(Main.screenPosition.Y + 600f))
					{
						bgLoopsY = (int)(num14 * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)bgStartY) / Main.backgroundHeight[2];
						flag = true;
					}
					int num30 = (int)(float)Math.Round(0f - (float)Math.IEEERemainder((float)bgStart + Main.screenPosition.X, 16.0));
					if (num30 == -8)
					{
						num30 = 8;
					}
					for (int num31 = 0; num31 < bgLoops; num31++)
					{
						for (int num32 = 0; num32 < bgLoopsY; num32++)
						{
							for (int num33 = 0; num33 < 6; num33++)
							{
								for (int num34 = 0; num34 < 6; num34++)
								{
									float num35 = bgStartY + num32 * 96 + num34 * 16 + 8;
									int num36 = (int)(((float)(bgStart + num3 * num31 + num33 * 16 + 8) + Main.screenPosition.X) / 16f);
									int num37 = (int)((num35 + Main.screenPosition.Y) / 16f);
									Color color8 = Lighting.GetColor(num36, num37);
									bool flag3 = false;
									if (caveParrallax != 0f)
									{
										if (Main.tile[num36, num37].WallType == 0 || Main.tile[num36, num37].WallType == 21 || Main.tile[num36 - 1, num37].WallType == 0 || Main.tile[num36 - 1, num37].WallType == 21 || Main.tile[num36 + 1, num37].WallType == 0 || Main.tile[num36 + 1, num37].WallType == 21)
										{
											flag3 = true;
										}
									}
									else if (Main.tile[num36, num37].WallType == 0 || Main.tile[num36, num37].WallType == 21)
									{
										flag3 = true;
									}
									if ((!flag3 && color8.R != 0 && color8.G != 0 && color8.B != 0) || (color8.R <= 0 && color8.G <= 0 && color8.B <= 0) || (Main.tile[num36, num37].WallType != 0 && Main.tile[num36, num37].WallType != 21 && caveParrallax == 0f))
									{
										continue;
									}
									if (Lighting.NotRetro && color8.R < 230 && color8.G < 230 && color8.B < 230)
									{
										if ((color8.R > num || (double)(int)color8.G > (double)num * 1.1 || (double)(int)color8.B > (double)num * 1.2) && !Main.tile[num36, num37].HasTile)
										{
											for (int num38 = 0; num38 < 9; num38++)
											{
												int num39 = 0;
												int num40 = 0;
												int width2 = 4;
												int height2 = 4;
												Color color9 = color8;
												Color color10 = color8;
												if (num38 == 0 && !Main.tile[num36 - 1, num37 - 1].HasTile)
												{
													color10 = Lighting.GetColor(num36 - 1, num37 - 1);
												}
												if (num38 == 1)
												{
													width2 = 8;
													num39 = 4;
													if (!Main.tile[num36, num37 - 1].HasTile)
													{
														color10 = Lighting.GetColor(num36, num37 - 1);
													}
												}
												if (num38 == 2)
												{
													if (!Main.tile[num36 + 1, num37 - 1].HasTile)
													{
														color10 = Lighting.GetColor(num36 + 1, num37 - 1);
													}
													num39 = 12;
												}
												if (num38 == 3)
												{
													if (!Main.tile[num36 - 1, num37].HasTile)
													{
														color10 = Lighting.GetColor(num36 - 1, num37);
													}
													height2 = 8;
													num40 = 4;
												}
												if (num38 == 4)
												{
													width2 = 8;
													height2 = 8;
													num39 = 4;
													num40 = 4;
												}
												if (num38 == 5)
												{
													num39 = 12;
													num40 = 4;
													height2 = 8;
													if (!Main.tile[num36 + 1, num37].HasTile)
													{
														color10 = Lighting.GetColor(num36 + 1, num37);
													}
												}
												if (num38 == 6)
												{
													if (!Main.tile[num36 - 1, num37 + 1].HasTile)
													{
														color10 = Lighting.GetColor(num36 - 1, num37 + 1);
													}
													num40 = 12;
												}
												if (num38 == 7)
												{
													width2 = 8;
													height2 = 4;
													num39 = 4;
													num40 = 12;
													if (!Main.tile[num36, num37 + 1].HasTile)
													{
														color10 = Lighting.GetColor(num36, num37 + 1);
													}
												}
												if (num38 == 8)
												{
													if (!Main.tile[num36 + 1, num37 + 1].HasTile)
													{
														color10 = Lighting.GetColor(num36 + 1, num37 + 1);
													}
													num39 = 12;
													num40 = 12;
												}
												color9.R = (byte)((color8.R + color10.R) / 2);
												color9.G = (byte)((color8.G + color10.G) / 2);
												color9.B = (byte)((color8.B + color10.B) / 2);
												color9.R = (byte)((float)(int)color9.R * num4);
												color9.G = (byte)((float)(int)color9.G * num5);
												color9.B = (byte)((float)(int)color9.B * num6);
												Main.spriteBatch.Draw(backgroundTexture[3].Value, new Vector2(bgStart + num3 * num31 + 16 * num33 + num39 + num30, bgStartY + Main.backgroundHeight[2] * num32 + 16 * num34 + num40) + vector, new Rectangle(16 * num33 + num39 + num30 + 16, 16 * num34 + num40, width2, height2), color9);
											}
										}
										else if (color8.R > num2 || (double)(int)color8.G > (double)num2 * 1.1 || (double)(int)color8.B > (double)num2 * 1.2)
										{
											Lighting.GetColor4Slice(num36, num37, ref slices);
											for (int num41 = 0; num41 < 4; num41++)
											{
												int num42 = 0;
												int num43 = 0;
												Color color11 = color8;
												Color color12 = slices[num41];
												if (num41 == 0)
												{
													//color12 = ((!Lighting.Brighter(num36, num37 - 1, num36 - 1, num37)) ? Lighting.GetColor(num36, num37 - 1) : Lighting.GetColor(num36 - 1, num37)); //same as the previous slices point
												}
												if (num41 == 1)
												{
													//color12 = ((!Lighting.Brighter(num36, num37 - 1, num36 + 1, num37)) ? Lighting.GetColor(num36, num37 - 1) : Lighting.GetColor(num36 + 1, num37));
													num42 = 8;
												}
												if (num41 == 2)
												{
													//color12 = ((!Lighting.Brighter(num36, num37 + 1, num36 - 1, num37)) ? Lighting.GetColor(num36, num37 + 1) : Lighting.GetColor(num36 - 1, num37));
													num43 = 8;
												}
												if (num41 == 3)
												{
													//color12 = ((!Lighting.Brighter(num36, num37 + 1, num36 + 1, num37)) ? Lighting.GetColor(num36, num37 + 1) : Lighting.GetColor(num36 + 1, num37));
													num42 = 8;
													num43 = 8;
												}
												color11.R = (byte)((color8.R + color12.R) / 2);
												color11.G = (byte)((color8.G + color12.G) / 2);
												color11.B = (byte)((color8.B + color12.B) / 2);
												color11.R = (byte)((float)(int)color11.R * num4);
												color11.G = (byte)((float)(int)color11.G * num5);
												color11.B = (byte)((float)(int)color11.B * num6);
												Main.spriteBatch.Draw(backgroundTexture[3].Value, new Vector2(bgStart + num3 * num31 + 16 * num33 + num42 + num30, bgStartY + Main.backgroundHeight[2] * num32 + 16 * num34 + num43) + vector, new Rectangle(16 * num33 + num42 + num30 + 16, 16 * num34 + num43, 8, 8), color11);
											}
										}
										else
										{
											color8.R = (byte)((float)(int)color8.R * num4);
											color8.G = (byte)((float)(int)color8.G * num5);
											color8.B = (byte)((float)(int)color8.B * num6);
											Main.spriteBatch.Draw(backgroundTexture[3].Value, new Vector2(bgStart + num3 * num31 + 16 * num33 + num30, bgStartY + Main.backgroundHeight[2] * num32 + 16 * num34) + vector, new Rectangle(16 * num33 + num30 + 16, 16 * num34, 16, 16), color8);
										}
									}
									else
									{
										color8.R = (byte)((float)(int)color8.R * num4);
										color8.G = (byte)((float)(int)color8.G * num5);
										color8.B = (byte)((float)(int)color8.B * num6);
										Main.spriteBatch.Draw(backgroundTexture[3].Value, new Vector2(bgStart + num3 * num31 + 16 * num33 + num30, bgStartY + Main.backgroundHeight[2] * num32 + 16 * num34) + vector, new Rectangle(16 * num33 + num30 + 16, 16 * num34, 16, 16), color8);
									}
								}
							}
						}
					}
					if (flag)
					{
						bgParrallax = caveParrallax;
						bgStart = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParrallax, num3) - (double)(num3 / 2));
						bgLoops = Main.screenWidth / num3 + 2;
						bgTop = bgStartY + bgLoopsY * Main.backgroundHeight[2];
						for (int num44 = 0; num44 < bgLoops; num44++)
						{
							for (int num45 = 0; num45 < 6; num45++)
							{
								float num46 = bgStart + num3 * num44 + num45 * 16 + 8;
								float num47 = bgTop;
								Color color13 = Lighting.GetColor((int)((num46 + Main.screenPosition.X) / 16f), (int)((Main.screenPosition.Y + num47) / 16f));
								color13.R = (byte)((float)(int)color13.R * num4);
								color13.G = (byte)((float)(int)color13.G * num5);
								color13.B = (byte)((float)(int)color13.B * num6);
								Main.spriteBatch.Draw(backgroundTexture[6].Value, new Vector2(bgStart + num3 * num44 + 16 * num45 + num30, bgTop) + vector, new Rectangle(16 * num45 + num30 + 16, Main.magmaBGFrame * 16, 16, 16), color13);
							}
						}
					}
				}
				bgTop = (int)((float)((int)num14 * 16) - Main.screenPosition.Y + 16f + 600f) - 8;
				if (num14 * 16.0 <= (double)(Main.screenPosition.Y + 600f))
				{
					bgStart = (int)(0.0 - Math.IEEERemainder(96.0 + (double)Main.screenPosition.X * bgParrallax, num3) - (double)(num3 / 2)) - (int)vector.X;
					bgLoops = (Main.screenWidth + (int)vector.X * 2) / num3 + 2;
					if (num14 * 16.0 + (double)Main.screenHeight < (double)(Main.screenPosition.Y - 16f))
					{
						bgStartY = (int)(Math.IEEERemainder(bgTop, Main.backgroundHeight[2]) - (double)Main.backgroundHeight[2]);
						bgLoopsY = (Main.screenHeight - bgStartY + (int)vector.Y * 2) / Main.backgroundHeight[2] + 1;
					}
					else
					{
						bgStartY = bgTop;
						bgLoopsY = (Main.screenHeight - bgTop + (int)vector.Y * 2) / Main.backgroundHeight[2] + 1;
					}
					num = (int)((double)num * 1.5);
					num2 = (int)((double)num2 * 1.5);
					int num48 = (int)(float)Math.Round(0f - (float)Math.IEEERemainder((float)bgStart + Main.screenPosition.X, 16.0));
					if (num48 == -8)
					{
						num48 = 8;
					}
					for (int num49 = 0; num49 < bgLoops; num49++)
					{
						for (int num50 = 0; num50 < bgLoopsY; num50++)
						{
							for (int num51 = 0; num51 < 6; num51++)
							{
								for (int num52 = 0; num52 < 6; num52++)
								{
									float num53 = bgStartY + num50 * 96 + num52 * 16 + 8;
									int num54 = (int)(((float)(bgStart + num3 * num49 + num51 * 16 + 8) + Main.screenPosition.X) / 16f);
									int num55 = (int)((num53 + Main.screenPosition.Y) / 16f);
									Color color14 = Lighting.GetColor(num54, num55);
									bool flag4 = false;
									if (caveParrallax != 0f)
									{
										if (Main.tile[num54, num55].WallType == 0 || Main.tile[num54, num55].WallType == 21 || Main.tile[num54 - 1, num55].WallType == 0 || Main.tile[num54 - 1, num55].WallType == 21 || Main.tile[num54 + 1, num55].WallType == 0 || Main.tile[num54 + 1, num55].WallType == 21)
										{
											flag4 = true;
										}
									}
									else if (Main.tile[num54, num55].WallType == 0 || Main.tile[num54, num55].WallType == 21)
									{
										flag4 = true;
									}
									if ((!flag4 && color14.R != 0 && color14.G != 0 && color14.B != 0) || (color14.R <= 0 && color14.G <= 0 && color14.B <= 0) || (Main.tile[num54, num55].WallType != 0 && Main.tile[num54, num55].WallType != 21 && caveParrallax == 0f))
									{
										continue;
									}
									if (Lighting.NotRetro && color14.R < 230 && color14.G < 230 && color14.B < 230)
									{
										if ((color14.R > num || (double)(int)color14.G > (double)num * 1.1 || (double)(int)color14.B > (double)num * 1.2) && !Main.tile[num54, num55].HasTile)
										{
											for (int num56 = 0; num56 < 9; num56++)
											{
												int num57 = 0;
												int num58 = 0;
												int width3 = 4;
												int height3 = 4;
												Color color15 = color14;
												Color color16 = color14;
												if (num56 == 0 && !Main.tile[num54 - 1, num55 - 1].HasTile)
												{
													color16 = Lighting.GetColor(num54 - 1, num55 - 1);
												}
												if (num56 == 1)
												{
													width3 = 8;
													num57 = 4;
													if (!Main.tile[num54, num55 - 1].HasTile)
													{
														color16 = Lighting.GetColor(num54, num55 - 1);
													}
												}
												if (num56 == 2)
												{
													if (!Main.tile[num54 + 1, num55 - 1].HasTile)
													{
														color16 = Lighting.GetColor(num54 + 1, num55 - 1);
													}
													num57 = 12;
												}
												if (num56 == 3)
												{
													if (!Main.tile[num54 - 1, num55].HasTile)
													{
														color16 = Lighting.GetColor(num54 - 1, num55);
													}
													height3 = 8;
													num58 = 4;
												}
												if (num56 == 4)
												{
													width3 = 8;
													height3 = 8;
													num57 = 4;
													num58 = 4;
												}
												if (num56 == 5)
												{
													num57 = 12;
													num58 = 4;
													height3 = 8;
													if (!Main.tile[num54 + 1, num55].HasTile)
													{
														color16 = Lighting.GetColor(num54 + 1, num55);
													}
												}
												if (num56 == 6)
												{
													if (!Main.tile[num54 - 1, num55 + 1].HasTile)
													{
														color16 = Lighting.GetColor(num54 - 1, num55 + 1);
													}
													num58 = 12;
												}
												if (num56 == 7)
												{
													width3 = 8;
													height3 = 4;
													num57 = 4;
													num58 = 12;
													if (!Main.tile[num54, num55 + 1].HasTile)
													{
														color16 = Lighting.GetColor(num54, num55 + 1);
													}
												}
												if (num56 == 8)
												{
													if (!Main.tile[num54 + 1, num55 + 1].HasTile)
													{
														color16 = Lighting.GetColor(num54 + 1, num55 + 1);
													}
													num57 = 12;
													num58 = 12;
												}
												color15.R = (byte)((color14.R + color16.R) / 2);
												color15.G = (byte)((color14.G + color16.G) / 2);
												color15.B = (byte)((color14.B + color16.B) / 2);
												color15.R = (byte)((float)(int)color15.R * num4);
												color15.G = (byte)((float)(int)color15.G * num5);
												color15.B = (byte)((float)(int)color15.B * num6);
												Main.spriteBatch.Draw(backgroundTexture[5].Value, new Vector2(bgStart + num3 * num49 + 16 * num51 + num57 + num48, bgStartY + Main.backgroundHeight[2] * num50 + 16 * num52 + num58) + vector, new Rectangle(16 * num51 + num57 + num48 + 16, 16 * num52 + Main.backgroundHeight[2] * Main.magmaBGFrame + num58, width3, height3), color15, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
											}
										}
										else if (color14.R > num2 || (double)(int)color14.G > (double)num2 * 1.1 || (double)(int)color14.B > (double)num2 * 1.2)
										{
											Lighting.GetColor4Slice(num54, num55, ref slices);
											for (int num59 = 0; num59 < 4; num59++)
											{
												int num60 = 0;
												int num61 = 0;
												Color color17 = color14;
												Color color18 = slices[num59];
												if (num59 == 0)
												{
													//color18 = ((!Lighting.Brighter(num54, num55 - 1, num54 - 1, num55)) ? Lighting.GetColor(num54, num55 - 1) : Lighting.GetColor(num54 - 1, num55));
												}
												if (num59 == 1)
												{
													//color18 = ((!Lighting.Brighter(num54, num55 - 1, num54 + 1, num55)) ? Lighting.GetColor(num54, num55 - 1) : Lighting.GetColor(num54 + 1, num55));
													num60 = 8;
												}
												if (num59 == 2)
												{
													//color18 = ((!Lighting.Brighter(num54, num55 + 1, num54 - 1, num55)) ? Lighting.GetColor(num54, num55 + 1) : Lighting.GetColor(num54 - 1, num55));
													num61 = 8;
												}
												if (num59 == 3)
												{
													//color18 = ((!Lighting.Brighter(num54, num55 + 1, num54 + 1, num55)) ? Lighting.GetColor(num54, num55 + 1) : Lighting.GetColor(num54 + 1, num55));
													num60 = 8;
													num61 = 8;
												}
												color17.R = (byte)((color14.R + color18.R) / 2);
												color17.G = (byte)((color14.G + color18.G) / 2);
												color17.B = (byte)((color14.B + color18.B) / 2);
												color17.R = (byte)((float)(int)color17.R * num4);
												color17.G = (byte)((float)(int)color17.G * num5);
												color17.B = (byte)((float)(int)color17.B * num6);
												Main.spriteBatch.Draw(backgroundTexture[5].Value, new Vector2(bgStart + num3 * num49 + 16 * num51 + num60 + num48, bgStartY + Main.backgroundHeight[2] * num50 + 16 * num52 + num61) + vector, new Rectangle(16 * num51 + num60 + num48 + 16, 16 * num52 + Main.backgroundHeight[2] * Main.magmaBGFrame + num61, 8, 8), color17, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
											}
										}
										else
										{
											color14.R = (byte)((float)(int)color14.R * num4);
											color14.G = (byte)((float)(int)color14.G * num5);
											color14.B = (byte)((float)(int)color14.B * num6);
											Main.spriteBatch.Draw(backgroundTexture[5].Value, new Vector2(bgStart + num3 * num49 + 16 * num51 + num48, bgStartY + Main.backgroundHeight[2] * num50 + 16 * num52) + vector, new Rectangle(16 * num51 + num48 + 16, 16 * num52 + Main.backgroundHeight[2] * Main.magmaBGFrame, 16, 16), color14, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
										}
									}
									else
									{
										color14.R = (byte)((float)(int)color14.R * num4);
										color14.G = (byte)((float)(int)color14.G * num5);
										color14.B = (byte)((float)(int)color14.B * num6);
										Main.spriteBatch.Draw(backgroundTexture[5].Value, new Vector2(bgStart + num3 * num49 + 16 * num51 + num48, bgStartY + Main.backgroundHeight[2] * num50 + 16 * num52) + vector, new Rectangle(16 * num51 + num48 + 16, 16 * num52 + Main.backgroundHeight[2] * Main.magmaBGFrame, 16, 16), color14, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
									}
								}
							}
						}
					}
				}
				//Lighting.brightness = Lighting.defBrightness;
				//renderTimer[3] = stopwatch.ElapsedMilliseconds;
				// -> updated to the following ->
				Lighting.GlobalBrightness = globalBrightness; //loc var

				self.SetBackgroundParrallax(bgParrallax);
				self.SetBackgroundStartX(bgStart);
				self.SetBackgroundLoops(bgLoops);
				self.SetBackgroundTopY(bgTop);
				self.SetBackgroundLoopsY(bgLoopsY);
				self.SetBackgroundStartY(bgStartY);

				TimeLogger.DrawTime(3, stopwatch.Elapsed.TotalMilliseconds);
				//Add saving of main values (if need saving, ie bgLoops)
			}
			else
			{
				orig.Invoke(self);
			}
		}
	}
}
