using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class LaziteGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(3, 2, 209));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBlendAll[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[Type][ModContent.TileType<BlastedStone>()] = true;
        Main.tileMerge[ModContent.TileType<BlastedStone>()][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BlastedStone>();
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<BlastedStone>();
        DustType = DustID.GemSapphire;
        HitSound = SoundID.Tink;
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            Main.tile[i, j].TileType = (ushort)ModContent.TileType<BlastedStone>();
            WorldGen.SquareTileFrame(i, j);
        }
    }
}
