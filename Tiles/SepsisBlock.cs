using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class SepsisBlock : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(126, 132, 72));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.SepsisBlock>();
        DustType = DustID.BrownMoss;
        HitSound = SoundID.NPCDeath1;
    }
}
