using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class SavannaGrassWall : ModWall
{
    public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(76, 61, 0));
        HitSound = SoundID.Grass;
        DustType = ModContent.DustType<SavannaGrassBladeDust>();
    }
}
