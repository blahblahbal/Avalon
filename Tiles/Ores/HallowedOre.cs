using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class HallowedOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 5.5f;
        AddMapEntry(new Color(219, 183, 0), this.GetLocalization("MapEntry"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 690;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1150;
        HitSound = SoundID.Tink;
        MinPick = 185;
        DustType = DustID.Enchanted_Gold;
        TileID.Sets.Ore[Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
