using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

public class StructureSaver : ModItem
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
		Item.useAnimation = Item.useTime = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = 0;
		Item.height = dims.Height;
		Item.autoReuse = false;
		//item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Scroll");
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public bool coordStartSet = false;
	public (Point start, Point end) storedCoords = (Point.Zero, Point.Zero);
	public override bool? UseItem(Player player)
	{
		int x = (int)Main.MouseWorld.X / 16;
		int y = (int)Main.MouseWorld.Y / 16;
		if (player.ItemAnimationJustStarted)
		{
			if (player.altFunctionUse == 2)
			{
				//coordStartSet = false;
				//storedCoords.start = Point.Zero;
				//storedCoords.end = Point.Zero;
				//return false;
				PlaceStructure(new Point(x, y));
			}
			else
			{
				if (!coordStartSet)
				{
					storedCoords.start = new Point(x, y);
					Dust d = Dust.QuickDust(storedCoords.start, Color.Red);
					d.fadeIn = 2f;
					coordStartSet = true;
				}
				else
				{
					storedCoords.end = new Point(x, y);
					SaveStructure(storedCoords.start, storedCoords.end);
					coordStartSet = false;
					storedCoords.start = Point.Zero;
					storedCoords.end = Point.Zero;
				}
			}

		}
		return false;
	}

	public static void SaveStructure(Point pos1, Point pos2)
	{
		Point minCoords = new(Math.Min(pos1.X, pos2.X), Math.Min(pos1.Y, pos2.Y));
		Point maxCoords = new(Math.Max(pos1.X, pos2.X), Math.Max(pos1.Y, pos2.Y));
		int width = maxCoords.X - minCoords.X + 1;
		int height = maxCoords.Y - minCoords.Y + 1;

		string path = Path.Combine(Main.SavePath, "AvalonSavedStructures");
		Directory.CreateDirectory(path);
		string fileBaseName = "SavedStructure_";
		int fileSuffix = 0;
		StringBuilder pathToFile = new(Path.Combine(path, fileBaseName + fileSuffix));
		while (File.Exists(pathToFile.ToString() + ".gz"))
		{
			fileSuffix++;
			pathToFile = new(Path.Combine(path, fileBaseName + fileSuffix));
		}

		using FileStream compressedFileStream = File.Create(pathToFile.ToString() + ".gz");
		using GZipStream compressor = new(compressedFileStream, CompressionLevel.Optimal);
		//using (StreamWriter writer = new(compressor))
		using (BinaryWriter writer = new(compressor))
		{
			writer.Write(1.0);
			writer.Write(width);
			writer.Write(height);
			#region binary types
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if ((i == 0 || i == height - 1) || (j == 0 || j == width - 1))
					{
						Dust d = Dust.QuickDust(minCoords + new Point(j, i), Color.Red);
						d.fadeIn = 2f;
					}
					Tile tile = Main.tile[minCoords.X + j, minCoords.Y + i];
					// HasTile
					writer.Write(tile.HasTile);
					// TileType
					if (tile.TileType >= TileID.Count)
					{
						writer.Write(true);
						writer.Write(ModContent.GetModTile(tile.TileType).FullName);
					}
					else
					{
						writer.Write(false);
						writer.Write(tile.TileType);
					}
					// BlockType
					writer.Write((byte)tile.BlockType);
					// WallType
					if (tile.WallType >= WallID.Count)
					{
						writer.Write(true);
						writer.Write(ModContent.GetModWall(tile.WallType).FullName);
					}
					else
					{
						writer.Write(false);
						writer.Write(tile.WallType);
					}
					// TileColor
					//writer.Write(tile.TileColor);
					// HasActuator
					writer.Write(tile.HasActuator);
					// IsActuated
					//writer.Write(tile.IsActuated);
					// LiquidType
					writer.Write(tile.LiquidType);
					// Liquid Amount
					//writer.Write(tile.LiquidAmount);
					// Red Wire
					//writer.Write(tile.RedWire);
					// Green Wire
					//writer.Write(tile.GreenWire);
					// Blue Wire
					//writer.Write(tile.BlueWire);
					// Yellow Wire
					//writer.Write(tile.YellowWire);
					// Frame Important and FrameX/Y
					if (Main.tileFrameImportant[tile.TileType])
					{
						writer.Write(true);
						writer.Write(tile.TileFrameX);
						writer.Write(tile.TileFrameY);
					}
					else
					{
						writer.Write(false);
					}
				}
			}
			#endregion binary types
		}

		Main.NewText("Structure saved to: " + $"[c/2eec78:" + pathToFile.ToString() + ".gz" + $"]");
	}

	public static void PlaceStructure(Point topLeft)
	{
		string CompressedFileName = Path.Combine(Main.SavePath, "AvalonSavedStructures", "SavedStructure_0.gz");

		using FileStream compressedFileStream = File.Open(CompressedFileName, FileMode.Open);
		using var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress);
		//using (StreamReader reader = new(decompressor))
		//AvalonUtils.NewTextRainbow(ModContent.Find<ModTile>(ModContent.GetModTile(ModContent.TileType<SkyBrick>()).FullName));
		//AvalonUtils.NewTextRainbow(ModContent.GetModTile(ModContent.TileType<SkyBrick>()).FullName);
		using (BinaryReader reader = new(decompressor))
		{
			double version = reader.ReadDouble();
			int width = reader.ReadInt32();
			int height = reader.ReadInt32();
			#region binary types
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if ((i == 0 || i == height - 1) || (j == 0 || j == width - 1))
					{
						Dust d = Dust.QuickDust(topLeft + new Point(j, i), Color.Red);
						d.fadeIn = 2f;
					}
					Tile tile = Main.tile[topLeft.X + j, topLeft.Y + i];
					// HasTile
					tile.HasTile = reader.ReadBoolean();
					// TileType
					if (reader.ReadBoolean())
					{
						tile.TileType = ModContent.Find<ModTile>(reader.ReadString()).Type;
					}
					else
					{
						tile.TileType = reader.ReadUInt16();
					}
					// BlockType
					tile.BlockType = (BlockType)reader.ReadByte();
					// WallType
					if (reader.ReadBoolean())
					{
						tile.WallType = ModContent.Find<ModWall>(reader.ReadString()).Type;
					}
					else
					{
						tile.WallType = reader.ReadUInt16();
					}
					// TileColor
					//writer.Write(tile.TileColor);
					// HasActuator
					tile.HasActuator = reader.ReadBoolean();
					// IsActuated
					//writer.Write(tile.IsActuated);
					// LiquidType
					tile.LiquidType = reader.ReadInt32();
					// Liquid Amount
					//writer.Write(tile.LiquidAmount);
					// Red Wire
					//writer.Write(tile.RedWire);
					// Green Wire
					//writer.Write(tile.GreenWire);
					// Blue Wire
					//writer.Write(tile.BlueWire);
					// Yellow Wire
					//writer.Write(tile.YellowWire);
					if (reader.ReadBoolean())
					{
						tile.TileFrameX = reader.ReadInt16();
						tile.TileFrameY = reader.ReadInt16();
					}
				}
			}
			WorldGeneration.Utils.SquareTileFrameArea(topLeft.X, topLeft.Y, width, height);
			WorldGeneration.Utils.SquareWallFrameArea(topLeft.X, topLeft.Y, width, height);
			#endregion binary types
		}
	}
}
