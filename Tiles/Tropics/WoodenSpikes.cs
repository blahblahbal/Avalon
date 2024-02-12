using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics;

public class WoodenSpikes : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(132, 65, 172), LanguageManager.Instance.GetText("Wooden Spike"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        RegisterItemDrop(ItemID.WoodenSpike);
        DustType = DustID.WoodFurniture;
    }
    public override bool Slope(int i, int j)
    {
        return false;
    }
}
