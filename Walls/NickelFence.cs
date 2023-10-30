using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class NickelFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.NickelFence>();
        //AddMapEntry(new Color(52, 78, 85));
        DustType = ModContent.DustType<Dusts.NickelDust>();
        Main.wallLight[Type] = true;
        WallID.Sets.AllowsWind[Type] = true;
        WallID.Sets.AllowsPlantsToGrow[Type] = true;
    }
}
