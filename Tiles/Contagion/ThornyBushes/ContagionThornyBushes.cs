using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.ThornyBushes;

public class ContagionThornyBushes : ModTile
{
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
    public override void SetStaticDefaults()
    {
        Main.tileNoFail[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.Thorn[Type] = true;
        TileID.Sets.TileCutIgnore.IgnoreDontHurtNature[Type] = true;
        TileID.Sets.GetsDestroyedForMeteors[Type] = true;
        TileID.Sets.TouchDamageDestroyTile[Type] = true;
        TileID.Sets.TouchDamageImmediate[Type] = 12;
        TileID.Sets.SpreadOverground[Type] = true;
        //TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        //TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
        //TileObjectData.newTile.CoordinateWidth = 16;
        //TileObjectData.newTile.CoordinatePadding = 2;
        //TileObjectData.newTile.StyleHorizontal = false;
        //TileObjectData.newTile.LavaDeath = false;
        //TileObjectData.addTile(Type);
        HitSound = SoundID.Grass;
        AddMapEntry(new Color(155, 174, 50));
        DustType = ModContent.DustType<ContagionDust>();
    }

    //public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    //{

    //}
}
