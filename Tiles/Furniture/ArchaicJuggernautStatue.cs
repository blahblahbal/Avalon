using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture;

public class ArchaicJuggernautStatue : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.StyleWrapLimit = 70;
		TileObjectData.newTile.Width = 4;
        TileObjectData.newTile.Height = 6;
        TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16];
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
        TileObjectData.addAlternate(1);
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(144, 148, 144), Language.GetText("MapObject.Statue"));
        DustType = DustID.Stone;
        TileID.Sets.DisableSmartCursor[Type] = true;
    }
	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		int item = 0;
		int num9 = (Main.tile[i, j].TileFrameY / 74);
		switch (num9)
		{
			case 0:
			case 1:
				item = ModContent.ItemType<Items.Placeable.Tile.ArchaicJuggernautStatue>();
				break;
		}

		yield return new Item(item);
	}
}
