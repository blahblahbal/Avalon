using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ImperviousBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(10, 10, 10));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[TileID.Ash][Type] = true;
        Main.tileMerge[Type][TileID.Ash] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.ImperviousBrick>();
        HitSound = SoundID.Tink;
        MinPick = 300;
        DustType = DustID.Wraith;
    }
    public override bool Slope(int i, int j)
    {
        return ModContent.GetInstance<DownedBossSystem>().DownedPhantasm;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
