using Avalon.Common;
using Terraria.ModLoader;
using Terraria;
using Avalon.Tiles.Contagion;
using MonoMod.Cil;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.GameContent.Drawing;
using Terraria.Map;

namespace Avalon.Hooks;

internal class CattailHooks : ModHook
{
	protected override void Apply()
	{
		On_Player.PlaceThing_Tiles_PlaceIt_KillGrassForSolids += KillConjoinedGrass_PlaceThing;
		On_Player.DoesPickTargetTransformOnKill += PickaxeKillTile;
		IL_Liquid.DelWater += BurnGrass;
		//IL_WorldGen.PlantCheck += PlantTileFrameIL;
		//On_Player.DoBootsEffect_PlaceFlowersOnTile += FlowerBootsEdit;
		//On_WorldGen.IsFitToPlaceFlowerIn += Flowerplacement;
		On_WorldGen.PlaceTile += PlaceTile;
		//IL_WorldGen.TileFrame += VineTileFrame;
		IL_MapHelper.CreateMapTile += CactusMapColor;
		On_WorldGen.PlaceLilyPad += LilyPadPreventer;
		IL_WorldGen.CheckCatTail += CheckCattailEdit;
		IL_WorldGen.PlaceCatTail += PlaceCattailEdit;
		On_WorldGen.GrowCatTail += GrowCattailEdit;
		IL_WorldGen.CheckLilyPad += CheckLilyPadEdit;
		IL_WorldGen.PlaceLilyPad += PlaceLilyPadEdit;
		//On_TileDrawing.DrawSingleTile += LilyPadDrawingPreventer;
	}
	private bool PlaceTile(On_WorldGen.orig_PlaceTile orig, int i, int j, int Type, bool mute, bool forced, int plr, int style)
	{
		int num = Type;
		if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
		{
			Tile tile = Main.tile[i, j];
			if (forced || Collision.EmptyTile(i, j) || !Main.tileSolid[num])
			{
				if (num == ModContent.TileType<ContagionShortGrass>())
				{
					if (WorldGen.IsFitToPlaceFlowerIn(i, j, num))
					{
						if (tile.WallType >= 0 && WallID.Sets.AllowsPlantsToGrow[tile.WallType] && Main.tile[i, j + 1].WallType >= 0 && Main.tile[i, j + 1].WallType < WallLoader.WallCount && WallID.Sets.AllowsPlantsToGrow[Main.tile[i, j + 1].WallType])
						{
							if (WorldGen.genRand.NextBool(50) || WorldGen.genRand.NextBool(40))
							{
								tile.HasTile = true;
								tile.TileType = (ushort)num;
								tile.TileFrameX = 144;
							}
							else if (WorldGen.genRand.NextBool(35) || (Main.tile[i, j].WallType >= 63 && Main.tile[i, j].WallType <= 70))
							{
								tile.HasTile = true;
								tile.TileType = (ushort)num;
								int num3 = WorldGen.genRand.NextFromList<int>(6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20);
								tile.TileFrameX = (short)(num3 * 18);
							}
							else
							{
								tile.HasTile = true;
								tile.TileType = (ushort)num;
								tile.TileFrameX = (short)(WorldGen.genRand.Next(6) * 18);
							}
						}
					}
				}
				else if (num == ModContent.TileType<ContagionLilyPads>())
				{
					WorldGen.PlaceLilyPad(i, j);
				}
				else if (num == ModContent.TileType<ContagionCattails>())
				{
					WorldGen.PlaceCatTail(i, j);
				}
			}
		}
		return orig.Invoke(i, j, Type, mute, forced, plr, style);
	}
	private void CactusMapColor(ILContext il)
	{
		ILCursor c = new(il);
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdsfld("Terraria.Map.MapHelper", "tileLookup"),
			i => i.MatchLdloc(7),
			i => i.MatchLdelemU2(),
			i => i.MatchStloc3());
		c.EmitLdarg0(); //i (aka X)
		c.EmitLdarg1(); //j (aka Y)
		c.EmitLdloca(3); //num5
		c.EmitDelegate((int i, int j, ref int num5) => {
			Tile tile = Main.tile[i, j];
			if (tile != null)
			{ //somehow still out of bounds
				WorldGen.GetCactusType(i, j, tile.TileFrameX, tile.TileFrameY, out var sandType);
				if (Main.tile[i, j].TileType == TileID.Cactus && TileLoader.CanGrowModCactus(sandType) && sandType == ModContent.TileType<Snotsand>())
				{
					num5 = MapHelper.tileLookup[ModContent.TileType<IckyCactusDummyTile>()];
				}
			}
		});
	}
	private void PlaceLilyPadEdit(ILContext il)
	{
		ILCursor c = new(il);
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdcI4(5),
			i => i.MatchStloc(1),
			i => i.MatchLdcI4(0),
			i => i.MatchStloc(2));
		c.EmitLdarg(0); //x
		c.EmitLdloc1(); //num2
		c.EmitLdloc0(); //num
		c.EmitLdloca(2); //ref num3
		c.EmitDelegate((int x, int num2, int num, ref int num3) => {
			for (int i = x - num2; i <= x + num2; i++)
			{
				for (int k = num - num2; k <= num + num2; k++)
				{
					if (Main.tile[i, k].HasTile && Main.tile[i, k].TileType == ModContent.TileType<ContagionLilyPads>())
					{
						num3++;
					}
				}
			}
		});
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdindU2(),
			i => i.MatchStloc(5),
			i => i.MatchLdcI4(-1),
			i => i.MatchStloc(6));
		c.EmitLdloc(5); //type
		c.EmitLdloca(6); //ref num5
		c.EmitDelegate((int type, ref int num5) => {
			if (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<Snotsand>())
			{
				num5 = ModContent.TileType<ContagionLilyPads>();
			}
		});
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdloca(7),
			i => i.MatchCall<Tile>("get_frameY"),
			i => i.MatchLdloc(6),
			i => i.MatchConvI2(),
			i => i.MatchStindI2());
		c.EmitLdarg(0); //x
		c.EmitLdloc(0); //num
		c.EmitLdloc(6); //num5
		c.EmitDelegate((int x, int num, int num5) => {
			if (num5 == ModContent.TileType<ContagionLilyPads>())
			{
				Main.tile[x, num].TileType = (ushort)num5;
				Main.tile[x, num].TileFrameY = 0;
			}
		});
	}

	private void CheckLilyPadEdit(ILContext il)
	{
		ILCursor c = new(il);
		ILLabel IL_0000 = c.DefineLabel();
		c.GotoNext(
			MoveType.After,
			i => i.MatchStloc(1),
			i => i.MatchLdcI4(-1), //also known as Ldc.i4.m1
			i => i.MatchStloc(2));
		c.EmitLdloc(1); //type
		c.EmitLdloca(2); //ref num2
		c.EmitLdarg0(); //x
		c.EmitLdarg1(); //y
		c.EmitLdloca(3); //ref tile
		c.EmitDelegate((int type, ref int num2, int x, int y, ref Tile tile) => { //we inject this to change the num2 to our tile when under a certain tile type (we use the TileID for creamlilypads as num2 for the same reasons as CheckCattail())
			if (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<Snotsand>())
			{
				num2 = -1;
				int num3 = ModContent.TileType<ContagionLilyPads>();
				tile = Main.tile[x, y];
				if (num3 != tile.TileType)
				{
					tile.TileType = (ushort)num3;
					tile.TileFrameY = 0;
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, x, y);
					}
				}
				tile = Main.tile[x, y - 1];
				if (tile.LiquidType > 0)
				{
					if (!tile.HasTile)
					{
						tile.HasTile = true;
						tile.TileType = (ushort)ModContent.TileType<ContagionLilyPads>();
						ref short frameX = ref tile.TileFrameX;
						tile = Main.tile[x, y];
						frameX = tile.TileFrameX;
						tile = Main.tile[x, y - 1];
						ref short frameY = ref tile.TileFrameY;
						tile = Main.tile[x, y];
						frameY = tile.TileFrameY;
						tile = Main.tile[x, y - 1];
						tile.IsHalfBlock = false;
						tile.Slope = 0;
						tile = Main.tile[x, y];
						tile.HasTile = false;
						tile.TileType = 0;
						WorldGen.SquareTileFrame(x, y - 1, resetFrame: false);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, x, y - 1, 1, 2);
						}
						return;
					}
				}
				tile = Main.tile[x, y];
				if (tile.LiquidAmount != 0)
				{
					return;
				}
				Tile tileSafely = Framing.GetTileSafely(x, y + 1);
				if (!tileSafely.HasTile)
				{
					tile = Main.tile[x, y + 1];
					tile.HasTile = true;
					tile.TileType = (ushort)ModContent.TileType<ContagionLilyPads>();
					ref short frameX2 = ref tile.TileFrameX;
					tile = Main.tile[x, y];
					frameX2 = tile.TileFrameX;
					tile = Main.tile[x, y + 1];
					ref short frameY2 = ref tile.TileFrameY;
					tile = Main.tile[x, y];
					frameY2 = tile.TileFrameY;
					tile = Main.tile[x, y + 1];
					tile.IsHalfBlock = false;
					tile.Slope = 0;
					tile = Main.tile[x, y];
					tile.HasTile = false;
					tile = Main.tile[x, y];
					tile.TileType = 0;
					WorldGen.SquareTileFrame(x, y + 1, resetFrame: false);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, x, y, 1, 2);
					}
				}
				else if (tileSafely.HasTile && !TileID.Sets.Platforms[tileSafely.TileType] && (!Main.tileSolid[tileSafely.TileType] || Main.tileSolidTop[tileSafely.TileType]))
				{
					WorldGen.KillTile(x, y);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 0, x, y);
					}
				}
			}
		});
		c.EmitLdloc(1); //type
		c.EmitDelegate((int type) => {
			return type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<Snotsand>();
		});
		c.EmitBrfalse(IL_0000);
		c.EmitRet(); //return
		c.MarkLabel(IL_0000);

		c.GotoNext(
			MoveType.After,
			i => i.MatchLdloca(3),
			i => i.MatchCall<Tile>("get_frameY"),
			i => i.MatchLdloc(2),
			i => i.MatchConvI2(),
			i => i.MatchStindI2());
		c.EmitLdloca(3); //ref Tile
		c.EmitDelegate((ref Tile tile) => { //set the TileType to lilyPad since so the conversion between our and their tiles dont result in unintended consiquences
			tile.TileType = TileID.LilyPad;
		});
	}
	private void BurnGrass(ILContext il)
	{
		ILCursor c = new(il);
		if (!c.TryGotoNext(
			MoveType.After,
			i => i.MatchLdloca(10),
			i => i.MatchCall<Tile>("active"), //Gets the if statement checking if the tile at tile5 is active
			i => i.MatchBrfalse(out _))) //aka this places this BEFORE the check for normal grasses (normal, hallowed, corruption, (anything that uses dirt)
		{
			ModContent.GetInstance<ExxoAvalonOrigins>().Logger.Debug("Avalon: lava tile burning instructions not found");
			return;
		}
		c.EmitLdloca(10); //ref tile5
		c.EmitLdloc(8); //i
		c.EmitLdloc(9); //j
		c.EmitLdloc(0); //num
		c.EmitLdloc(1); //num2
		c.EmitDelegate((ref Tile tile5, int i, int j, int num, int num2) => {
			if (tile5.TileType == ModContent.TileType<Ickgrass>())
			{ //Turns Ickgrass into dirt when lava is near
				tile5.TileType = TileID.Dirt;
				WorldGen.SquareTileFrame(i, j);
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendTileSquare(-1, num, num2, 3);
				}
			}
		});
	}

	public static int ClimbContagionCatTail(int originx, int originy)
	{
		int num = 0;
		int num2 = originy;
		while (num2 > 10)
		{
			Tile tile = Main.tile[originx, num2];
			if (!tile.HasTile || tile.TileType != ModContent.TileType<ContagionCattails>())
			{
				break;
			}
			if (tile.TileFrameX >= 180)
			{
				num++;
				break;
			}
			num2--;
			num++;
		}
		return num;
	}
	private bool PickaxeKillTile(On_Player.orig_DoesPickTargetTransformOnKill orig, Player self, HitTile hitCounter, int damage, int x, int y, int pickPower, int bufferIndex, Tile tileTarget)
	{
		if (hitCounter.AddDamage(bufferIndex, damage, updateAmount: false) >= 100 && tileTarget.TileType == ModContent.TileType<Ickgrass>())
		{
			return true;
		}
		else
		{
			return orig.Invoke(self, hitCounter, damage, x, y, pickPower, bufferIndex, tileTarget);
		}
	}
	private void KillConjoinedGrass_PlaceThing(On_Player.orig_PlaceThing_Tiles_PlaceIt_KillGrassForSolids orig, Player self)
	{
		orig.Invoke(self);
		for (int i = Player.tileTargetX - 1; i <= Player.tileTargetX + 1; i++)
		{
			for (int j = Player.tileTargetY - 1; j <= Player.tileTargetY + 1; j++)
			{
				Tile tile = Main.tile[i, j];
				if (!tile.HasTile || self.inventory[self.selectedItem].createTile == tile.TileType || tile.TileType != ModContent.TileType<Ickgrass>())
				{
					continue;
				}
				bool flag = true;
				for (int k = i - 1; k <= i + 1; k++)
				{
					for (int l = j - 1; l <= j + 1; l++)
					{
						if (!WorldGen.SolidTile(k, l))
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					WorldGen.KillTile(i, j, fail: true);
					if (Main.netMode == NetmodeID.MultiplayerClient)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j, 1f);
					}
				}
			}
		}
	}
	private void GrowCattailEdit(On_WorldGen.orig_GrowCatTail orig, int x, int j)
	{
		AvalonWorld.GrowContagionCatTail(x, j);
		orig.Invoke(x, j);
	}

	private void PlaceCattailEdit(ILContext il)
	{
		ILCursor c = new(il);
		ILLabel IL_0000 = c.DefineLabel();
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdcI4(7),
			i => i.MatchStloc(2),
			i => i.MatchLdcI4(0),
			i => i.MatchStloc(3));
		c.EmitLdarg(0); //x
		c.EmitLdloc2(); //num2
		c.EmitLdloc0(); //num
		c.EmitLdloca(3); //ref num3
		c.EmitDelegate((int x, int num2, int num, ref int num3) => {
			for (int i = x - num2; i <= x + num2; i++)
			{ //
				for (int k = num - num2; k <= num + num2; k++)
				{
					if (Main.tile[i, k].HasTile && Main.tile[i, k].TileType == ModContent.TileType<ContagionCattails>())
					{
						num3++;
						break;
					}
				}
			}
		});
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdloc1(),
			i => i.MatchRet(),
			i => i.MatchLdcI4(-1), //also known as m1
			i => i.MatchStloc(7));
		c.EmitLdloc(6); //type
		c.EmitLdloca(7); //ref num5
		c.EmitDelegate((int type, ref int num5) => {
			if (type == ModContent.TileType<Ickgrass>())
			{
				num5 = ModContent.TileType<ContagionCattails>();
			}
		});
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdloc1(),
			i => i.MatchRet(),
			i => i.MatchLdloc(4),
			i => i.MatchLdcI4(1),
			i => i.MatchSub(),
			i => i.MatchStloc0());
		c.EmitLdloc(7); //num5
		c.EmitDelegate((int num5) => {
			return num5 == ModContent.TileType<ContagionCattails>();
		});
		c.EmitBrfalse(IL_0000);
		c.EmitLdloc(7); //num5
		c.EmitLdarg(0); //x
		c.EmitLdloc0(); //num
		c.EmitDelegate((int num5, int x, int num) => {
			Tile tile2 = Main.tile[x, num];
			tile2.HasTile = true;
			Main.tile[x, num].TileType = (ushort)num5;
			Main.tile[x, num].TileFrameX = 0;
			Main.tile[x, num].TileFrameY = 0;
			tile2.IsHalfBlock = false;
			tile2.Slope = 0;
			Main.tile[x, num].CopyPaintAndCoating(Main.tile[x, num + 1]);
			WorldGen.SquareTileFrame(x, num);

		});
		//return new Point(x, num);
		c.EmitLdarg0(); //x
		c.EmitLdloc0(); //num
		c.EmitNewobj(typeof(Point).GetConstructor([typeof(int), typeof(int)])); //new Point()
		c.EmitRet(); //return
		c.MarkLabel(IL_0000); //thanks fox for figuring out a solution for me for the in if bound return :3
	}

	private void CheckCattailEdit(ILContext il)
	{
		ILCursor c = new(il);
		c.GotoNext(
			MoveType.After,
			i => i.MatchStloc(5),
			i => i.MatchLdcI4(-1), //also known as Ldc.i4.m1
			i => i.MatchStloc(6));
		c.EmitLdloc(5); //type
		c.EmitLdloca(6); //ref num5
		c.EmitDelegate((int type, ref int num5) => { //injects this delegate just before the switch to set num5 to the TileFrameY
			if (type == ModContent.TileType<Ickgrass>())
			{
				num5 = ModContent.TileType<ContagionCattails>();
			}
		}); //num5 is usually used for tileframeY of the cattail type (normal, desert, hallow(unused), corruption, crimson, mushroom)
			//but here we use the TileID so that we dont clash with other mods
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdcI4(519),
			i => i.MatchBeq(out _),
			i => i.MatchLdloc0(),
			i => i.MatchLdcI4(1),
			i => i.MatchAdd(),
			i => i.MatchStloc0());
		c.EmitLdarg0(); //x
		c.EmitLdloc2(); //num2
		c.EmitLdloc1(); //flag
		c.EmitLdloca(0); //ref num
		c.EmitLdloca(6); //ref num5
		c.EmitDelegate((int x, int num2, bool flag, ref int num, ref int num5) => { //to make injection easier, we insert this after the incrimenting of num,
																					//this converts the tile if num5 is the creamcattail ID
			if (Main.tile[x, num2].TileType == ModContent.TileType<ContagionCattails>())
			{
				CreamcattailCheck(x, num2, ref num, ref flag);
			}
			if (!flag)
			{
				if (num5 == ModContent.TileType<ContagionCattails>())
				{
					for (int k = num; k < num2; k++)
					{
						if (Main.tile[x, k] != null && Main.tile[x, k].HasTile)
						{
							Main.tile[x, k].TileType = (ushort)num5;
							Main.tile[x, k].TileFrameY = 0;
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, x, num);
							}
						}
					}
					//return; //commented out since returns dont work in IL edits, id have to call a ret instruction here 
				}
			}
		});
		c.GotoNext(
			MoveType.After,
			i => i.MatchLdloc(6),
			i => i.MatchConvI2(),
			i => i.MatchStindI2());
		c.EmitLdarg1(); //x
		c.EmitLdloc(11); //k
		c.EmitDelegate((int x, int k) => { //Adds tiletype to make sure we arent placing a CreamCattail instead
			Main.tile[x, k].TileType = TileID.Cattail;
		});
	}
	private bool LilyPadPreventer(On_WorldGen.orig_PlaceLilyPad orig, int x, int j)
	{
		int num = j;
		while (Main.tile[x, num].LiquidAmount > 0 && num > 50)
		{
			num--;
		}
		num++;
		int l;
		for (l = num; (!Main.tile[x, l].HasTile || !Main.tileSolid[Main.tile[x, l].TileType] || Main.tileSolidTop[Main.tile[x, l].TileType]) && l < Main.maxTilesY - 50; l++)
		{
			if (Main.tile[x, l].HasTile && Main.tile[x, l].TileType == ModContent.TileType<ContagionCattails>())
			{
				return false;
			}
		}
		return orig.Invoke(x, j);
	}
	private void CreamcattailCheck(int x, int y, ref int num, ref bool flag)
	{
		int num2 = y;
		while ((!Main.tile[x, num2].HasTile || !Main.tileSolid[Main.tile[x, num2].TileType] || Main.tileSolidTop[Main.tile[x, num2].TileType]) && num2 < Main.maxTilesY - 50)
		{
			if (Main.tile[x, num2].HasTile && Main.tile[x, num2].TileType != ModContent.TileType<ContagionCattails>())
			{
				flag = true;
			}
			if (!Main.tile[x, num2].HasTile)
			{
				break;
			}
			num2++;
			if (Main.tile[x, num2] == null)
			{
				return;
			}
		}
		num = num2 - 1;
		while (Main.tile[x, num] != null && Main.tile[x, num].LiquidAmount > 0 && num > 50)
		{
			if ((Main.tile[x, num].HasTile && Main.tile[x, num].TileType != ModContent.TileType<ContagionCattails>()) || Main.tile[x, num].LiquidType != 0)
			{
				flag = true;
			}
			num--;
			if (Main.tile[x, num] == null)
			{
				return;
			}
		}
		num++;
		int num3 = num;
		int num4 = 8;//WorldGen.catTailDistance;
		if (num2 - num3 > num4)
		{
			flag = true;
		}
		if (!Main.tile[x, num2].HasTile)
		{
			flag = true;
		}
		num = num2 - 1;
		if (Main.tile[x, num] != null && !Main.tile[x, num].HasTile)
		{
			for (int num6 = num; num6 >= num3; num6--)
			{
				if (Main.tile[x, num6] == null)
				{
					return;
				}
				if (Main.tile[x, num6].HasTile && Main.tile[x, num6].TileType == ModContent.TileType<ContagionCattails>())
				{
					num = num6;
					break;
				}
			}
		}
		while (Main.tile[x, num] != null && Main.tile[x, num].HasTile && Main.tile[x, num].TileType == ModContent.TileType<ContagionCattails>())
		{
			num--;
		}
		num++;
		if (Main.tile[x, num2 - 1] != null && Main.tile[x, num2 - 1].LiquidAmount < 127 && WorldGen.genRand.NextBool(4))
		{
			flag = true;
		}
		if (Main.tile[x, num] != null && Main.tile[x, num].TileFrameX >= 180 && Main.tile[x, num].LiquidAmount > 127 && WorldGen.genRand.NextBool(4))
		{
			flag = true;
		}
		if (Main.tile[x, num] != null && Main.tile[x, num2 - 1] != null && Main.tile[x, num].TileFrameX > 18)
		{
			if (Main.tile[x, num2 - 1].TileFrameX < 36 || Main.tile[x, num2 - 1].TileFrameX > 72)
			{
				flag = true;
			}
			else if (Main.tile[x, num].TileFrameX < 90)
			{
				flag = true;
			}
			else if (Main.tile[x, num].TileFrameX >= 108 && Main.tile[x, num].TileFrameX <= 162)
			{
				Main.tile[x, num].TileFrameX = 90;
			}
		}
		if (num2 > num + 4 && Main.tile[x, num + 4] != null && Main.tile[x, num + 3] != null && Main.tile[x, num + 4].LiquidAmount == 0 && Main.tile[x, num + 3].TileType == ModContent.TileType<ContagionCattails>())
		{
			flag = true;
		}
	}
}
