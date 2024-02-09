using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class SulphurOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(218, 216, 114), this.GetLocalization("MapEntry"));
        TileID.Sets.Ore[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Enchanted_Gold;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
    }
}
