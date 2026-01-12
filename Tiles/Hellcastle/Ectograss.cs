using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Hellcastle;

public class Ectograss : ModTile
{
	private Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");

		AddMapEntry(new Color(27, 194, 254));
		Main.tileSolid[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileBlendAll[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileLighted[Type] = true;
		TileID.Sets.NeedsGrassFraming[Type] = true;
		TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Ash;
		TileID.Sets.CanBeDugByShovel[Type] = true;
		TileID.Sets.Grass[Type] = true;
		RegisterItemDrop(ItemID.AshBlock);
		DustType = DustID.Silt;
	}
	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		TileGlowDrawing.DrawGlowmask(i, j, Color.White, glow);
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 35f / 255f;
		g = 200f / 255f;
		b = 254f / 255f;
	}
	//public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	//{
	//    if (fail && !effectOnly)
	//    {
	//        Main.tile[i, j].TileType = TileID.Ash;
	//        WorldGen.SquareTileFrame(i, j);
	//    }
	//}
}
