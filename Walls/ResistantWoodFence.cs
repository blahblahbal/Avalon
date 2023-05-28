using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ResistantWoodFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ResistantWoodFence>();
        AddMapEntry(new Color(25, 25, 25));
        DustType = DustID.Wraith;
    }
}
