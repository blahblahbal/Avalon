using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class PlacedGems : ModTile
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
		//TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.addTile(Type);

		AddMapEntry(new Color(86, 239, 255), this.GetLocalization("Opal"));
		AddMapEntry(new Color(61, 56, 65), this.GetLocalization("Onyx"));
		AddMapEntry(new Color(251, 66, 146), this.GetLocalization("Kunzite"));
		AddMapEntry(new Color(22, 212, 198), this.GetLocalization("Tourmaline"));
		AddMapEntry(new Color(0, 237, 14), this.GetLocalization("Peridot"));
		AddMapEntry(new Color(198, 168, 130), this.GetLocalization("Zircon"));
		AddMapEntry(new Color(86, 239, 255), this.GetLocalization("Opal"));
		AddMapEntry(new Color(61, 56, 65), this.GetLocalization("Onyx"));
		AddMapEntry(new Color(251, 66, 146), this.GetLocalization("Kunzite"));
		AddMapEntry(new Color(22, 212, 198), this.GetLocalization("Tourmaline"));
		AddMapEntry(new Color(0, 237, 14), this.GetLocalization("Peridot"));
		AddMapEntry(new Color(198, 168, 130), this.GetLocalization("Zircon"));
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
			//case 0:
			//    type = ModContent.DustType<Dusts.OpalDust>();
			//    break;
			//case 1:
			//    type = ModContent.DustType<Dusts.OnyxDust>();
			//    break;
			//case 2:
			//    type = ModContent.DustType<Dusts.KunziteDust>();
			//    break;
			case 3:
				var dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.TourmalineDust>(), 0, 0, 50, default, 0.8f);
				Main.dust[dust].noLightEmittence = true;
				Main.dust[dust].noLight = true;
				return false;
			case 4:
				dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.PeridotDust>(), 0, 0, 50, default, 0.8f);
				Main.dust[dust].noLightEmittence = true;
				Main.dust[dust].noLight = true;
				return false;
			case 5:
				dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.ZirconDust>(), 0, 0, 50, default, 0.8f);
				Main.dust[dust].noLightEmittence = true;
				Main.dust[dust].noLight = true;
				return false;
		}
		return false;
	}
	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		int toDrop = 0;
		switch (Main.tile[i, j].TileFrameX / 18)
		{
			//case 0:
			//    toDrop = ModContent.ItemType<Items.Material.Opal>();
			//    break;
			//case 1:
			//    toDrop = ModContent.ItemType<Items.Material.Onyx>();
			//    break;
			//case 2:
			//    toDrop = ModContent.ItemType<Items.Material.Kunzite>();
			//    break;
			case 3:
				toDrop = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
				break;
			case 4:
				toDrop = ModContent.ItemType<Items.Material.Ores.Peridot>();
				break;
			case 5:
				toDrop = ModContent.ItemType<Items.Material.Ores.Zircon>();
				break;
		}

		yield return new Item(toDrop);
	}
	//public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	//{
	//    int toDrop = 0;
	//    //Main.NewText("" + Main.tile[i, j].frameX);
	//    switch (Main.tile[i, j].TileFrameX / 18)
	//    {
	//        //case 0:
	//        //    toDrop = ModContent.ItemType<Items.Material.Opal>();
	//        //    break;
	//        //case 1:
	//        //    toDrop = ModContent.ItemType<Items.Material.Onyx>();
	//        //    break;
	//        //case 2:
	//        //    toDrop = ModContent.ItemType<Items.Material.Kunzite>();
	//        //    break;
	//        case 3:
	//            toDrop = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
	//            break;
	//        case 4:
	//            toDrop = ModContent.ItemType<Items.Material.Ores.Peridot>();
	//            break;
	//        case 5:
	//            toDrop = ModContent.ItemType<Items.Material.Ores.Zircon>();
	//            break;
	//    }
	//    if (toDrop > 0) Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, toDrop);
	//}

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
		if (tile.TileFrameX >= 18 * 6)
		{
			if (WorldGen.SolidTile(i - 1, j) || WorldGen.SolidTile(i + 1, j) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1))
			{
				tile.TileFrameX -= 18 * 6;
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
