using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class LimeStainedGlass : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<LimeStainedGlass>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.LimeStainedGlass>();
        AddMapEntry(new Color(76, 74, 72));
        DustType = ModContent.DustType<Dusts.PeridotDust>();
    }
}
