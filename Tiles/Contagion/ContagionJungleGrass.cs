using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class ContagionJungleGrass : ModTile
{
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(147, 166, 42));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.JungleGrass[Type] = true;
        TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        //TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = false;
        //TileID.Sets.CanBeDugByShovel[Type] = true;
        //TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
        //TileID.Sets.SpreadOverground[Type] = true;
        //TileID.Sets.SpreadUnderground[Type] = true;
        //TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        RegisterItemDrop(ItemID.MudBlock);
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            noItem = true;
            Main.tile[i, j].TileType = TileID.Mud;
            WorldGen.SquareTileFrame(i, j);
        }
    }
}
