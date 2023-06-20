using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class BacciliteBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.BismuthBrickWall>();
        AddMapEntry(new Color(51, 66, 55));
        Main.wallLight[Type] = true;
        DustType = ModContent.DustType<Dusts.ChunkstoneBrickDust>();
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.1f;
        g = 0.15f;
        b = 0f;
    }
}
