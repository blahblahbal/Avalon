using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Tiles.Ores;

public class BismuthOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(187, 89, 192), LanguageManager.Instance.GetText("Bismuth"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1000;
        Main.tileOreFinderPriority[Type] = 275;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Material.Ores.BismuthOre>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.BismuthDust>();
        TileID.Sets.Ore[Type] = true;
    }
}
