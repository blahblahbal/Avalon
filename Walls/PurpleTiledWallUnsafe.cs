using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class PurpleTiledWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallDungeon[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.PurpleTiledWall>();
        AddMapEntry(new Color(67, 52, 107));
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
    }
}
