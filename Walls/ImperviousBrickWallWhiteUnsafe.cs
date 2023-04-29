using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ImperviousBrickWallWhiteUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        AddMapEntry(new Color(112, 112, 109));
        DustType = DustID.Stone;
    }
    //public override void KillWall(int i, int j, ref bool fail)
    //{
    //    if (!ModContent.GetInstance<DownedBossSystem>().DownedPhantasm) fail = true;
    //}
}
