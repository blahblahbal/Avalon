using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ImperviousBrickWallEctoUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        AddMapEntry(new Color(27, 194, 254));
        DustType = DustID.DungeonSpirit;
        Data.Sets.Wall.Hellcastle[Type] = true;
    }
    public override void KillWall(int i, int j, ref bool fail)
    {
        if (!ModContent.GetInstance<DownedBossSystem>().DownedPhantasm) fail = true;
    }
}
