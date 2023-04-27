using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class MoonWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.MoonWallItem>();
        AddMapEntry(new Color(81, 76, 98));
        DustType = ModContent.DustType<Dusts.BismuthDust>();
    }
}
