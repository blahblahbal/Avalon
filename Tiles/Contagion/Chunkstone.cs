using Avalon.Common;
using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class Chunkstone : ModTile
{
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(69, 82, 61));
        //Main.tileShine2[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.Stone[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.Stone[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        HitSound = SoundID.Tink;
        MinPick = 60;
        DustType = ModContent.DustType<ContagionDust>();
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail && !effectOnly)
        {
            if (Main.tile[i, j - 1].TileType == ModContent.TileType<ContagionStalactgmites>())
            {
                WorldGen.KillTile(i, j - 1);
                if (Main.tile[i, j - 2].TileType == ModContent.TileType<ContagionStalactgmites>())
                {
                    WorldGen.KillTile(i, j - 2);
                }
            }
            if (Main.tile[i, j + 1].TileType == ModContent.TileType<ContagionStalactgmites>())
            {
                WorldGen.KillTile(i, j + 1);
                if (Main.tile[i, j + 2].TileType == ModContent.TileType<ContagionStalactgmites>())
                {
                    WorldGen.KillTile(i, j + 2);
                }
            }
        }
    }
}
