using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class YellowTiledWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallDungeon[Type] = true;
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Wall.YellowTiledWall>());
        AddMapEntry(new Color(55, 56, 36));
        DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
    }
}
