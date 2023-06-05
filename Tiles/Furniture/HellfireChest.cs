using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Avalon.Common.Templates;

namespace Avalon.Tiles.Furniture;

public class HellfireChest : ChestTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.HellfireChest>();
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int style = TileObjectData.GetTileStyle(tile);
        if (style == 0)
        {
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.HellfireChest>());
        }
        if (style == 1)
        {
            // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
            // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
            yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.HellfireChest>());
        }
    }
}
