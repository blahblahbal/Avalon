using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;
public class RhodiumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Walls.RhodiumBrickWallItem>();
        AddMapEntry(new Color(79, 40, 61));
        DustType = DustID.t_LivingWood;
    }
}
