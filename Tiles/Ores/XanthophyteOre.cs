using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class XanthophyteOre : ModTile
{
    private readonly Color xanthophyteColor = new(210, 217, 0);

    public override void SetStaticDefaults()
    {
        AddMapEntry(xanthophyteColor, LanguageManager.Instance.GetText("Xanthophyte"));
        Main.tileSolid[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 775;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 705;
        Main.tileMerge[Type][ModContent.TileType<Savanna.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Savanna.Loam>()][Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Confetti_Yellow;
        MinPick = 200;
        TileID.Sets.JungleSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.Ore[Type] = true;
        TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
        // Unlike chlorophyte
        TileID.Sets.OreMergesWithMud[Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
