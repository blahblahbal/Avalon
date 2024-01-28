using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Ores;

public class SmallSulphurBlob : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(218, 216, 114), LanguageManager.Instance.GetText("Sulphur Blob"));
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = false;
        Main.tileOreFinderPriority[Type] = 441;
        Main.tileSpelunker[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);
        HitSound = SoundID.Tink;
        DustType = DustID.Enchanted_Gold;
    }
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        yield return new Item(ModContent.ItemType<Sulphur>(), WorldGen.genRand.Next(5, 11));
    }
}
