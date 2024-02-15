using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics;

public class Bramble : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(78, 70, 67));
        TileID.Sets.TouchDamageImmediate[Type] = 20;
        //Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        //Main.tileShine2[Type] = true;
        HitSound = SoundID.Grass;
        DustType = DustID.JungleGrass;
    }
    public override void FloorVisuals(Player player)
    {
        //player.velocity *= 0.6f;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
    public override bool Slope(int i, int j)
    {
        return false;
    }
    public override bool IsTileDangerous(int i, int j, Player player)
    {
        return true;
    }
}
