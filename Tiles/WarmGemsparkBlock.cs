using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class WarmGemsparkBlock : ModTile
{
	public static int G { get; set; } = 0;
	private static ModWaterfallStyle waterfallStyle;
	public static byte time;

	public override void SetStaticDefaults()
	{
		if (Main.netMode != NetmodeID.Server)
		{
			waterfallStyle = Mod.Find<ModWaterfallStyle>("WarmGemsparkWaterfallStyle");
		}
		AddMapEntry(Color.OrangeRed);
		Main.tileSolid[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileLighted[Type] = true;
		TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
		TileID.Sets.ForcedDirtMerging[Type] = true;
		TileID.Sets.GemsparkFramingTypes[Type] = Type;
		DustType = DustID.Crimstone;
		TileID.Sets.DontDrawTileSliced[Type] = true;
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1f;
		g = G / 255f;
		b = 0;
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
}
