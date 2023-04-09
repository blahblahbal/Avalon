using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CrimstoneColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(188, 40, 40));
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.CrimstoneColumn>();
        HitSound = SoundID.Tink;
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.CrimtaneWeapons;
    }
    public override bool CanPlace(int i, int j)
    {
        return (Main.tile[i, j - 1].HasTile || Main.tile[i, j + 1].HasTile || Main.tile[i, j].WallType != 0 && !Main.tile[i, j].HasTile);
    }
}
