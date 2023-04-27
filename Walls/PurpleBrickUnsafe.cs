using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class PurpleBrickUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallDungeon[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.PurpleBrickWall>();
        AddMapEntry(new Color(40, 28, 69));
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
    }
}
