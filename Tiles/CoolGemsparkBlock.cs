using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CoolGemsparkBlock : ModTile
{
	public static int R { get; private set; } = 160;
	public static int G { get; private set; } = 0;
	public static int B { get; private set; } = 255;
	private static ModWaterfallStyle waterfallStyle;
	private static byte time;

	public override void SetStaticDefaults()
	{
		if (Main.netMode != NetmodeID.Server)
		{
			waterfallStyle = Mod.Find<ModWaterfallStyle>("CoolGemsparkWaterfallStyle");
		}
		AddMapEntry(Color.DarkCyan);
		Main.tileSolid[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileLighted[Type] = true;
		TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
		TileID.Sets.ForcedDirtMerging[Type] = true;
		TileID.Sets.GemsparkFramingTypes[Type] = Type;
		DustType = DustID.Ebonwood;
		TileID.Sets.DontDrawTileSliced[Type] = true;
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = R / 255f;
		g = G / 255f;
		b = B / 255f;
	}
	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
		return false;
	}
	public override void ChangeWaterfallStyle(ref int style)
	{
		style = waterfallStyle.Slot;
	}
	// colors are determined here, drawing is done in Avalon.Hooks.TileDrawingHooks
	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (time <= 31)
		{
			R -= 5;
			if (R <= 0)
			{
				R = 0;
			}
		}
		if (time >= 32 && time <= 112)
		{
			G += 5;
			if (G >= 255)
			{
				G = 255;
			}
			if (G >= 160)
			{
				B -= 5;
				if (B <= 0)
				{
					B = 0;
				}
			}
		}
		if (time >= 112 && time <= 180)
		{
			G -= 5;
			if (G <= 0)
			{
				G = 0;
			}
			if (G <= 160)
			{
				B += 5;
				if (B >= 255)
				{
					B = 255;
				}
			}
		}
		if (time >= 180)
		{
			R += 5;
			if (R >= 160)
			{
				R = 160;
			}
		}
		time++;
		time = (byte)(time % 212);
	}
}
