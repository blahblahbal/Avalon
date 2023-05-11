using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class TwilightWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.TwilightWallItem>();
        AddMapEntry(new Color(96, 0, 137));
        DustType = ModContent.DustType<Dusts.BismuthDust>();
    }
}
