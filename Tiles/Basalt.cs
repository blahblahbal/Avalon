using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class Basalt : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(39, 40, 42));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Wraith;
    }
    public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
    {
        int PseudoRandom = i * (int)(Math.Sin(i * 0.1f) + Math.Tan(i * 0.3f));
        if (PseudoRandom % 2 == 0 && PseudoRandom % 3 != 0)
            frameYOffset = 90;
        else if (PseudoRandom % 3 == 0 && PseudoRandom % 2 != 0)
            frameYOffset = 180;
    }
}
