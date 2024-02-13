using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class Tourmaline : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Aqua, this.GetLocalization("MapEntry"));
        Main.tileSolid[Type] = true;
        RegisterItemDrop(ModContent.ItemType<Items.Material.Ores.Tourmaline>());
        TileID.Sets.Ore[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileStone[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Tropics.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Tropics.Loam>()][Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Stone;
    }
}
