using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class PoisonSpike : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(95, 95, 36), this.GetLocalization("MapEntry"));
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		HitSound = SoundID.Tink;
		DustType = DustID.Grass;
	}
	public override bool Slope(int i, int j)
	{
		return false;
	}
}
