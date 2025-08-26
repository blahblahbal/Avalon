using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Tiles;
public class BloodfallTile : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(200, 0, 0));
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileBrick[Type] = true;
		TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
		TileID.Sets.ForcedDirtMerging[Type] = true;
		HitSound = SoundID.Shatter;
		DustType = DustID.Glass;
	}
	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 7) // waterfalls set this value to 5, lava/honey/sand set this to 10
		{
			frameCounter = 0;
			frame++;
			if (frame > 7)
			{
				frame = 0;
			}
		}
	}
	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameYOffset = Main.tileFrame[type] * 90;
	}
}
