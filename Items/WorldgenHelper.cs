using Avalon.Tiles.Blastplains;
using Avalon.Tiles.CrystalMines;
using Avalon.Tiles.Ores;
using Avalon.WorldGeneration.Passes;
using Avalon.WorldGeneration.Structures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

public class WorldgenHelper : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return true;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Purple;
		Item.width = dims.Width;
		Item.maxStack = 1;
		Item.useAnimation = Item.useTime = 14;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = 0;
		Item.height = dims.Height;
		Item.autoReuse = true;
		//item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Scroll");
	}
	public static float Distance(Vector2 a, Vector2 b)
	{
		float diff_x = a.X - b.X;
		float diff_y = a.Y - b.Y;
		return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y);
	}
	public override bool? UseItem(Player player)
	{
		int x = (int)Main.MouseWorld.X / 16;
		int y = (int)Main.MouseWorld.Y / 16;

		if (player.ItemAnimationJustStarted)
		{
			WorldGeneration.Structures.IceShrine.Generate(x, y);
			//JonasHellcastle.Generate(x, y);
			//bool top = WorldGen.genRand.NextBool();
			//CreateOvalCave(x, y, 20, top ? -5f : 5f, WorldGen.genRand.NextFloat(-.4f, .4f), top);
			//WorldGeneration.Biomes.Basalt.PlaceBasalt();

			#region old junk
			////if (AvalonGlobalTile.TileVisibilityState < (int)AvalonGlobalTile.TileVisibilityStateEnum.OnlyTiles)
			////{
			////	AvalonGlobalTile.TileVisibilityState++;
			////}
			////else
			////{
			////	AvalonGlobalTile.TileVisibilityState = 0;
			////}

			////Tile tile = Framing.GetTileSafely(x, y);
			////tile.LiquidType = LiquidID.Water;
			////tile.LiquidAmount++;
			////Main.NewText(tile.LiquidAmount);

			//// 64, then 1px per 16

			////for (int i = 0; i < Main.item.Length - 1; i++)
			////{
			////	if (Main.item[i].type > ItemID.None)
			////	{
			////		Main.NewText(Main.item[i].position + " _ " + i);
			////		player.Center = Main.item[i].position;
			////		i = Main.item.Length;
			////	}
			////}
			////Contagion.ContagionRunner(x, y);
			////AvalonSpecialSeedsGenSystem.GenerateSpawnArea(x, y);
			////WorldGeneration.Structures.IceShrine.Generate(x, y);
			////WorldGeneration.Structures.TuhrtlOutpost.Outpost(x, y);
			////WorldGeneration.Structures.LeafTrap.CreateLargeLeafTrap(x, y);
			////Crystals(x, y);
			////World.Biomes.CrystalMines.Place(new Point(x, y));

			////WorldGeneration.Structures.Nest.CreateWaspNest(x, y);

			////WorldGeneration.Structures.LavaOcean.MakeLavaLake(x, y);
			////WorldGeneration.Structures.LavaShrine.NewLavaShrine(x - 29, y - 10);


			////int rift = Item.NewItem(Item.GetSource_TileInteraction(x, y), x * 16, y * 16, 8, 8, ModContent.ItemType<Accessories.Hardmode.OreRift>());
			////Main.item[rift].velocity *= 0f;
			////Main.item[rift].GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft = 300;

			////MakeSpike(x, y, 15, 8, -1);
			////MakeSpike(x, y, 22, 13, 1);
			////int width = 13;
			////int height = WorldGen.genRand.Next(15, 22);
			////Underworld.MakeSpike(x, y, height - 2, width, (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), 1);
			////Underworld.MakeSpike(x, y, height, width, (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), -1);
			////Main.NewText($"height: {height}");
			////MakeSpike(x, y, height, width, -1);
			////MakeSpike(x, y, height - 3, width);
			////WorldGeneration.Utils.MakeCircle2(x, y, (int)((height - 1) * 0.4f) - 2, ModContent.TileType<SkyBrick>(), ModContent.TileType<SkyBrick>());
			////WorldGeneration.Utils.MakeCircle2(x, y, 1, ModContent.TileType<Tiles.CaesiumCrystal>(), ModContent.TileType<Tiles.Ores.CaesiumOre>());
			////Main.NewText(WorldGen.WorldSizeMediumX);
			////Main.NewText(Main.maxTilesX);

			////Tile targetTile = Main.tile[x, y + 1];
			//Main.NewText((int)1.5f + " _ " + (int)-1.5f);
			////Main.NewText(TileLoader.GetTile(targetTile.TileType).GetType().GetType());

			////Main.NewText(FindDerivedTypes(Assembly.GetCallingAssembly(), typeof(ModHerb)));
			////Main.NewText(typeof(Barfbush).BaseType);
			////Main.NewText(TileLoader.GetTile(Main.tile[x, y].TileType).GetType().BaseType);
			#endregion old junk
		}
		return false;
	}

	public static void CreateOvalCave(int x, int y, int radius, float intercept, float slope, bool top = true)
	{
		int radiusX = radius + WorldGen.genRand.Next(-8, 9);
		int radiusY = radius;
		//float slope = WorldGen.genRand.NextFloat(-0.4f, 0.4f); // range from -0.5 to 0.5
		//float intercept = -5f;

		for (int i = -radiusX; i <= radiusX; i++)
		{
			for (int j = -radiusY; j <= radiusY; j++)
			{
				double dx = i / (double)radiusX;
				double dy = j / (double)radiusY;

				if (dx * dx + dy * dy <= 1.0)
				{
					float bottomY = i * slope + intercept;
					if (top)
					{
						if (j > bottomY)
							continue;
					}
					else if (j < bottomY) continue;

					int worldX = x + i;
					int worldY = y + j;

					if (WorldGen.InWorld(worldX, worldY))
					{
						Main.tile[worldX, worldY].Active(false);
					}
				}
			}
		}
	}



	public IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
	{
		return assembly.GetTypes().Where(t => t != baseType &&
											  baseType.IsAssignableFrom(t));
	}


	
	public static void MakeSkyFortress(int x, int y)
	{
		// with these parameters, the top floor will always have space for two of the hidden rooms (technically more, but at least 2)
		// these guaranteed free spots should be checked for after the middle floors though, so that it's likely that at least 1 of the rooms generates in those instead

		(bool X, bool Y) smallWorld = (Main.maxTilesX < WorldGen.WorldSizeMediumX, Main.maxTilesY < WorldGen.WorldSizeMediumY);
		//smallWorld = (true, true);
		(bool X, bool Y) mediumWorld = (Main.maxTilesX is >= WorldGen.WorldSizeMediumX and < WorldGen.WorldSizeLargeX, Main.maxTilesY is >= WorldGen.WorldSizeMediumY and < WorldGen.WorldSizeLargeY);
		//mediumWorld = (true, true);
		//(bool X, bool Y) largeWorld = (smallWorld.Item1 == false && mediumWorld.Item1 == false, smallWorld.Item2 == false && mediumWorld.Item2 == false);


		//int totalSegments = Main.maxTilesX < 6300 ? /*small: */ 10 : (Main.maxTilesX < 8400 ? /*medium: */ 14 : /*large: */ 16);
		//int lowerHallsFloorCount = Main.maxTilesY < 1800 ? /*small: */ 1 : (Main.maxTilesY < 2400 ? /*medium: */ 1 : /*large: */ 2);
		//int middleHallsFloorCount = 2;
		//int upperHallsFloorCount = Main.maxTilesY < 1800 ? /*small: */ 1 : (Main.maxTilesY < 2400 ? /*medium: */ 2 : /*large: */ 2);

		int totalSegments = smallWorld.X ? 12 : (mediumWorld.X ? 14 : 16);
		int lowerHallsFloorCount = smallWorld.Y ? 1 : (mediumWorld.Y ? 1 : 2);
		int middleHallsFloorCount = 2;
		int upperHallsFloorCount = smallWorld.Y ? 1 : (mediumWorld.Y ? 2 : 2);

		// chance that a dividing wall will be created for each cell, given the below conditions are satisfied
		int wallChanceDenominator = 8;
		// how many vertical dividing walls are on each floor
		int maxWallsPerFloor = smallWorld.X ? 1 : 2;
		int topFloorMaxWalls = smallWorld.X ? 0 : 2;
		// the minimum gap in cells between dividing walls
		int minGapBetweenWalls = 5;
		// pretty sure these next two values are not working properly rn, I'll just fix it later
		// used for the far left side of the structure
		int initialGapBetweenWalls = 2;
		// used for the far right side of the structure
		int finalGapBetweenWalls = 2;

		initialGapBetweenWalls = Math.Clamp(initialGapBetweenWalls, 1, minGapBetweenWalls); // clamped between 1 and minGapBetweenWalls, as it does not work outside this range
		finalGapBetweenWalls = Math.Clamp(finalGapBetweenWalls, 1, minGapBetweenWalls); // clamped between 1 and minGapBetweenWalls, as it does not work outside this range

		//WorldGeneration.Structures.ChainedArrayBuilder.NewChainedStructure(x, y, totalSegments, lowerHallsFloorCount, middleHallsFloorCount, upperHallsFloorCount, wallChanceDenominator, maxWallsPerFloor, topFloorMaxWalls, minGapBetweenWalls, initialGapBetweenWalls, finalGapBetweenWalls);
	}




	public static void Crystals(int x, int y)
	{
		int mult = WorldGen.genRand.Next(2, 4);
		int pstep = 0;

		for (int pyY = y; pyY <= y + mult + WorldGen.genRand.Next(1, 6); pyY++)
		{
			for (int pyX = x - pstep + WorldGen.genRand.Next(1, 4); pyX <= x + pstep + WorldGen.genRand.Next(1, 4); pyX++)
			{
				WorldGen.PlaceTile(pyX, pyY, ModContent.TileType<CrystalStoneCrystals>());
			}
			pstep++;
		}
	}







	public static void GrowCloud(int x, int y)
	{
		GrowFragile(x, y, TileID.Cloud, 40);
		for (int i = x; i < x + 40; i++)
		{
			for (int j = y; j < y + 40; j++)
			{
				if (Main.tile[i, j].TileType == TileID.Cloud &&
					!Main.tile[i, j + 1].HasTile)
				{
					Main.tile[i, j].TileType = TileID.RainCloud;
				}
			}
		}
	}

	public static void GrowFragile(int x, int y, ushort type, int rounds)
	{
		int growth = WorldGen.genRand.Next(256);

		for (int i = 0; i < 18; i++)
		{
			int j = i % 3;
			int tgro = 1 << i;
			if ((tgro & growth) == tgro)
			{
				int tx = (x + j - 1);
				int ty = y + (i / 3) - 1;

				if (!Main.tile[tx, ty].HasTile)
				{
					Tile t = Main.tile[tx, ty];
					t.HasTile = true;
					t.TileType = type;
					WorldGen.SquareTileFrame(tx, ty);
					if (rounds > 0)
					{
						GrowFragile(tx, ty, type, rounds - 1);
					}
				}
			}
		}
		WorldGen.SquareTileFrame(x, y);
	}


	//public static void GetXCoord(int x, int y, int ylength, ref int xCoord)
	//{
	//    bool leftSideActive = false;
	//    bool rightSideActive = false;
	//    for (int i = y; i < y + ylength; i++)
	//    {
	//        if (Main.tile[x, i].HasTile || Main.tile[x, i].LiquidAmount > 0 || Main.tile[x, i].WallType > 0)
	//        {
	//            leftSideActive = true;
	//            break;
	//        }
	//    }
	//    for (int i = y; i < y + ylength; i++)
	//    {
	//        if (Main.tile[x + 4, i].HasTile || Main.tile[x + 4, i].LiquidAmount > 0 || Main.tile[x + 4, i].WallType > 0)
	//        {
	//            rightSideActive = true;
	//            break;
	//        }
	//    }
	//    if (leftSideActive || rightSideActive)
	//    {
	//        xCoord--;
	//        GetXCoord(xCoord, y, ylength, ref xCoord);
	//    }
	//}
	//public static int GetXCoord(int x, int y, ref int xStored)
	//{
	//    if (Main.tile[x, y].HasTile || Main.tile[x, y].liquid > 0 || Main.tile[x, y].WallType > 0)
	//    {
	//        xStored--;
	//        GetXCoord(xStored, y, ref xStored);
	//    }
	//    return xStored;
	//}



	/// <summary>
	/// Makes a spike at the given coordinates.
	/// </summary>
	/// <param name="x">The X coordinate.</param>
	/// <param name="y">The Y coordinate.</param>
	/// <param name="length">The height/tallness of the spike to generate.</param>
	/// <param name="width">The width/thickness of the spike to generate.</param>
	/// <param name="direction">The vertical direction of the spike; 1 is down, -1 is up.</param>
	public static void MakeSpike(int x, int y, int length, int width, int direction = 1)
	{
		// Store the x and y in new vars
		int startX = x;
		int startY = y;

		// Define variables to determine how many tiles to travel in one direction before changing direction
		int howManyTimes = 0;
		int maxTimes = WorldGen.genRand.Next(4) + 2;
		Main.NewText($"maxTimes: {maxTimes}", Main.DiscoColor);
		int modifier = 1;

		// Change direction (left/right) of the spike before generating
		if (WorldGen.genRand.NextBool())
		{
			modifier *= -1;
		}

		// Initial assignment of last position
		Vector2 lastPos = new(x, y);

		float radiusMult = WorldGen.genRand.NextFloat(0.2f, 0.35f);
		Main.NewText($"radiusMult: {radiusMult}", Main.DiscoColor);
		// Loop until length
		for (int q = 1; q <= length; q++)
		{
			// Grab the distance between the start and the current position
			float distFromStart = Vector2.Distance(new Vector2(startX, startY), new Vector2(x, y));

			// If the distance is divisible by 4 and the width is less than 5, reduce the width
			int w = width;
			if ((int)distFromStart % 4 == 0 || w < 5)
			{
				width--;
			}

			// Put a circle between the last point and the current
			int betweenXPos = (int)(lastPos.X + x) / 2;
			int betweenYPos = (int)(lastPos.Y + y) / 2;
			float radiusTemp = (length - q) * radiusMult;
			int radius = radiusTemp > 1f ? (int)radiusTemp : (int)MathF.Round(radiusTemp);
			float radiusNextTemp = MathHelper.Clamp(radiusTemp - radiusMult, 0, radiusTemp);
			int radiusNext = radiusNextTemp > 1f ? (int)radiusNextTemp : (int)MathF.Round(radiusNextTemp);
			if (radius == 2 && radiusNext == 1) ;
			//if (radius == 0) radius = 1;
			//Main.NewText($"{radius} {radiusTemp} {radiusNext}");
			if (radiusNext > 1)
			{
				WorldGeneration.Utils.MakeCircleBordered(betweenXPos, betweenYPos, radius, ModContent.TileType<CaesiumCrystal>(), ModContent.TileType<Tiles.Ores.CaesiumOre>(), 3, 0.5f);

				// Make a square/circle of the tile
				WorldGeneration.Utils.MakeCircleBordered(x, y, radius, ModContent.TileType<CaesiumCrystal>(), ModContent.TileType<Tiles.Ores.CaesiumOre>(), 3, 0.5f);
			}
			else
			{
				if ((direction == 1 || q != length) && (direction == 1 ? MathHelper.Max(betweenXPos, x) - MathHelper.Min(betweenXPos, x) != 0 : MathHelper.Max(betweenXPos, x) - MathHelper.Min(betweenXPos, x) == 0))
				{
					betweenXPos -= 1;
					//Main.NewText($"{betweenXPos - x} {x - betweenXPos}", Main.DiscoColor);
				}
				for (int i = 0; i < radius + 1 + (int)(radiusTemp - radiusMult); i++)
				{
					int betweenXPosMod = betweenXPos - i * modifier;
					if (direction == 1 && q == length)
					{
						Tile betweenPosUp = Framing.GetTileSafely(betweenXPosMod, betweenYPos - 1);
						if (betweenPosUp.TileType != (ushort)ModContent.TileType<CaesiumCrystal>())
						{
							Tile betweenPosUpL = Framing.GetTileSafely(betweenXPosMod - 1, betweenYPos - 1);
							Tile betweenPosUpR = Framing.GetTileSafely(betweenXPosMod + 1, betweenYPos - 1);
							if (betweenPosUpL.TileType == (ushort)ModContent.TileType<CaesiumCrystal>())
							{
								betweenXPosMod -= 1;
							}
							else if (betweenPosUpR.TileType == (ushort)ModContent.TileType<CaesiumCrystal>())
							{
								betweenXPosMod += 1;
							}

						}
					}
					Tile t = Framing.GetTileSafely(betweenXPosMod, betweenYPos);
					t.HasTile = true;
					t.IsHalfBlock = false;
					t.Slope = SlopeType.Solid;
					t.TileType = (ushort)ModContent.TileType<CaesiumCrystal>();
					Tile.SmoothSlope(betweenXPosMod, betweenYPos);
					WorldGen.SquareTileFrame(betweenXPosMod, betweenYPos);

					if (q != length)
					{
						int xMod = x - i * modifier;
						Tile t2 = Framing.GetTileSafely(xMod, y);
						t2.HasTile = true;
						t2.IsHalfBlock = false;
						t2.Slope = SlopeType.Solid;
						t2.TileType = (ushort)ModContent.TileType<CaesiumCrystal>();
						Tile.SmoothSlope(xMod, y);
						WorldGen.SquareTileFrame(xMod, y);
						Dust.QuickDust(new Point(xMod, y), Color.Blue);
						//Main.NewText(MathHelper.Max(betweenXPos, x) - MathHelper.Min(betweenXPos, x), Main.DiscoColor);
					}
					Dust.QuickDust(new Point(betweenXPosMod, betweenYPos), Color.Red);
				}
			}

			for (int k = x - radius; k <= x + radius; k++)
			{
				for (int l = y - radius; l <= y + radius; l++)
				{
					if (Vector2.Distance(new Vector2(k, l), new Vector2(x, y)) < radius && radius > 1)
					{
						if (k > 0 && l > 0)
						{
							WorldGeneration.Utils.Place_Check8WayMatchingTile(k, l, (ushort)ModContent.TileType<CaesiumOre>(), ((ushort)ModContent.TileType<CaesiumCrystal>(), 0.035f), ((ushort)ModContent.TileType<CaesiumOre>(), 5f));
							WorldGen.SquareTileFrame(k, l);
						}
					}
				}
			}
			Tile t3 = Framing.GetTileSafely(startX, startY);
			t3.HasTile = true;
			t3.TileType = (ushort)ModContent.TileType<CaesiumOre>();

			// Assign the last position to the current position
			lastPos = new(x, y);

			// Make the spike go in the opposite direction after a certain amount of tiles
			howManyTimes++;
			if (howManyTimes % maxTimes == 0 && radiusNext > 3)
			{
				modifier *= -1;
				howManyTimes = 0;
				maxTimes = WorldGen.genRand.Next(4) + 2;
				Main.NewText($"maxTimes: {maxTimes}", Main.DiscoColor);
			}

			// Add to the x to make the spike turn/curve in a direction
			int xm = WorldGen.genRand.Next(2);

			x += ((radiusNext > 1 ? xm + 1 : y != startY + length * direction ? 1 : 0)) * modifier; // * (maxTimes / (WorldGen.genRand.Next(2) + 1));
			Main.NewText($"x: {x}", Main.DiscoColor);

			// Add a bit to the y coord to make it go up or down depending on the parameter
			if (w > 3)
			{
				int ym = WorldGen.genRand.Next(3);

				y += ((radiusNext > 2 ? ym + 4 : 1)) * direction;
				Main.NewText($"ym: {ym}", Main.DiscoColor);
			}
			else
			{
				y += 1 * direction;
			}
		}
	}
}
