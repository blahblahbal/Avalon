using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class StarstoneBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(42, 102, 221));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.StarstoneBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.StarstoneDust>();
    }
}
