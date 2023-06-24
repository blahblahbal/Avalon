using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class RhodiumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 2f;
        AddMapEntry(new Color(187, 99, 115), LanguageManager.Instance.GetText("Rhodium"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 420;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1150;
        //ItemDrop = ModContent.ItemType<Items.Material.Ores.RhodiumOre>();
        HitSound = SoundID.Tink;
        MinPick = 60;
        DustType = DustID.t_LivingWood;
        TileID.Sets.Ore[Type] = true;
    }
}
