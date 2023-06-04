using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Avalon.Common.Templates;

namespace Avalon.Tiles.Furniture.Gem;
public class AmberChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.AmberChest>();
	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int style = TileObjectData.GetTileStyle(tile);
		if (style == 0)
		{
			yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.AmberChest>());
		}
		if (style == 1)
		{
			// Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
			// No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
			yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.AmberChest>());
		}
	}
}
public class AmethystChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.AmethystChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.AmethystChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.AmethystChest>());
        }
    }
}
public class DiamondChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.DiamondChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.DiamondChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.DiamondChest>());
        }
    }
}
public class EmeraldChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.EmeraldChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.EmeraldChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.EmeraldChest>());
        }
    }
}
public class PeridotChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.PeridotChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.PeridotChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.PeridotChest>());
        }
    }
}
public class RubyChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.RubyChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.RubyChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.RubyChest>());
        }
    }
}
public class SapphireChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.SapphireChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.SapphireChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.SapphireChest>());
        }
    }
}
public class TopazChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.TopazChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.TopazChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.TopazChest>());
        }
    }
}
public class TourmalineChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.TourmalineChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.TourmalineChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.TourmalineChest>());
        }
    }
}
public class ZirconChest : ChestTemplate
{
    public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.ZirconChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.ZirconChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.Gem.ZirconChest>());
        }
    }
}
