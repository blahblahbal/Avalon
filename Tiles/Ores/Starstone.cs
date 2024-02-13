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
        AddMapEntry(starstoneColor, this.GetLocalization("MapEntry"));
        TileID.Sets.Ore[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 775;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Tropics.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Tropics.Loam>()][Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.StarstoneDust>();
    }
}
