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
        Main.tileShine[Type] = 1150;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BlastedStone>();
        HitSound = SoundID.Tink;
        DustType = DustID.Wraith;
        TileID.Sets.HellSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
}
