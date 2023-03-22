using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BrimstoneBlock : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(165, 80, 98));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        //Main.tileMerge[Type][ModContent.TileType<Ectograss>()] = true;
        //Main.tileMerge[ModContent.TileType<Ectograss>()][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BrimstoneBlock>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.BrimstoneDust>();
        TileID.Sets.HellSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
}
