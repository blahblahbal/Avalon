using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class Starstone : ModTile
{
    private Color starstoneColor = new Color(42, 102, 221);
    public override void SetStaticDefaults()
    {
        AddMapEntry(starstoneColor, LanguageManager.Instance.GetText("Starstone"));
        Main.tileSolid[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 775;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Material.Ores.Starstone>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.StarstoneDust>();
    }
}
