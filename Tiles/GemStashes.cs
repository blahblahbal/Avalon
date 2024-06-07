using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace Avalon.Tiles;

public class GemStashes : ModTile
{
    public override void SetStaticDefaults()
    {
        // Properties
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
		Main.tileSpelunker[Type] = true;
		TileID.Sets.DisableSmartCursor[Type] = true;

        // Placement
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.CoordinateHeights = new[] { 16 };
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(22, 212, 198), this.GetLocalization("Tourmaline"));
        AddMapEntry(new Color(0, 237, 14), this.GetLocalization("Peridot"));
        AddMapEntry(new Color(198, 168, 130), this.GetLocalization("Zircon"));
    }
    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)(Main.tile[i, j].TileFrameX / 36);
    }
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        int toDrop = 0;
        switch (Main.tile[i, j].TileFrameX / 36)
        {
            case 0:
                toDrop = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
                break;
            case 1:
                toDrop = ModContent.ItemType<Items.Material.Ores.Peridot>();
                break;
            case 2:
                toDrop = ModContent.ItemType<Items.Material.Ores.Zircon>();
                break;
        }
        yield return new Item(toDrop, WorldGen.genRand.Next(3) + 1);
    }
}
