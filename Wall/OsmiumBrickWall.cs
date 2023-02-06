using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Wall;
public class OsmiumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Walls.OsmiumBrickWallItem>();
        AddMapEntry(new Color(24, 97, 149));
        DustType = ModContent.DustType<Dusts.OsmiumDust>();
    }
}
