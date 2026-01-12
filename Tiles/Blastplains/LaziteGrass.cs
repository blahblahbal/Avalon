using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Blastplains;

public class LaziteGrass : ModTile
{
	private Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");

		AddMapEntry(new Color(3, 2, 209));
		Main.tileSolid[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileBlendAll[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileMerge[Type][ModContent.TileType<BlastedStone>()] = true;
		Main.tileMerge[ModContent.TileType<BlastedStone>()][Type] = true;
		RegisterItemDrop(ModContent.ItemType<Items.Placeable.Tile.BlastedStone>());
		TileID.Sets.NeedsGrassFraming[Type] = true;
		TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<BlastedStone>();
		DustType = DustID.GemSapphire;
		HitSound = SoundID.Tink;
	}
	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		var colors = new List<Color> { new(80, 80, 80), new(145, 145, 145) };
		int index = (int)(Main.GameUpdateCount / 135 % colors.Count);
		int nextIndex = (index + 1) % colors.Count;
		Color colorShift = Color.Lerp(colors[index], colors[nextIndex], Main.GameUpdateCount % 135 / 135f);

		TileGlowDrawing.DrawGlowmask(i, j, colorShift, glow);
	}
	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (fail && !effectOnly)
		{
			Main.tile[i, j].TileType = (ushort)ModContent.TileType<BlastedStone>();
			WorldGen.SquareTileFrame(i, j);
		}
	}
}
