using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Savanna;

public class Bramble : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(65, 67, 24));
        TileID.Sets.TouchDamageImmediate[Type] = 20;
        Main.tileBlockLight[Type] = true;
        HitSound = SoundID.Grass;
        DustType = ModContent.DustType<Dusts.TropicalDust>();
        TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;
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
