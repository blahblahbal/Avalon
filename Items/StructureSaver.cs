using Avalon.Common;
using Avalon.Hooks;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.CrystalMines;
using Avalon.Tiles.Savanna;
using Avalon.WorldGeneration.Passes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
					Console.Clear();
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
							arrayLines.Add(Main.tile[minCoords.X + j, minCoords.Y + i].TileType);
						}
						Console.WriteLine(String.Join(", ", arrayLines.Cast<int>()));
					}
					coordStartSet = false;
					storedCoords.start = Point.Zero;
					storedCoords.end = Point.Zero;
				}
			}

		}
		return false;
    }
}
