using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Thorium.Walls;

public class ChrysoberylGemsparkWall : ModWall
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallLight[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ChrysoberylGemsparkWall>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ChrysoberylGemsparkWall>();
        AddMapEntry(new Color(180, 220, 95));
        DustType = ModContent.DustType<Dusts.ChrysoberylDust>();
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.745f * 0.8f;
        g = 0.925f * 0.8f;
        b = 0.1f * 0.8f;
    }
}
public class ChrysoberylGemsparkWallOff : ModWall
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ChrysoberylGemsparkWallOff>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ChrysoberylGemsparkWall>();
        AddMapEntry(new Color(86, 96, 23));
        DustType = ModContent.DustType<Dusts.ChrysoberylDust>();
    }
}
