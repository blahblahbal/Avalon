using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class Peridot : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Lime, this.GetLocalization("MapEntry"));
        Main.tileSolid[Type] = true;
        RegisterItemDrop(ModContent.ItemType<Items.Material.Ores.Peridot>());
        TileID.Sets.Ore[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Savanna.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Savanna.Loam>()][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileStone[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        HitSound = SoundID.Tink;
        DustType = DustID.Grass;
    }
}
