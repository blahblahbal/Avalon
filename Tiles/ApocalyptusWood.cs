using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ApocalyptusWood : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(80, 63, 88));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.ApocalyptusWood>();
        DustType = ModContent.DustType<Dusts.DarkMatterWoodDust>();
    }
}
