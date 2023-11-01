using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BasaltBlock : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(44, 45, 48));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Wraith;
    }
}
