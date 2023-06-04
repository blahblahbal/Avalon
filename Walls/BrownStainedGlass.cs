using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class BrownStainedGlass : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<BrownStainedGlass>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.BrownStainedGlass>();
        AddMapEntry(new Color(76, 74, 72));
        DustType = ModContent.DustType<Dusts.ZirconDust>();
    }
}