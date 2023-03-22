using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BoltstoneBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(21, 150, 59));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BoltstoneBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.JungleSpray>();
    }
}
