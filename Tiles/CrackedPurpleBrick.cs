using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CrackedPurpleBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(102, 78, 123));//(96, 52, 151));// 166, 87, 45));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
        //HitSound = SoundID.Item127;
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
        TileID.Sets.CrackedBricks[Type] = true;
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail) AvalonWorld.ShatterCrackedBricks(i, j, Main.tile[i, j], true);
        SoundEngine.PlaySound(SoundID.Item127, new(i * 16, j * 16));
    }
    public override bool CanDrop(int i, int j)
    {
        return false;
    }
    public override bool IsTileDangerous(int i, int j, Player player)
    {
        return true;
    }
}
