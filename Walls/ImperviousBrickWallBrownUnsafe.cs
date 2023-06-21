using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ImperviousBrickWallBrownUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        AddMapEntry(new Color(73, 63, 50));
        DustType = DustID.Dirt;
    }
    //public override void KillWall(int i, int j, ref bool fail)
    //{
    //    if (!ModContent.GetInstance<DownedBossSystem>().DownedPhantasm) fail = true;
    //}
}
