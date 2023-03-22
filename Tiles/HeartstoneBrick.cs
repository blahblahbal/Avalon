using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class HeartstoneBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(173, 0, 38));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.HeartstoneBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.HeartstoneDust>();
    }
}
