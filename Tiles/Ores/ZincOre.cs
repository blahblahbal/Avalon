using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class ZincOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(168, 155, 168), LanguageManager.Instance.GetText("Zinc"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1025;
        Main.tileOreFinderPriority[Type] = 255;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Material.Ores.ZincOre>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.ZincDust>();
        TileID.Sets.Ore[Type] = true;
    }
}
