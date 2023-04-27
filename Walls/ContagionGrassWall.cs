using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ContagionGrassWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(106, 116, 59));
        HitSound = SoundID.Grass;
        WallID.Sets.Conversion.Grass[Type] = true;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
