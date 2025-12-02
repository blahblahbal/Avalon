using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class ShardsTier2 : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileObsidianKill[Type] = true;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 500;
		Main.tileSpelunker[Type] = true;

		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 1;
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinateHeights = [16];
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.AnchorWall = false;
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(CanPlaceAlter, -1, 0, processedCoordinates: true);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(AfterPlacement, -1, 0, processedCoordinates: false);
		TileObjectData.addTile(Type);
	}

	public static int CanPlaceAlter(int i, int j, int type, int style, int direction, int alternate)
	{
		return 1;
	}
	public static int AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
	{
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			NetMessage.SendTileSquare(Main.myPlayer, i, j, 1, 1);
		}

		return 1;
	}

	// selects the map entry depending on the frameX
	public override ushort GetMapOption(int i, int j)
	{
		return (ushort)(Main.tile[i, j].TileFrameX / 18);
	}
	public override bool CreateDust(int i, int j, ref int type)
	{
		switch (Main.tile[i, j].TileFrameX / 18)
		{
			case 0:
				type = DustID.Torch;
				break;
			case 1:
				type = DustID.IceTorch;
				break;
			case 2:
				type = DustID.Venom;
				break;
			case 3:
				type = DustID.Dirt;
				break;
			case 4:
				type = DustID.RainCloud;
				break;
			case 5:
				type = DustID.Bone;
				break;
			case 6:
				type = DustID.DungeonWater;
				break;
			case 7:
				type = DustID.CorruptionThorns;
				break;
			case 8:
				if (Main.rand.NextBool(2))
				{
					Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Cloud);
				}
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Enchanted_Pink);
				return false;
		}
		return true;
	}
	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		int toDrop = 0;
		switch (Main.tile[i, j].TileFrameX / 18)
		{
			case 0:
				toDrop = ModContent.ItemType<BlastShard>();
				break;
			case 1:
				toDrop = ModContent.ItemType<FrigidShard>();
				break;
			case 2:
				toDrop = ModContent.ItemType<VenomShard>();
				break;
			case 3:
				toDrop = ModContent.ItemType<CoreShard>();
				break;
			case 4:
				toDrop = ModContent.ItemType<TornadoShard>();
				break;
			case 5:
				toDrop = ModContent.ItemType<DemonicShard>();
				break;
			case 6:
				toDrop = ModContent.ItemType<TorrentShard>();
				break;
			case 7:
				toDrop = ModContent.ItemType<WickedShard>();
				break;
			case 8:
				toDrop = ModContent.ItemType<SacredShard>();
				break;
			case 9:
				toDrop = ModContent.ItemType<ElementShard>();
				break;
		}

		yield return new Item(toDrop);
	}

	// copy from the vanilla tileframe for placed gems
	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tile = Framing.GetTileSafely(i, j);
		Tile topTile = Framing.GetTileSafely(i, j - 1);
		Tile bottomTile = Framing.GetTileSafely(i, j + 1);
		Tile leftTile = Framing.GetTileSafely(i - 1, j);
		Tile rightTile = Framing.GetTileSafely(i + 1, j);
		int topType = -1;
		int bottomType = -1;
		int leftType = -1;
		int rightType = -1;

		// placed gem fix
		// I think lion is responsible for this, fuck knows what it's actually "fixing" cause it doesn't bloody say. Just make sure to update if more gems are added to the sheet.
		if (tile.TileFrameX >= 18 * 10)
		{
			if (WorldGen.SolidTile(i - 1, j) || WorldGen.SolidTile(i + 1, j) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1))
			{
				tile.TileFrameX -= 18 * 10;
			}
			else
			{
				tile.HasTile = false;
			}
		}
		// end fix

		if (topTile.HasTile && !topTile.BottomSlope)
		{
			bottomType = topTile.TileType;
		}
		if (bottomTile.HasTile && !bottomTile.IsHalfBlock && !bottomTile.TopSlope)
		{
			topType = bottomTile.TileType;
		}
		if (leftTile.HasTile && !leftTile.IsHalfBlock && !leftTile.RightSlope)
		{
			leftType = leftTile.TileType;
		}
		if (rightTile.HasTile && !rightTile.IsHalfBlock && !rightTile.LeftSlope)
		{
			rightType = rightTile.TileType;
		}

		// these door lines don't seem to do anything, but they're in the vanilla tileframe code for exposed gems, might be for worldgen idk
		// todo: add support for modded doors later, likely with TileID.Sets.CloseDoorID, but whatever tmod devs decide to use for WorldGen.SolidTile
		if (leftType == TileID.ClosedDoor)
		{
			leftType = -1;
		}
		if (rightType == TileID.ClosedDoor)
		{
			rightType = -1;
		}

		short variation = (short)(WorldGen.genRand.Next(3) * 18);
		if (topType >= 0 && Main.tileSolid[topType] && !Main.tileSolidTop[topType])
		{
			if (tile.TileFrameY < 0 || tile.TileFrameY > 36)
			{
				tile.TileFrameY = variation;
			}
		}
		else if (leftType >= 0 && Main.tileSolid[leftType] && !Main.tileSolidTop[leftType])
		{
			if (tile.TileFrameY < 108 || tile.TileFrameY > 54)
			{
				tile.TileFrameY = (short)(108 + variation);
			}
		}
		else if (rightType >= 0 && Main.tileSolid[rightType] && !Main.tileSolidTop[rightType])
		{
			if (tile.TileFrameY < 162 || tile.TileFrameY > 198)
			{
				tile.TileFrameY = (short)(162 + variation);
			}
		}
		else if (bottomType >= 0 && Main.tileSolid[bottomType] && !Main.tileSolidTop[bottomType])
		{
			if (tile.TileFrameY < 54 || tile.TileFrameY > 90)
			{
				tile.TileFrameY = (short)(54 + variation);
			}
		}
		else
		{
			WorldGen.KillTile(i, j);
		}
		return true;
	}

	// needed so gems are only allowed to be placed on solid tiles
	public override bool CanPlace(int i, int j)
	{
		return WorldGen.SolidTile(i - 1, j, noDoors: true) || WorldGen.SolidTile(i + 1, j, noDoors: true) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1);
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		if (Main.tile[i, j].TileFrameY / 18 < 3)
		{
			offsetY = 2;
		}
	}
}
