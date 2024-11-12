using Avalon.Common;
using Avalon.Hooks;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.CrystalMines;
using Avalon.Tiles.Savanna;
using Avalon.WorldGeneration.Passes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

class StructureSaver : ModItem
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
				coordStartSet = false;
				storedCoords.start = Point.Zero;
				storedCoords.end = Point.Zero;
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
					Point minCoords = new Point(Math.Min(storedCoords.start.X, storedCoords.end.X), Math.Min(storedCoords.start.Y, storedCoords.end.Y));
					Point maxCoords = new Point(Math.Max(storedCoords.start.X, storedCoords.end.X), Math.Max(storedCoords.start.Y, storedCoords.end.Y));
					int width = maxCoords.X - minCoords.X + 1;
					int height = maxCoords.Y - minCoords.Y + 1;
					//Console.Clear();
					List<string> lines = [];
					for (int data = 0; data < 4; data++)
					{
						string arrayType;
						switch (data)
						{
							case 0:
								arrayType = "Tile Type Array:" + "\n";
								//Console.WriteLine(arrayType);
								lines.Add(arrayType);
								break;
							case 1:
								arrayType = "\n\n" + "Wall Type Array:" + "\n";
								//Console.WriteLine(arrayType);
								lines.Add(arrayType);
								break;
							case 2:
								arrayType = "\n\n" + "Slope Type/Liquid Amount Array (negative values are inverse of the BlockType, positive are LiquidAmount):" + "\n";
								//Console.WriteLine(arrayType);
								lines.Add(arrayType);
								break;
							case 3:
								arrayType = "\n\n" + "Liquid Type Array:" + "\n";
								//Console.WriteLine(arrayType);
								lines.Add(arrayType);
								break;

						}
						for (int i = 0; i < height; i++)
						{
							List<int> arrayLines = new List<int>();
							for (int j = 0; j < width; j++)
							{
								if ((i == 0 || i == height - 1) || (j == 0 || j == width - 1))
								{
									Dust d = Dust.QuickDust(minCoords + new Point(j, i), Color.Red);
									d.fadeIn = 2f;
								}
								switch (data) // to-do: store actuation, paint, and coating
								{
									case 0:
										arrayLines.Add(Main.tile[minCoords.X + j, minCoords.Y + i].TileType);
										break;
									case 1:
										arrayLines.Add(Main.tile[minCoords.X + j, minCoords.Y + i].WallType);
										break;
									case 2:
										int states = (int)Main.tile[minCoords.X + j, minCoords.Y + i].BlockType;
										arrayLines.Add(states != 0 ? -states : Main.tile[minCoords.X + j, minCoords.Y + i].LiquidAmount);
										break;
									case 3:
										arrayLines.Add(Main.tile[minCoords.X + j, minCoords.Y + i].LiquidType);
										break;

								}
							}
							//Console.WriteLine("{" + String.Join(", ", arrayLines.Cast<int>()) + "},");
							lines.Add("{" + String.Join(", ", arrayLines.Cast<int>()) + "},");
						}
					}
					string path = Path.Combine(Main.SavePath, "AvalonSavedStructures");
					Directory.CreateDirectory(path);
					StringBuilder pathToFile = new StringBuilder(Path.Combine(path, "SavedStructure_0"));
					while (File.Exists(pathToFile.ToString() + ".txt"))
					{
						pathToFile[^1] = (char)(pathToFile[^1] + 1);
					}
					using (StreamWriter outputFile = new StreamWriter(pathToFile.ToString() + ".txt"))
					{
						foreach (string line in lines)
							outputFile.WriteLine(line);
					}
					Main.NewText("Structure saved to: " + $"[c/2eec78:" + pathToFile.ToString() + ".txt" + $"]");
					coordStartSet = false;
					storedCoords.start = Point.Zero;
					storedCoords.end = Point.Zero;
				}
			}

		}
		return false;
    }
}
