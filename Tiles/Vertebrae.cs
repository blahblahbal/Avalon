using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class Vertebrae : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(255, 127, 127));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        RegisterItemDrop(ItemID.Vertebrae);
        HitSound = SoundID.NPCHit2;
        DustType = DustID.HeartCrystal;
    }
}
