using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class ShroomiteOre : ModTile
{
	public override void SetStaticDefaults()
	{
		MineResist = 5f;
		AddMapEntry(Color.CornflowerBlue, this.GetLocalization("MapEntry"));
		Main.tileSolid[Type] = true;
		Main.tileSpelunker[Type] = true;
		Main.tileOreFinderPriority[Type] = 705;
		Main.tileBlockLight[Type] = true;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 1400;
		HitSound = SoundID.Tink;
		MinPick = 205;
		DustType = DustID.Clentaminator_Blue;
		TileID.Sets.ChecksForMerge[Type] = true;
		Main.tileMerge[TileID.MushroomGrass][Type] = true;
		TileID.Sets.JungleSpecial[Type] = true;
		TileID.Sets.Ore[Type] = true;
	}
	public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
	{
		WorldGen.TileMergeAttempt(-2, TileID.MushroomGrass, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
		WorldGen.TileMergeAttempt(-2, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
	}
	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
