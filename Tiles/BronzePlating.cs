using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BronzePlating : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(121, 50, 42));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLargeFrames[Type] = 1;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BronzePlating>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.BronzeDust>();
    }
}
