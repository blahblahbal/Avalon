using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

[LegacyName("BacciliteBrickTile")]
public class BacciliteBrick : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(96, 124, 60));
		Main.tileSolid[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileMerge[Type][TileID.WoodBlock] = true;
		Main.tileMerge[TileID.WoodBlock][Type] = true;
		Main.tileLighted[Type] = true;
		HitSound = SoundID.Tink;
		DustType = ModContent.DustType<Dusts.ChunkstoneBrickDust>();
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.2f;
		g = 0.3f;
		b = 0f;
	}
}
