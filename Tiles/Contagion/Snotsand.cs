using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class Snotsand : ModTile
{
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(136, 157, 56));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSand[Type] = true;
        TileID.Sets.Conversion.Sand[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.Falling[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        Common.TileMerge.MergeWith(Type, ModContent.TileType<Snotsandstone>());
        DustType = ModContent.DustType<SnotsandDust>();
    }
    //public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    //{
    //    //Common.TileMerge.MergeWithFrame(i, j, Type, ModContent.TileType<Snotsandstone>(), false, false, false, false, resetFrame);
    //    return true;
    //}

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        if (j < Main.maxTilesY && !Main.tile[i, j + 1].HasTile)
        {
            Main.tile[i, j].Get<TileWallWireStateData>().HasTile = false;
            int num = Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16f + 8f, j * 16f + 8f), new Vector2(0f, 0.41f), ModContent.ProjectileType<Projectiles.SnotsandBall>(), 15, 0f, 255);
            WorldGen.SquareTileFrame(i, j);
            return false;
        }
        return true;
    }
    public override bool HasWalkDust() => Main.rand.NextBool(3);

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
