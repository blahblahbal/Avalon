using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class Chunkstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(48, 53, 42));
        Main.tileShine2[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.Stone[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.Stone[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        ItemDrop = ModContent.ItemType<ChunkstoneBlock>();
        HitSound = SoundID.Tink;
        MinPick = 60;
        DustType = ModContent.DustType<ContagionDust>();
    }
}
