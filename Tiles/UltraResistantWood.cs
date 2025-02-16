using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class UltraResistantWood : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(50, 50, 50));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
		TileID.Sets.DisableSmartCursor[Type] = true;
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Tile.ResistantWood>());
        DustType = ModContent.DustType<ResistantWoodDust>();
        //MinPick = 225;
        Main.tileAxe[Type] = true;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
    public override bool Slope(int i, int j)
    {
        return false;
    }
}
