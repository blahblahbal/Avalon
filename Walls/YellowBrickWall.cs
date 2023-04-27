using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class YellowBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallDungeon[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.YellowBrickWall>();
        AddMapEntry(new Color(65, 61, 42));
        DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
    }
}
