using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Tiles.Ores;

public class IridiumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(146, 167, 123), LanguageManager.Instance.GetText("Iridium"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1150;
        Main.tileOreFinderPriority[Type] = 440;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Material.Ores.IridiumOre>();
        HitSound = SoundID.Tink;
        MinPick = 60;
        DustType = ModContent.DustType<Dusts.IridiumDust>();
        TileID.Sets.Ore[Type] = true;
    }
}
