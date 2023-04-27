using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class YellowTiledWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.YellowTiledWall>();
        AddMapEntry(new Color(60, 59, 39));
        DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
    }
}
