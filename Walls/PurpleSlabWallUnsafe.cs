using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class PurpleSlabWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallDungeon[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.PurpleSlabWall>();
        AddMapEntry(new Color(51, 36, 91));
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
    }
}
