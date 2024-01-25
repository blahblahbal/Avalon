using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class Booger : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(83, 65, 67));
        Main.tileSolid[Type] = false;
        Main.tileBlockLight[Type] = false;
        RegisterItemDrop(ModContent.ItemType<Items.Material.Booger>());
        DustType = ModContent.DustType<Dusts.SnotsandDust>();
    }
    public override bool Slope(int i, int j)
    {
        return false;
    }
    public override void FloorVisuals(Player player)
    {
        player.velocity *= 0.6f;
    }
}
