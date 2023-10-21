using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class ContagionVines : ModTile
{
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
    public override void SetStaticDefaults()
    {
        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        TileID.Sets.IsVine[Type] = true;
        TileID.Sets.ReplaceTileBreakDown[Type] = true;
        TileID.Sets.VineThreads[Type] = true;
        TileID.Sets.DrawFlipMode[Type] = 1;
        Main.tileCut[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileNoFail[Type] = true;
        HitSound = SoundID.Grass;
        DustType = ModContent.DustType<ContagionDust>();
        AddMapEntry(new Color(117, 131, 37));
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if (Main.LightingEveryFrame)
        {
            Main.instance.TilesRenderer.CrawlToTopOfVineAndAddSpecialPoint(j, i);
        }
        return false;
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        var tile = Main.tile[i, j];
        var tileAbove = Framing.GetTileSafely(i, j - 1);

        int up;
        if (!tileAbove.HasUnactuatedTile)
            up = -1;
        else if (!tileAbove.BottomSlope)
            up = tileAbove.TileType;
        else
            up = -1;

        if (up != tile.TileType && tile.TileType == ModContent.TileType<ContagionVines>() && !CanGrowFromTile(up))
            WorldGen.KillTile(i, j);

        return false;
    }

    public static bool CanGrowFromTile(int tileType)
    {
        return tileType == ModContent.TileType<Ickgrass>() ||
               tileType == ModContent.TileType<ContagionJungleGrass>();
    }
}
