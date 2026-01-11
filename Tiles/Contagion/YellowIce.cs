using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class YellowIce : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(234, 204, 0));
        Main.tileBrick[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        HitSound = SoundID.Item50;
        DustType = ModContent.DustType<YellowIceDust>();
        TileID.Sets.Conversion.Ice[Type] = true;
        TileID.Sets.Ices[Type] = true;
        TileID.Sets.IcesSlush[Type] = true;
        TileID.Sets.IcesSnow[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
    }
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
}
