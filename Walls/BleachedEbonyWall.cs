using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class BleachedEbonyWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.BleachedEbonyWall>();
        AddMapEntry(new Color(100, 100, 100));
        DustType = DustID.SnowBlock;
    }
}
