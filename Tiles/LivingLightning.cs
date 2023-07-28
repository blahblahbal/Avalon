using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class LivingLightning : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(196, 142, 238));
        Main.tileLighted[Type] = true;
        DustType = DustID.Bone;
    }
    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frame = Main.tileFrame[TileID.LivingFire];
    }
}
