using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ContagionGrassWallSafe : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(106, 116, 59));
        HitSound = SoundID.Grass;
        Main.wallHouse[Type] = true;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
