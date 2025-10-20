using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.HardenedSnotsand;

public class HardenedSnotsandBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<HardenedSnotsand>());
	}
}
public class HardenedSnotsand : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(64, 78, 59));
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		TileID.Sets.Conversion.HardenedSand[Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
		TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
		DustType = ModContent.DustType<ContagionDust>();

		TileID.Sets.ChecksForMerge[Type] = true;
	}
	public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
	{
		WorldGen.TileMergeAttempt(-2, ModContent.TileType<Snotsand.Snotsand>(), ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
		WorldGen.TileMergeAttemptFrametest(i, j, Type, ModContent.TileType<Snotsandstone.Snotsandstone>(), ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
	}
	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
		return true;
	}
}
