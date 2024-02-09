using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Ores;

public class SulphurBlob : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(218, 216, 114), this.GetLocalization("MapEntry"));
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = false;
        Main.tileOreFinderPriority[Type] = 441;
        Main.tileSpelunker[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);
        HitSound = SoundID.Tink;
        DustType = DustID.Enchanted_Gold;
    }
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        yield return new Item(ModContent.ItemType<Sulphur>(), WorldGen.genRand.Next(10, 18));
    }
}
