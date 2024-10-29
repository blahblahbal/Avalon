using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class LivingLightning : ModTile
{
    public override void SetStaticDefaults()
	{
		TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;
		Main.tileLighted[Type] = true;
        DustType = ModContent.DustType<LivingLightningDust>();
		AnimationFrameHeight = 90;
        AddMapEntry(new Color(196, 142, 238));
    }
    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        if (++frameCounter > 5)
        {
            frameCounter = 0;
            if (++frame > 2) frame = 0;
        }
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.768f;
        g = 0.556f;
        b = 0.933f;
    }
	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = Main.rand.Next(4, 6);
	}
}
