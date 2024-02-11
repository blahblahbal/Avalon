using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics;

public class TropicalStone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(234, 234, 234));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        //ItemDrop = ModContent.ItemType<TropicalStoneBlock>();
        HitSound = SoundID.Tink;
        DustType = DustID.SnowBlock;
    }
}
