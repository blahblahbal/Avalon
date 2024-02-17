using Avalon.Dusts;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tile = Avalon.Data.Sets.Tile;

namespace Avalon.Tiles.Contagion;

public class ContagionShortGrass : ModTile
{
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
    
    public override void SetStaticDefaults()
    {
        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.SlowlyDiesInWater[Type] = true;
        TileID.Sets.SwaysInWindBasic[Type] = true;
        TileID.Sets.DrawFlipMode[Type] = 1;
        TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileNoFail[Type] = true;
        DustType = ModContent.DustType<ContagionDust>();
        HitSound = SoundID.Grass;
        Tile.Conversion.ShortGrass[Type] = true;
        AddMapEntry(new Color(133, 150, 39));
    }

    public const int MushroomFrameX = 18 * 8;
        
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (Main.tile[i, j].TileFrameX == MushroomFrameX)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<VirulentMushroom>());
        }
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        WorldGen.PlantCheck(i, j);
        return false;
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        height = 20;
        offsetY = -2;
    }
    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        //Flips the sprite if x coord is odd. Makes the tile more interesting
        if (i % 2 == 0)
            spriteEffects = SpriteEffects.FlipHorizontally;
    }
}
