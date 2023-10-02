using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BlastedStone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(20, 20, 20));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BlastedStone>();
        HitSound = SoundID.Tink;
        DustType = DustID.Wraith;
        TileID.Sets.HellSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail && !effectOnly)
        {
            if (Main.tile[i, j - 1].TileType == ModContent.TileType<BlastedStalac>())
            {
                WorldGen.KillTile(i, j - 1);
                if (Main.tile[i, j - 2].TileType == ModContent.TileType<BlastedStalac>())
                {
                    WorldGen.KillTile(i, j - 2);
                }
            }
            if (Main.tile[i, j + 1].TileType == ModContent.TileType<BlastedStalac>())
            {
                WorldGen.KillTile(i, j + 1);
                if (Main.tile[i, j + 2].TileType == ModContent.TileType<BlastedStalac>())
                {
                    WorldGen.KillTile(i, j + 2);
                }
            }
        }
    }
}
