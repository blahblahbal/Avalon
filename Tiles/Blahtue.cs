using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class Blahtue : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
        TileObjectData.newTile.StyleWrapLimit = 52;
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 18 };
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
                item = ModContent.ItemType<Items.Placeable.Statue.Blahtue>();
                break;
        }

        yield return new Item(item);
    }
}
