using Avalon;
using Avalon.Dusts;
using Avalon.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.Snotsand;

public class SnotsandBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
		ItemID.Sets.SandgunAmmoProjectileData[Type] = new(ModContent.ProjectileType<SnotsandSandgunProjectile>(), 5);
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Snotsand>());
		Item.width = 12;
		Item.height = 12;
		Item.ammo = AmmoID.Sand;
		Item.notAmmo = true;
	}
}
public class Snotsand : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileBlockLight[Type] = true;

		Main.tileSand[Type] = true;
		TileID.Sets.Conversion.Sand[Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
		TileID.Sets.CanBeDugByShovel[Type] = true;
		TileID.Sets.Falling[Type] = true;
		TileID.Sets.Suffocate[Type] = true;
		TileID.Sets.FallingBlockProjectile[Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<SnotsandBallFallingProjectile>());

		TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
		TileID.Sets.GeneralPlacementTiles[Type] = false;
		TileID.Sets.ChecksForMerge[Type] = true;

		MineResist = 0.5f;
		DustType = ModContent.DustType<SnotsandDust>();
		AddMapEntry(new Color(136, 157, 56));
	}
	public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
	{
		WorldGen.TileMergeAttemptFrametest(i, j, Type, ModContent.TileType<HardenedSnotsand.HardenedSnotsand>(), ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
		WorldGen.TileMergeAttemptFrametest(i, j, Type, ModContent.TileType<Snotsandstone.Snotsandstone>(), ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
	}
	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
		return true;
	}
	public override bool HasWalkDust() => Main.rand.NextBool(3);

	public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
	{
		dustType = DustType;
	}
}
