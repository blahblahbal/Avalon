using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class BismuthBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.BismuthBrickWall>();
        AddMapEntry(new Color(96, 53, 105));
        DustType = ModContent.DustType<Dusts.BismuthDust>();
    }
}
