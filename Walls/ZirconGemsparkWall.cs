using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ZirconGemsparkWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallLight[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ZirconGemsparkWall>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ZirconGemsparkWall>();
        AddMapEntry(new Color(231, 170, 116));
        DustType = ModContent.DustType<Dusts.ZirconDust>();
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 1.1f * 0.8f;
        g = 0.75f * 0.8f;
        b = 0.5f * 0.8f;
    }
}
public class ZirconGemsparkWallOff : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ZirconGemsparkWallOff>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ZirconGemsparkWall>();
        AddMapEntry(new Color(121, 82, 49));
        DustType = ModContent.DustType<Dusts.ZirconDust>();
    }
}
