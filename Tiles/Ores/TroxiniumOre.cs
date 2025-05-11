using Avalon.Dusts;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class TroxiniumOre : ModTile
{
	private Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");

		MineResist = 4f;
		AddMapEntry(Color.Goldenrod, this.GetLocalization("MapEntry"));
		Data.Sets.TileSets.RiftOres[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileSpelunker[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileOreFinderPriority[Type] = 660;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 875;
		HitSound = SoundID.Tink;
		MinPick = 150;
		DustType = ModContent.DustType<TroxiniumDust>();
		TileID.Sets.Ore[Type] = true;
		Main.tileMerge[Type][TileID.Mud] = true;
		Main.tileMerge[TileID.Mud][Type] = true;
		Main.tileMerge[Type][ModContent.TileType<Savanna.Loam>()] = true;
		Main.tileMerge[ModContent.TileType<Savanna.Loam>()][Type] = true;
	}
	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		TileGlowDrawing.DrawGlowmask(i, j, new Color(255, 255, 255, 0) * (Lighting.Brightness(i, j) * 4f), glow);
	}
	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
