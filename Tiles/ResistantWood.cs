using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ResistantWood : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(50, 50, 50));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.ResistantWood>();
        DustType = DustID.Wraith;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
