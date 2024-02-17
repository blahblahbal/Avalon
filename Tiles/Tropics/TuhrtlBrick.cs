using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics;

public class TuhrtlBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(78, 70, 67));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.TuhrtlBrick>();
        HitSound = SoundID.Tink;
        MinPick = 210;
        DustType = DustID.Silt;
        TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        TileID.Sets.JungleSpecial[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
