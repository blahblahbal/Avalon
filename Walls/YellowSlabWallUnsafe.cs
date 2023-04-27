using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class YellowSlabWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallDungeon[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.YellowSlabWall>();
        AddMapEntry(new Color(70, 58, 41));
        DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
    }
}
