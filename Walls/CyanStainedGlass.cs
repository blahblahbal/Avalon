using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class CyanStainedGlass : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<CyanStainedGlass>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.CyanStainedGlass>();
        AddMapEntry(new Color(76, 74, 72));
        DustType = ModContent.DustType<Dusts.TourmalineDust>();
    }
}
