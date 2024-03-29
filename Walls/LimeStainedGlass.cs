using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class LimeStainedGlass : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Transparent[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<LimeStainedGlass>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.LimeStainedGlass>();
        DustType = ModContent.DustType<Dusts.PeridotDust>();
        HitSound = SoundID.Shatter;
    }
}
