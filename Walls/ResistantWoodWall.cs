using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ResistantWoodWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ResistantWoodWall>();
        AddMapEntry(new Color(25, 25, 25));
        DustType = ModContent.DustType<ResistantWoodDust>();
    }
}
