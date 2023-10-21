using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ImperviousBrickWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        AddMapEntry(new Color(51, 44, 48));
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Wall.ImperviousBrickWallItem>());
        DustType = DustID.Wraith;
        Data.Sets.Wall.Hellcastle[Type] = true;
    }
    public override void KillWall(int i, int j, ref bool fail)
    {
        if (!ModContent.GetInstance<DownedBossSystem>().DownedPhantasm) fail = true;
    }
}
