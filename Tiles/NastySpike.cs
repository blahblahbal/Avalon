using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class NastySpike : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(89, 69, 53), this.GetLocalization("MapEntry"));
		Main.tileSolid[Type] = true;
		DustType = DustID.ScourgeOfTheCorruptor;
		HitSound = SoundID.Tink;
	}
}
