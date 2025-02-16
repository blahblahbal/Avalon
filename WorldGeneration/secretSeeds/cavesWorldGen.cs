using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.secretSeeds
{
	public class cavesWorldGen : ModHook
	{
		protected override void Apply()
		{
			//blahr moment, i have 0 clue what any of this actually does, and blah doesnt pay me enough to look into it
			On_WorldGen.MakeDungeon += On_WorldGen_MakeDungeon;
			On_WorldGen.DungeonEnt += On_WorldGen_DungeonEnt;
			On_TerrainPass.ApplyPass += On_TerrainPass_ApplyPass;
			On_TerrainPass.FillColumn += On_TerrainPass_FillColumn;
			On_DesertBiome.Place += On_DesertBiome_Place;
			On_WorldGen.CrimStart += On_WorldGen_CrimStart;
			On_WorldGen.ChasmRunner += On_WorldGen_ChasmRunner;
			On_WorldGen.SonOfLakinater += On_WorldGen_SonOfLakinater;
			On_HoneyPatchBiome.Place += On_HoneyPatchBiome_Place;
			On_EnchantedSwordBiome.Place += On_EnchantedSwordBiome_Place;
		}

		#region caves wg
		private bool On_EnchantedSwordBiome_Place(On_EnchantedSwordBiome.orig_Place orig, EnchantedSwordBiome self, Point origin, StructureMap structures)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				//if (!WorldGen.InWorld(origin.X, origin.Y))
				{
					return false;
				}
			}
			return orig.Invoke(self, origin, structures);
		}

		private bool On_HoneyPatchBiome_Place(On_HoneyPatchBiome.orig_Place orig, HoneyPatchBiome self, Point origin, StructureMap structures)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				if (!WorldGen.InWorld(origin.X, origin.Y))
				{
					return false;
				}
			}
			return orig.Invoke(self, origin, structures);
		}

		private void On_WorldGen_SonOfLakinater(On_WorldGen.orig_SonOfLakinater orig, int i, int j, double strengthMultiplier)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				orig.Invoke(i, j + 40, strengthMultiplier);
			}
			else orig.Invoke(i, j, strengthMultiplier);
		}

		private void On_WorldGen_ChasmRunner(On_WorldGen.orig_ChasmRunner orig, int i, int j, int steps, bool makeOrb)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				orig.Invoke(i, j + 40, steps, makeOrb);
			}
			else orig.Invoke(i, j, steps, makeOrb);
		}

		private void On_WorldGen_CrimStart(On_WorldGen.orig_CrimStart orig, int i, int j)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				orig.Invoke(i, j + 40);
			}
			else orig.Invoke(i, j);
		}

		private bool On_DesertBiome_Place(On_DesertBiome.orig_Place orig, DesertBiome self, Point origin, StructureMap structures)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				Point newPoint = origin + new Point(0, 50);
				return orig.Invoke(self, newPoint, structures);
			}
			return orig.Invoke(self, origin, structures);
		}

		private void On_TerrainPass_FillColumn(On_TerrainPass.orig_FillColumn orig, int x, double worldSurface, double rockLayer)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				worldSurface = 20;
				for (int i = 0; i < worldSurface; i++)
				{
					Main.tile[x, i].Active(false);
					Main.tile[x, i].TileFrameX = -1;
					Main.tile[x, i].TileFrameY = -1;
				}

				for (int j = (int)worldSurface; j < Main.maxTilesY; j++)
				{
					Main.tile[x, j].Active(true);
					Main.tile[x, j].TileType = 1;
					Main.tile[x, j].TileFrameX = -1;
					Main.tile[x, j].TileFrameY = -1;
				}

				//orig.Invoke(x, 80, 130);
			}
			else orig.Invoke(x, worldSurface, rockLayer);
		}

		private void On_TerrainPass_ApplyPass(On_TerrainPass.orig_ApplyPass orig, TerrainPass self, GenerationProgress progress, GameConfiguration configuration)
		{
			orig.Invoke(self, progress, configuration);
			if (secretSeedSystem.GetCavesWorldGen)
			{
				GenVars.rockLayer = 50;
				GenVars.rockLayerHigh = 55;
				GenVars.rockLayerLow = 45;
				GenVars.worldSurface = 30;
				GenVars.worldSurfaceHigh = 35;
				GenVars.worldSurfaceLow = 30;
			}
		}

		private void On_WorldGen_DungeonEnt(On_WorldGen.orig_DungeonEnt orig, int i, int j, ushort tileType, int wallType)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				WorldGen.drunkWorldGen = true;
				orig.Invoke(i, j + 150, tileType, wallType);
				WorldGen.drunkWorldGen = false;
			}
			else orig.Invoke(i, j, tileType, wallType);
		}

		private void On_WorldGen_MakeDungeon(On_WorldGen.orig_MakeDungeon orig, int x, int y)
		{
			if (secretSeedSystem.GetCavesWorldGen)
			{
				y += 250;
			}
			orig.Invoke(x, y);
		}

		public static bool GenerateSpawnArea(int X, int Y)
		{
			ushort tileStone = TileID.Stone;
			ushort dirt = TileID.Dirt;

			int num = 0;
			double num12 = 0.6;
			double num18 = 1.3;
			double num19 = 0.3;
			if (num == 0)
			{
				num12 = 0.55;
				num18 = 2.0;
			}
			num12 *= 1.05 - WorldGen.genRand.NextDouble() * 0.1;
			num18 *= 1.05 - WorldGen.genRand.NextDouble() * 0.1;
			num19 *= 1.0 - WorldGen.genRand.NextDouble() * 0.1;
			int biomeWidth = WorldGen.genRand.Next(125, 140); // 105, 125
			int num21 = (int)(biomeWidth * num19);
			int num22 = (int)(biomeWidth * num12);
			int num23 = WorldGen.genRand.Next(9, 13);
			int num24 = X - biomeWidth + (biomeWidth / 3);
			int num2 = X + biomeWidth - (biomeWidth / 3);
			int num3 = Y - biomeWidth;
			int num4 = Y + biomeWidth;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num24; j <= num2; j++)
				{
					if (!WorldGen.InWorld(j, i, 50))
					{
						return false;
					}
					if (Main.tile[j, i].TileType == 203 || Main.tile[j, i].TileType == 25)
					{
						return false;
					}
				}
			}
			int num5 = Y;
			if (WorldGen.genRand.Next(4) == 0)
			{
				num5 = Y - WorldGen.genRand.Next(2);
			}
			int num6 = Y - num23;
			if (WorldGen.genRand.Next(4) == 0)
			{
				num6 = Y - num23 - WorldGen.genRand.Next(2);
			}
			for (int k = num3; k <= num4; k++)
			{
				for (int l = num24; l <= num2; l++)
				{
					Tile t0 = Main.tile[l, k];
					t0.LiquidAmount = 0;
					if (WorldGen.genRand.Next(4) == 0)
					{
						num5 = Y - WorldGen.genRand.Next(2);
					}
					if (WorldGen.genRand.Next(4) == 0)
					{
						num6 = Y - num23 + WorldGen.genRand.Next(2);
					}
					int num7 = ((k <= Y) ? ((int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2) + Math.Pow(Math.Abs(k - Y) * 1.8 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2))) : ((int)Math.Sqrt(Math.Pow((double)Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2) + Math.Pow((double)Math.Abs(k - Y) * 1.8 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2))));
					if (num7 < biomeWidth)
					{
						Tile t = Main.tile[l, k];
						t.Slope = SlopeType.Solid;
						t.IsHalfBlock = false;
						t.TileType = tileStone;
						if (l > num24 + 5 + WorldGen.genRand.Next(2) && l < num2 - 5 - WorldGen.genRand.Next(2))
						{
							t.HasTile = true;
						}
						if (num7 < biomeWidth * 0.9)
						{
							t.WallType = WallID.None;
						}
						WorldGen.SquareTileFrame(l, k);
					}
					num7 = (int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.3) + Math.Pow((double)Math.Abs(k - Y) * num18 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.3));
					if (k > num6 && k < num5)
					{
						Tile t = Main.tile[l, k];
						t.HasTile = false;
					}
					if (k < num5 && num7 < (int)(num22 * (1.0 + WorldGen.genRand.NextDouble() * 0.02)))
					{
						Tile t = Main.tile[l, k];
						t.HasTile = false;
					}
					num7 = (int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 1.85) + Math.Pow((Math.Abs(k - Y) * 2) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 1.85));
					if (k < Y - 1 || num7 >= (int)(num21 * (1.0 + WorldGen.genRand.NextDouble() * 0.025)))
					{
						continue;
					}
					if (k <= Y + 2 || num7 != num21 - 1 || WorldGen.genRand.Next(2) != 0)
					{
						Tile t = Main.tile[l, k];
						t.HasTile = false;
					}
					if (k >= Y + 8)
					{
						Tile t = Main.tile[l, k];
						if (k == Y + 8)
						{
							t.TileType = TileID.Grass;
							t.HasTile = true;
						}
						else
						{
							t.TileType = TileID.Dirt;
							t.HasTile = true;
						}
					}
					if (k >= Y + 6 && k <= Y + 8)
					{
						if (WorldGen.genRand.NextBool(5))
						{
							WorldGen.GrowTree(l, k);
						}
					}
				}
			}
			if (num == 0)
			{
				num24 = (int)(X - biomeWidth * num19) - WorldGen.genRand.Next(-15, 1) - 5;
				num2 = (int)(X + biomeWidth * num19) + WorldGen.genRand.Next(0, 16);
				int m = num24;
				int num8 = 0;
				for (; m < num2; m += WorldGen.genRand.Next(9, 14))
				{
					int num9 = Y - 3;
					while (!Main.tile[m, num9].HasTile || Main.tile[m, num9].TileType == TileID.Trees)
					{
						num9--;
					}
					num9 -= 4;
					int num10 = WorldGen.genRand.Next(5, 10);
					int num11 = WorldGen.genRand.Next(5, 10);
					int n = m - num10;
					while (num10 > 0)
					{
						for (n = m - num10; n < m + num10; n++)
						{
							Tile t = Main.tile[n, num9];
							t.HasTile = true;
							t.TileType = tileStone;
						}
						num8++;
						if (WorldGen.genRand.Next(3) < num8)
						{
							num8 = 0;
							num10--;
							m += WorldGen.genRand.Next(-1, 2);
						}
						if (num11 <= 0)
						{
							num10--;
						}
						num11--;
						num9++;
					}
					n -= WorldGen.genRand.Next(1, 3);
					Tile t1 = Main.tile[n, num9 - 2];
					Tile t2 = Main.tile[n, num9 - 1];
					Tile t3 = Main.tile[n, num9];
					t1.HasTile = true;
					t1.TileType = tileStone;
					t2.HasTile = true;
					t2.TileType = tileStone;
					t3.HasTile = true;
					t3.TileType = tileStone;
					WorldGen.SquareTileFrame(n, num9 - 2);
					WorldGen.SquareTileFrame(n, num9 - 1);
					WorldGen.SquareTileFrame(n, num9);
					if (WorldGen.genRand.NextBool(2))
					{
						Tile t4 = Main.tile[n, num9 + 1];
						t4.HasTile = true;
						t4.TileType = tileStone;
						WorldGen.SquareTileFrame(n, num9 + 1);
						WorldGen.PlaceTight(n, num9 + 2);
					}
					else
					{
						WorldGen.PlaceTight(n, num9 + 1);
					}
				}
			}
			return true;
		}
		#endregion
	}
}
