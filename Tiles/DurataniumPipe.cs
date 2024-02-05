using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class DurataniumPipe : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(107, 20, 80));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.NickelBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.DurataniumDust>();
    }
}
