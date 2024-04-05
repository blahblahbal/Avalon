using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class YuckyBit : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(123, 123, 40));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        RegisterItemDrop(ModContent.ItemType<Items.Material.YuckyBit>());
        DustType = DustID.CorruptGibs;
    }
}
