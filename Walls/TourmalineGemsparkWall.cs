using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class TourmalineGemsparkWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallLight[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<TourmalineGemsparkWall>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.TourmalineGemsparkWall>();
        AddMapEntry(new Color(122, 211, 204));
        DustType = ModContent.DustType<Dusts.TourmalineDust>();
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0;
        g = 1f * 0.8f;
        b = 1f * 0.8f;
    }
}
public class TourmalineGemsparkWallOff : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<TourmalineGemsparkWallOff>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.TourmalineGemsparkWall>();
        AddMapEntry(new Color(36, 104, 83));
        DustType = ModContent.DustType<Dusts.TourmalineDust>();
    }
}
