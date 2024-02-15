using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class Bramble : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(78, 70, 67));
        TileID.Sets.TouchDamageImmediate[Type] = 20;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.IsBeam[Type] = true;
        Main.tileNoAttach[Type] = true;
        TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;
        HitSound = SoundID.Grass;
        DustType = DustID.JungleGrass;
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
