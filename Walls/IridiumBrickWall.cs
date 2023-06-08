using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;
public class IridiumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.IridiumBrickWallItem>();
        AddMapEntry(new Color(81, 97, 61));
        DustType = ModContent.DustType<Dusts.IridiumDust>();
    }
}
