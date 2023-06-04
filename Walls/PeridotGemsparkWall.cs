using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class PeridotGemsparkWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallLight[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<PeridotGemsparkWall>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.PeridotGemsparkWall>();
        AddMapEntry(new Color(143, 255, 92));
        DustType = ModContent.DustType<Dusts.PeridotDust>();
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.714f * 0.8f;
        g = 1f * 0.8f;
        b = 0;
    }
}
public class PeridotGemsparkWallOff : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<PeridotGemsparkWallOff>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.PeridotGemsparkWall>();
        AddMapEntry(new Color(73, 108, 23));
        DustType = ModContent.DustType<Dusts.PeridotDust>();
    }
}
