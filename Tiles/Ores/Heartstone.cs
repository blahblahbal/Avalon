using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class Heartstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(217, 2, 55), this.GetLocalization("MapEntry"));
        TileID.Sets.Ore[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.HeartstoneDust>();
    }
}
