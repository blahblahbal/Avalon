using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Bricks;

public class CaesiumSlab : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(56, 157, 107));
		Main.tileSolid[Type] = true;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 2050;
		Main.tileBlockLight[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileMerge[Type][TileID.WoodBlock] = true;
		Main.tileMerge[TileID.WoodBlock][Type] = true;
		HitSound = SoundID.Tink;
		DustType = ModContent.DustType<Dusts.CaesiumDust>();
	}
}
