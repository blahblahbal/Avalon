using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.SkyCastle;

public class SkyBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(102, 102, 82));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[TileID.Cloud][Type] = true;
        Main.tileMerge[Type][TileID.Cloud] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.SkyBrick>();
        HitSound = SoundID.Tink;
        MinPick = 300;
        DustType = DustID.Smoke;
    }
    public override bool Slope(int i, int j)
    {
        return ModContent.GetInstance<DownedBossSystem>().DownedDragonLord;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
